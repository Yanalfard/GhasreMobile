using DataLayer.Models;
using DataLayer.ViewModels;
using GhasreMobile.Utilities;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public IActionResult Index()
        {
            try
            {
                List<ShopCartItemVm> list = new List<ShopCartItemVm>();
                var sessions = HttpContext.Session.GetComplexData<List<ShopCartItem>>("ShopCart");
                if (sessions != null)
                {
                    List<ShopCartItem> listShop = (List<ShopCartItem>)sessions;

                    foreach (var item in listShop)
                    {
                        var product = db.Product.Get().Where(p => p.ProductId == item.ProductID).Select(p => new
                        {
                            p.MainImage,
                            p.Name,
                            p.PriceAfterDiscount,
                            p.PriceBeforeDiscount,
                            p.Brand,
                        }).Single();
                        list.Add(new ShopCartItemVm()
                        {
                            Count = item.Count,
                            ProductID = item.ProductID,
                            ColorID = item.ColorID,
                            ColorName = db.Color.GetById(item.ColorID).Name,
                            Name = product.Name,
                            ImageName = product.MainImage,
                            PriceAfterDiscount = product.PriceAfterDiscount,
                            PriceBeforeDiscount = product.PriceBeforeDiscount,
                            Brand = product.Brand.Name,
                            Sum = product.PriceAfterDiscount == 0 ? item.Count * product.PriceBeforeDiscount : product.PriceAfterDiscount * item.Count,
                        });
                    }
                }
                return View(list);
            }
            catch
            {
                return RedirectToAction("/ErrorPage/NotFound");
            }
        }

        public IActionResult UpDownCount(int id, int colorId, string command, string ReturnUrl = "/")
        {
            TblColor selectedProduct = db.Color.GetById(colorId);
            var listShop = HttpContext.Session.GetComplexData<List<ShopCartItem>>("ShopCart");
            var index = listShop.FindIndex(p => p.ColorID == colorId);
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
            return Redirect(ReturnUrl);
        }

        public IActionResult Comparison()
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

                return View(vm);
            }
            catch
            {
                return RedirectToAction("/ErrorPage/NotFound");
            }
        }

        public IActionResult Bookmarks()
        {
            return View();
        }

        public IActionResult Payment(string radio, string address)
        {
            List<ShopCartItem> sessions = HttpContext.Session.GetComplexData<List<ShopCartItem>>("ShopCart");
            DiscountVm selectedDiscount = HttpContext.Session.GetComplexData<DiscountVm>("Discount");
            if (selectedDiscount != null)
            {
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
                addOrder.FinalPrice = (int)selectedDiscount.SumWithDiscount;
                addOrder.IsPayed = false;
                addOrder.Status = 0;
                addOrder.PaymentStatus = 0;
                addOrder.PostalCode = "0";
                addOrder.SendPrice = 0;
                addOrder.SendStatus = 0;
                addOrder.ClientId = SelectUser().ClientId;
                db.Order.Add(addOrder);
                db.Order.Save();
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
                    if (product.PriceAfterDiscount == 0)
                    {
                        addOrderDetail.Price = (int)product.PriceBeforeDiscount;
                    }
                    else
                    {
                        addOrderDetail.Price = (int)product.PriceAfterDiscount;
                    }
                    db.OrderDetail.Add(addOrderDetail);

                }
                db.OrderDetail.Save();
                if (selectedDiscount.SumWithDiscount <= SelectUser().Balance)
                {
                    TblWallet addWallet = new TblWallet();
                    addWallet.Amount = (int)selectedDiscount.SumWithDiscount;
                    addWallet.Date = DateTime.Now;
                    addWallet.Description = "خرید";
                    addWallet.IsDeposit = false;
                    addWallet.IsFinaly = true;
                    addWallet.ClientId = SelectUser().ClientId;
                    addWallet.OrderId = addOrder.OrdeId;
                    db.Wallet.Add(addWallet);
                    //db.Wallet.Save();
                    TblClient selectedClient = db.Client.GetById(SelectUser().ClientId);
                    selectedClient.Balance -= selectedDiscount.SumWithDiscount;
                    TblOrder selectedOrder = db.Order.GetById(addOrder.OrdeId);
                    selectedOrder.IsPayed = true;
                    db.Client.Update(selectedClient);
                    db.Order.Update(selectedOrder);
                    db.Client.Save();
                    return View();
                }
                else
                {
                    long SumBalance = selectedDiscount.SumWithDiscount - SelectUser().Balance;
                    if (SumBalance < 1000)
                    {
                        SumBalance = 1000;
                    }
                    ChargeWalletVm charge = new ChargeWalletVm();
                    int Amount = (int)SumBalance;
                    int OrderId = addOrder.OrdeId;
                    return Redirect("/User/Wallet/ChargeWallet?Amount=" + Amount + "&OrderId=" + OrderId);
                    //return Redirect("/User/Wallet/ChargeWallet/" + charge);
                }
            }
            return View();
        }

        [Route("OnlinePayment/{id}")]
        public IActionResult OnlinePayment(int id)
        {
            if (HttpContext.Request.Query["Status"] != "" &&
                HttpContext.Request.Query["Status"].ToString().ToLower() == "ok"
                && HttpContext.Request.Query["Authority"] != "")
            {
                string authority = HttpContext.Request.Query["Authority"];

                var wallet = db.Wallet.GetById(id);

                var payment = new ZarinpalSandbox.Payment(wallet.Amount);
                var res = payment.Verification(authority).Result;
                if (res.Status == 100)
                {
                    ViewBag.code = res.RefId;
                    ViewBag.IsSuccess = true;
                    wallet.IsFinaly = true;
                    db.Wallet.Update(wallet);
                    TblClient selectedClient = db.Client.GetById(wallet.ClientId);
                    selectedClient.Balance += wallet.Amount;
                    db.Client.Update(selectedClient);
                    db.Client.Save();
                    if (wallet.OrderId != null)
                    {
                        TblOrder selectedOrder = db.Order.GetById(wallet.OrderId);
                        selectedOrder.IsPayed = true;
                        db.Order.Update(selectedOrder);
                        selectedClient.Balance -= selectedOrder.FinalPrice;
                        db.Client.Update(selectedClient);
                        db.Client.Save();
                        TblWallet addWallet = new TblWallet();
                        addWallet.Amount = selectedOrder.FinalPrice;
                        addWallet.Date = DateTime.Now;
                        addWallet.Description = "خرید";
                        addWallet.IsDeposit = false;
                        addWallet.IsFinaly = true;
                        addWallet.ClientId = SelectUser().ClientId;
                        addWallet.OrderId = wallet.OrderId;
                        db.Wallet.Add(addWallet);
                        db.Wallet.Save();
                    }
                }

            }

            return View();
        }





    }
}
