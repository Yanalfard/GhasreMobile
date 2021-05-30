using DataLayer.Models;
using DataLayer.Utilities;
using DataLayer.ViewModels;
using GhasreMobile.Utilities;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace GhasreMobile.Controllers
{
    public class ShopCartController : Controller
    {
        Core db = new Core();
        TblClient SelectUser()
        {
            int userId = Convert.ToInt32(User.Claims.First().Value);
            TblClient selectUser = db.Client.GetById(userId);
            return selectUser;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                List<ShopCartItemVm> list = new List<ShopCartItemVm>();
                var sessions = HttpContext.Session.GetComplexData<List<ShopCartItem>>("ShopCart");
                if (sessions != null && sessions.Count > 0)
                {
                    List<ShopCartItem> listShop = (List<ShopCartItem>)sessions;

                    foreach (var item in listShop)
                    {
                        if (db.Product.Get().Any(i => i.ProductId == item.ProductID && i.IsDeleted == false))
                        {

                            var product = db.Product.Get().Where(p => p.ProductId == item.ProductID).Select(p => new
                            {
                                p.MainImage,
                                p.Name,
                                p.PriceAfterDiscount,
                                p.PriceBeforeDiscount,
                                p.Brand,
                            }).Single();
                            ShopCartItemVm shop = new ShopCartItemVm();
                            shop.Count = item.Count;
                            shop.ProductID = item.ProductID;
                            shop.ColorID = item.ColorID;
                            shop.ColorName = db.Color.GetById(item.ColorID).Name;
                            shop.Name = product.Name;
                            shop.ImageName = product.MainImage;
                            shop.PriceAfterDiscount = product.PriceAfterDiscount;
                            shop.PriceBeforeDiscount = product.PriceBeforeDiscount;
                            shop.Brand = product.Brand.Name;
                            shop.Special = 0;
                            shop.Sum = product.PriceAfterDiscount == 0 ? item.Count * product.PriceBeforeDiscount : product.PriceAfterDiscount * item.Count;
                            TblSpecialOffer offer = db.SpecialOffer.Get(i => i.ProductId == item.ProductID && i.ValidTill >= DateTime.Now).SingleOrDefault();
                            if (offer != null)
                            {
                                var Special = product.PriceAfterDiscount == 0 ? product.PriceBeforeDiscount : product.PriceAfterDiscount;
                                shop.Special = Special - (long)(Math.Floor((double)(Special * offer.Discount / 100)));
                                shop.Special = (long)MainUtil.Round((double)shop.Special, 3);
                                shop.Sum = shop.Sum - (long)(Math.Floor((double)(shop.Sum * offer.Discount / 100)));
                                shop.Sum = (long)MainUtil.Round((double)shop.Sum, 3);
                            };
                            list.Add(shop);
                        }
                        else
                        {
                            var index = listShop.FindIndex(p => p.ProductID == item.ProductID);
                            listShop[index].Count = 0;
                        }
                    }
                    HttpContext.Session.SetComplexData("ShopCart", listShop);
                }
                return await Task.FromResult(View(list));
            }
            catch
            {
                return await Task.FromResult(Redirect("404.html"));
            }
        }

        public async Task<IActionResult> UpDownCount(int id, int colorId, string command, string ReturnUrl = "/")
        {
            try
            {
                TblColor selectedProduct = db.Color.GetById(colorId);
                TblProduct selectedP = db.Product.GetById(id);
                var listShop = HttpContext.Session.GetComplexData<List<ShopCartItem>>("ShopCart");
                var index = listShop.FindIndex(p => p.ColorID == colorId);
                if (selectedP.TblColor.Any(i => i.ProductId == id))
                {
                    switch (command)
                    {
                        case "up":
                            {
                                if (selectedProduct != null)
                                {
                                    int count = selectedProduct.Count - listShop[index].Count;
                                    if (count > 0 && selectedProduct.ProductId == id && selectedProduct.ColorId == colorId)
                                    {
                                        listShop[index].Count += 1;
                                    }
                                }
                                break;
                            }
                        case "down":
                            {
                                listShop[index].Count -= 1;
                                if (listShop[index].Count == 0)
                                {
                                    listShop.RemoveAt(index);
                                }
                                break;
                            }
                        case "delete":
                            {

                                listShop.RemoveAt(index);

                                break;
                            }
                    }
                    HttpContext.Session.SetComplexData("ShopCart", listShop);
                    if (listShop != null && listShop.Count == 0)
                    {
                        return Redirect("/");
                    }
                }

                return await Task.FromResult(Redirect(ReturnUrl));
            }
            catch
            {
                return await Task.FromResult(Redirect("404.html"));
            }

        }


        public async Task<int> MinusProduct(int id)
        {
            var listShop = HttpContext.Session.GetComplexData<List<ShopCartItem>>("ShopCart");
            if (listShop.Any(p => p.ColorID == id))
            {
                var index = listShop.FindIndex(p => p.ColorID == id);
                listShop[index].Count -= 1;
                if (listShop[index].Count == 0)
                {
                    listShop[index].Count = 0;
                    listShop.RemoveAt(index);
                }
            }
            HttpContext.Session.SetComplexData("ShopCart", listShop);
            return await Task.FromResult(0);
        }

        public async Task<IActionResult> Comparison()
        {
            try
            {
                List<CompareItemVm> list = new List<CompareItemVm>();
                var Session = HttpContext.Session.GetComplexData<List<CompareItemVm>>("Compare");
                if (Session != null)
                {
                    list = Session as List<CompareItemVm>;
                }
                List<TblProperty> features = new List<TblProperty>();
                List<TblProductPropertyRel> productFeatures = new List<TblProductPropertyRel>();

                foreach (var item in list)
                {
                    features.AddRange(db.ProductPropertyRel.Get(p => p.ProductId == item.ProductID).Select(f => f.Property).ToList());
                    productFeatures.AddRange(db.ProductPropertyRel.Get(p => p.ProductId == item.ProductID).ToList());
                }
                if (list.Any())
                {
                    features.Insert(0, new TblProperty() { PropertyId = -1, Name = "رنگ" });

                    foreach (var pro in list)
                    {
                        var colorProducts = db.Color.Get(i => i.ProductId == pro.ProductID).ToList().Select(i => i.Name);
                        productFeatures.Add(new TblProductPropertyRel() { PropertyId = -1, ProductId = pro.ProductID, Value = string.Join(" ، ", colorProducts) });
                    }

                }
                VmComparison vm = new VmComparison(features.Distinct().ToList(), productFeatures, list);
                return await Task.FromResult(View(vm));
            }
            catch
            {
                return await Task.FromResult(Redirect("404.html"));
            }
        }
        public async Task<IActionResult> DeleteFromCompare(int id, string ReturnUrl = "/")
        {
            try
            {
                List<CompareItemVm> list = new List<CompareItemVm>();
                var Session = HttpContext.Session.GetComplexData<List<CompareItemVm>>("Compare");
                if (Session != null)
                {
                    list = Session as List<CompareItemVm>;
                    int index = list.FindIndex(p => p.ProductID == id);
                    list.RemoveAt(index);
                    HttpContext.Session.SetComplexData("Compare", list);
                }
                return await Task.FromResult(Redirect(ReturnUrl));
            }
            catch
            {
                return await Task.FromResult(Redirect("404.html"));
            }
        }
        public async Task<IActionResult> Bookmarks()
        {
            try
            {
                return await Task.FromResult(View());
            }
            catch
            {
                return await Task.FromResult(Redirect("404.html"));
            }
        }
        [PermissionChecker("user,employee,admin")]
        public async Task<IActionResult> Payment(string radio, string address, string IsFractional)
        {
            try
            {
                bool fractional = false;
                if (IsFractional == "1")
                {
                    fractional = true;
                }
                List<ShopCartItem> sessions = HttpContext.Session.GetComplexData<List<ShopCartItem>>("ShopCart");
                DiscountVm selectedDiscount = HttpContext.Session.GetComplexData<DiscountVm>("Discount");
                if (selectedDiscount != null)
                {
                    long sumWithIsFractional = selectedDiscount.Sum / 3;
                    sumWithIsFractional = (long)MainUtil.Round((double)sumWithIsFractional, 3);
                    if (fractional && sumWithIsFractional > SelectUser().Balance)
                    {
                        return await Task.FromResult(Redirect("/User/Order/Finalize?fractional=true"));
                    }
                    TblOrder addOrder = new TblOrder();
                    if (selectedDiscount.DiscountId == 0)
                    {
                        addOrder.DiscountId = null;
                    }
                    else
                    {
                        addOrder.DiscountId = selectedDiscount.DiscountId;
                    }
                    addOrder.Address = address;
                    addOrder.DateSubmited = DateTime.Now;
                    addOrder.FinalPrice = (int)selectedDiscount.Sum;
                    addOrder.IsPayed = false;
                    addOrder.Status = 0;
                    addOrder.PaymentStatus = (int)selectedDiscount.DiscountPrice;
                    addOrder.PostalCode = "0";
                    addOrder.SendPrice = selectedDiscount.PostPrice;
                    addOrder.SendStatus = selectedDiscount.PostPriceId;
                    addOrder.ClientId = SelectUser().ClientId;
                    addOrder.IsFractional = fractional;
                    addOrder.SentId = selectedDiscount.PostPriceId;
                    db.Order.Add(addOrder);
                    db.Save();
                    foreach (var item in sessions)
                    {
                        var product = db.Product.Get().Where(p => p.ProductId == item.ProductID).Select(p => new
                        {
                            p.PriceAfterDiscount,
                            p.PriceBeforeDiscount,
                        }).Single();
                        TblOrderDetail addOrderDetail = new TblOrderDetail();
                        addOrderDetail.Count = item.Count;
                        addOrderDetail.FinalOrderId = addOrder.OrdeId;
                        addOrderDetail.ProductId = item.ProductID;
                        addOrderDetail.ColorId = item.ColorID;
                        TblSpecialOffer offer = db.SpecialOffer.Get(i => i.ProductId == item.ProductID && i.ValidTill >= DateTime.Now).SingleOrDefault();
                        if (offer != null)
                        {
                            if (product.PriceAfterDiscount == 0)
                            {
                                addOrderDetail.Price = (int)product.PriceBeforeDiscount - (long)(Math.Floor((double)((int)product.PriceBeforeDiscount * offer.Discount / 100)));
                            }
                            else
                            {
                                addOrderDetail.Price = (int)product.PriceAfterDiscount - (long)(Math.Floor((double)((int)product.PriceAfterDiscount * offer.Discount / 100)));
                            }
                            addOrderDetail.Price = (long)MainUtil.Round((double)addOrderDetail.Price, 3);
                        }
                        else
                        {
                            if (product.PriceAfterDiscount == 0)
                            {
                                addOrderDetail.Price = (int)product.PriceBeforeDiscount;
                            }
                            else
                            {
                                addOrderDetail.Price = (int)product.PriceAfterDiscount;
                            }
                        }

                        db.OrderDetail.Add(addOrderDetail);

                    }
                    db.Save();
                    if (fractional && sumWithIsFractional <= SelectUser().Balance)
                    {
                        TblWallet addWallet = new TblWallet();
                        addWallet.Amount = (int)sumWithIsFractional;
                        addWallet.Date = DateTime.Now;
                        addWallet.Description = "پیش پرداخت خرید اقساطی";
                        addWallet.IsDeposit = false;
                        addWallet.IsFinaly = true;
                        addWallet.ClientId = SelectUser().ClientId;
                        addWallet.OrderId = addOrder.OrdeId;
                        db.Wallet.Add(addWallet);
                        //db.Wallet.Save();
                        TblClient selectedClient = db.Client.GetById(SelectUser().ClientId);
                        selectedClient.Balance -= sumWithIsFractional;
                        TblOrder selectedOrder = db.Order.GetById(addOrder.OrdeId);
                        selectedOrder.IsPayed = false;
                        selectedOrder.IsFractional = true;
                        selectedOrder.FractionalPartPayed = sumWithIsFractional;
                        selectedOrder.DateSubmited = DateTime.Now;
                        db.Client.Update(selectedClient);
                        db.Order.Update(selectedOrder);
                        db.Save();
                        DiscountVm emptydiscount = new DiscountVm();
                        HttpContext.Session.SetComplexData("Discount", emptydiscount);
                        List<ShopCartItem> emptyShopCartItem = new List<ShopCartItem>();
                        HttpContext.Session.SetComplexData("ShopCart", emptyShopCartItem);
                        HttpContext.Session.Clear();
                        int message = selectedOrder.OrdeId;
                        await Sms.SendSms(selectedClient.TellNo, message.ToString(), "GhasrMobileFractionalOrder");
                        return await Task.FromResult(Redirect("/User/Order/Fractional/" + addOrder.OrdeId));
                    }
                    else
                    {
                        if (selectedDiscount.Sum <= SelectUser().Balance)
                        {
                            TblWallet addWallet = new TblWallet();
                            addWallet.Amount = (int)selectedDiscount.Sum;
                            addWallet.Date = DateTime.Now;
                            addWallet.Description = "خرید";
                            addWallet.IsDeposit = false;
                            addWallet.IsFinaly = true;
                            addWallet.ClientId = SelectUser().ClientId;
                            addWallet.OrderId = addOrder.OrdeId;
                            db.Wallet.Add(addWallet);
                            //db.Wallet.Save();
                            TblClient selectedClient = db.Client.GetById(SelectUser().ClientId);
                            selectedClient.Balance -= selectedDiscount.Sum;
                            TblOrder selectedOrder = db.Order.GetById(addOrder.OrdeId);
                            selectedOrder.IsPayed = true;
                            selectedOrder.DateSubmited = DateTime.Now;
                            db.Client.Update(selectedClient);
                            db.Order.Update(selectedOrder);
                            db.Save();
                            DiscountVm emptydiscount = new DiscountVm();
                            HttpContext.Session.SetComplexData("Discount", emptydiscount);
                            List<ShopCartItem> emptyShopCartItem = new List<ShopCartItem>();
                            HttpContext.Session.SetComplexData("ShopCart", emptyShopCartItem);
                            HttpContext.Session.Clear();
                            int message = selectedOrder.OrdeId;
                            await Sms.SendSms(selectedClient.TellNo, message.ToString(), "GhasrMobileDoneSefaresh");
                            List<TblOrderDetail> list = db.OrderDetail.Get(i => i.FinalOrderId == addWallet.OrderId).ToList();
                            foreach (var item in list)
                            {
                                TblColor colors = db.Color.GetById(item.ColorId);
                                if (colors.Count > 0 && colors.Count >= item.Count)
                                {
                                    colors.Count -= item.Count;
                                    db.Color.Update(colors);
                                }
                            }
                            db.Save();
                            ViewBag.IsSucces = true;
                            return await Task.FromResult(View());
                        }
                        else
                        {
                            long SumBalance = selectedDiscount.SumWithDiscount;
                            if (SumBalance < 100)
                            {
                                SumBalance = 100;
                            }
                            ChargeWalletVm charge = new ChargeWalletVm();
                            int Amount = (int)SumBalance;
                            int OrderId = addOrder.OrdeId;
                            return Redirect("/User/Wallet/ChargeWallet?Amount=" + Amount + "&OrderId=" + OrderId);
                            // return Redirect("/User/Wallet/Charg?Amount=" + Amount + "&OrderId=" + OrderId);
                        }
                    }

                }
                return await Task.FromResult(View());
            }
            catch
            {
                return await Task.FromResult(Redirect("404.html"));
            }
        }

        public async Task<IActionResult> OnlinePayment(string RefId, string ResCode, string SaleOrderId, string SaleReferenceId)
        {
            try
            {

                BypassCertificateError();

                if (string.IsNullOrEmpty(SaleReferenceId))
                {
                    if (!string.IsNullOrEmpty(ResCode))
                    {
                        //ViewBag.message = Infrastructure.BMResult.BMResultText(ResCode);
                        ViewBag.message = "<div class='uk-alert uk-alert-danger'><h2>پرداخت با شکست مواجه شد</h2></div>";
                    }
                    else
                    {
                        ViewBag.message = "<div class='uk-alert uk-alert-danger'><h2>پرداخت با شکست مواجه شد</h2></div>";
                    }
                    //ImageState.ImageUrl = "~/Images/notaccept.png";
                }
                else
                {
                    try
                    {

                        Shaparak1.PaymentGatewayClient bpService = new Shaparak1.PaymentGatewayClient();
                        //شماره ترمینال اخذ شده از به پرداخت
                        string TerminalId = "5869122";
                        //نام کاربری اخذ شده از به پرداخت
                        string UserName = "gasremobile777";
                        //رمز عبور اخذ شده از به پرداخت
                        string UserPassword = "16986563";
                        var Vresult2 = await bpService.bpVerifyRequestAsync(long.Parse(TerminalId), UserName, UserPassword, long.Parse(SaleOrderId), long.Parse(SaleOrderId), long.Parse(SaleReferenceId));
                        string Vresult = Vresult2.Body.@return;
                        if (!string.IsNullOrEmpty(Vresult))
                        {

                            if (Vresult == "0")
                            {
                                //در اینجا پرداخت انجام شده است و عملیات مربوط به سایت انجام می گیرد
                                ViewBag.message = "<div class='uk-alert uk-alert-success'>" + "پرداخت با موفقیت انجام شد." + "<br/>" + "شناسه سفارش: " + SaleOrderId + "<br/>" + " شناسه مرجع تراکنش:" + SaleReferenceId + "<br/></div>";
                                //ViewBag.message = "پرداخت با موفقیت انجام شد." + "<br/>" + "شناسه سفارش: " + SaleOrderId + "<br/>" + " شناسه مرجع تراکنش:" + SaleReferenceId + "<br/>" + "رسید پرداخت:" + RefId;
                                //ImageState.ImageUrl = "~/Images/accept.png";
                                //اعلام پرداخت نهایی
                                var Sresult2 = await bpService.bpSettleRequestAsync(long.Parse(TerminalId), UserName, UserPassword, long.Parse(SaleOrderId), long.Parse(SaleOrderId), long.Parse(SaleReferenceId));
                                string Sresult = Sresult2.Body.@return.ToString();
                                if (Sresult != null)
                                {
                                    if (Sresult == "0" || Sresult == "45")
                                    {
                                        //تراکنش تایید و ستل شده است 
                                        var wallet = db.Wallet.GetById(Convert.ToInt32(SaleOrderId));
                                        wallet.IsFinaly = true;
                                        wallet.Date = DateTime.Now;
                                        db.Wallet.Update(wallet);
                                        TblClient selectedClient = db.Client.GetById(wallet.ClientId);
                                        selectedClient.Balance += wallet.Amount;
                                        db.Client.Update(selectedClient);
                                        db.Save();
                                        if (wallet.OrderId != null)
                                        {
                                            TblOrder selectedOrder = db.Order.GetById(wallet.OrderId);
                                            selectedOrder.IsPayed = true;
                                            selectedOrder.DateSubmited = DateTime.Now;
                                            db.Order.Update(selectedOrder);
                                            selectedClient.Balance -= selectedOrder.FinalPrice;
                                            db.Client.Update(selectedClient);
                                            db.Save();
                                            TblWallet addWallet = new TblWallet();
                                            addWallet.Amount = selectedOrder.FinalPrice;
                                            addWallet.Date = DateTime.Now;
                                            addWallet.Description = "خرید";
                                            addWallet.IsDeposit = false;
                                            addWallet.IsFinaly = true;
                                            addWallet.ClientId = wallet.ClientId;
                                            addWallet.OrderId = wallet.OrderId;
                                            db.Wallet.Add(addWallet);
                                            if (selectedOrder.DiscountId != null)
                                            {
                                                TblDiscount selectedDiscount = db.Discount.GetById(selectedOrder.DiscountId);
                                                selectedDiscount.Count--;
                                                db.Discount.Update(selectedDiscount);
                                            }
                                            List<TblOrderDetail> list = db.OrderDetail.Get(i => i.FinalOrderId == wallet.OrderId).ToList();
                                            foreach (var item in list)
                                            {
                                                TblColor colors = db.Color.GetById(item.ColorId);
                                                if (colors.Count > 0 && colors.Count >= item.Count)
                                                {
                                                    colors.Count -= item.Count;
                                                    db.Color.Update(colors);
                                                }
                                            }
                                            db.Save();
                                            DiscountVm emptydiscount = new DiscountVm();
                                            HttpContext.Session.SetComplexData("Discount", emptydiscount);
                                            List<ShopCartItem> emptyShopCartItem = new List<ShopCartItem>();
                                            HttpContext.Session.SetComplexData("ShopCart", emptyShopCartItem);
                                            HttpContext.Session.Clear();
                                            int message = selectedOrder.OrdeId;
                                            await Sms.SendSms(selectedClient.TellNo, message.ToString(), "GhasrMobileDoneSefaresh");
                                        }
                                    }
                                    else
                                    {

                                        //تراکنش تایید شده ولی ستل نشده است
                                    }
                                }
                            }
                            else
                            {

                                // ViewBag.message = Infrastructure.BMResult.BMResultText(Vresult);
                                //ImageState.ImageUrl = "~/Images/notaccept.png";
                            }

                        }
                        else
                        {
                            ViewBag.message = "<div class='uk-alert uk-alert-danger'><h2>پرداخت با شکست مواجه شد</h2></div>";

                            //ImageState.ImageUrl = "~/Images/notaccept.png";
                        }

                    }
                    catch
                    {
                        ViewBag.message = "<div class='uk-alert uk-alert-danger'><h2><br/> مشکلی در پرداخت به وجود آمده است ، در صورتیکه وجه پرداختی از حساب بانکی شما کسر شده است آن مبلغ به صورت خودکار برگشت داده خواهد شد</h2></div>";
                        //ImageState.ImageUrl = "~/Images/notaccept.png";
                    }
                }
                return await Task.FromResult(View());
            }
            catch
            {
                return await Task.FromResult(Redirect("404.html"));
            }

        }



        #region // تابع رفع پیغام های امنیتی
        void BypassCertificateError()
        {
            ServicePointManager.ServerCertificateValidationCallback +=
                delegate (
                    Object sender1,
                    X509Certificate certificate,
                    X509Chain chain,
                    SslPolicyErrors sslPolicyErrors)
                {
                    return true;
                };
        }
        #endregion









        //[Route("OnlinePayment/{id}")]
        //public async Task<IActionResult> OnlinePayment(int id)
        //{
        //    try
        //    {
        //        if (HttpContext.Request.Query["Status"] != "" &&
        //            HttpContext.Request.Query["Status"].ToString().ToLower() == "ok"
        //            && HttpContext.Request.Query["Authority"] != "")
        //        {
        //            string authority = HttpContext.Request.Query["Authority"];

        //            var wallet = db.Wallet.GetById(id);

        //            var payment = new ZarinpalSandbox.Payment(wallet.Amount);
        //            var res = payment.Verification(authority).Result;
        //            if (res.Status == 100)
        //            {
        //                ViewBag.code = res.RefId;
        //                ViewBag.IsSuccess = true;
        //                wallet.IsFinaly = true;
        //                db.Wallet.Update(wallet);
        //                TblClient selectedClient = db.Client.GetById(wallet.ClientId);
        //                selectedClient.Balance += wallet.Amount;
        //                db.Client.Update(selectedClient);

        //                db.Client.Save();
        //                if (wallet.OrderId != null)
        //                {
        //                    TblOrder selectedOrder = db.Order.GetById(wallet.OrderId);
        //                    selectedOrder.IsPayed = true;
        //                    db.Order.Update(selectedOrder);
        //                    selectedClient.Balance -= selectedOrder.FinalPrice;
        //                    db.Client.Update(selectedClient);
        //                    db.Client.Save();
        //                    TblWallet addWallet = new TblWallet();
        //                    addWallet.Amount = selectedOrder.FinalPrice;
        //                    addWallet.Date = DateTime.Now;
        //                    addWallet.Description = "خرید";
        //                    addWallet.IsDeposit = false;
        //                    addWallet.IsFinaly = true;
        //                    addWallet.ClientId = SelectUser().ClientId;
        //                    addWallet.OrderId = wallet.OrderId;
        //                    db.Wallet.Add(addWallet);
        //                    if (selectedOrder.DiscountId != null)
        //                    {
        //                        TblDiscount selectedDiscount = db.Discount.GetById(selectedOrder.DiscountId);
        //                        selectedDiscount.Count--;
        //                        db.Discount.Update(selectedDiscount);
        //                    }
        //                    List<TblOrderDetail> list = db.OrderDetail.Get(i => i.FinalOrderId == wallet.OrderId).ToList();
        //                    foreach (var item in list)
        //                    {
        //                        TblColor colors = db.Color.GetById(item.ColorId);
        //                        if (colors.Count > 0 && colors.Count >= item.Count)
        //                        {
        //                            colors.Count -= colors.Count;
        //                            db.Color.Update(colors);
        //                        }
        //                    }
        //                    db.Wallet.Save();
        //                    List<ShopCartItem> sessions = HttpContext.Session.GetComplexData<List<ShopCartItem>>("ShopCart");
        //                    DiscountVm discount = HttpContext.Session.GetComplexData<DiscountVm>("Discount");
        //                    int message = selectedOrder.OrdeId;
        //                    await Sms.SendSms(selectedClient.TellNo, message.ToString(), "GhasrMobileDoneSefaresh");
        //                    HttpContext.Session.Clear();
        //                }
        //            }
        //        }
        //        return await Task.FromResult(View());
        //    }
        //    catch
        //    {
        //        return await Task.FromResult(Redirect("404.html"));
        //    }
        //}

    }
}
