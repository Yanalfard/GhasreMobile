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
                addOrder.DiscountId = selectedDiscount.DiscountId;
                addOrder.Address = address;
                addOrder.DateSubmited = DateTime.Now;
                addOrder.FinalPrice = (int)selectedDiscount.SumWithDiscount;
                addOrder.IsPayed = false;
                addOrder.Status = 0;
                addOrder.PaymentStatus = 0;
                addOrder.PostalCode = "0";
                addOrder.SendPrice = 0;
                addOrder.SendStatus = 0;
                addOrder.Status = 0;
                db.Order.Add(addOrder);
                db.Order.Save();
                foreach (var item in sessions)
                {
                    TblOrderDetail addOrderDetail = new TblOrderDetail();
                    addOrderDetail.ClientId= SelectUser().ClientId;
                    addOrderDetail.Count = item.Count;
                    addOrderDetail.FinalOrderId = addOrder.OrdeId;
                    addOrderDetail.ProductId = item.ProductID;
                    //addOrderDetail.Price = item.;
                    db.OrderDetail.Add(addOrderDetail);
                    db.OrderDetail.Save();

                }
                if (selectedDiscount.SumWithDiscount <= SelectUser().Balance)
                {
                    TblWallet addWallet = new TblWallet();
                    addWallet.Amount = (int)selectedDiscount.SumWithDiscount;
                    addWallet.Date = DateTime.Now;
                    addWallet.Description = "خرید";
                    addWallet.IsDeposit = false;
                    addWallet.IsFinaly = true;
                    addWallet.ClientId = SelectUser().ClientId;
                    db.Wallet.Add(addWallet);
                    db.Wallet.Save();
                    TblClient selectedClient=db.Client.GetById(SelectUser().ClientId);
                    selectedClient.Balance -= selectedDiscount.SumWithDiscount;
                    TblOrder selectedOrder = db.Order.GetById(addOrder.OrdeId);
                    selectedOrder.IsPayed = true;
                    db.Client.Update(selectedClient);
                    db.Order.Update(selectedOrder);
                    db.Client.Save();
                }
                else
                {
                    long SumBalance = selectedDiscount.SumWithDiscount - SelectUser().Balance;
                    if (SumBalance < 10000)
                    {
                        SumBalance = 10000;
                    }
                    //////چک شود که بعد شارژ کیف پول جنس رو تایید کند
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
                    db.Wallet.Save();
                    TblClient selectedClient = db.Client.GetById(wallet.ClientId);
                    selectedClient.Balance += wallet.Amount;
                    db.Client.Update(selectedClient);
                    db.Client.Save();
                }

            }

            return View();
        }





        //[HttpPost]
        //[PermissionChecker("user,employee,admin")]
        //public ActionResult Payment()
        //{
        //    try
        //    {
        //        int userId = _users.GetAllUsers().Single(u => u.UserName == User.Identity.Name).UserID;
        //        int sumPrice = Convert.ToInt32(PriceSum);
        //        int Dis;
        //        Dis = Convert.ToInt32(DiscountId);
        //        Orders order = new Orders();
        //        if (DiscountId != "0")
        //        {
        //            order.UserID = userId;
        //            order.Date = DateTime.Now;
        //            order.IsFinaly = false;
        //            order.Sum = sumPrice;
        //            order.DiscountId = Dis;
        //        }
        //        else
        //        {
        //            order.UserID = userId;
        //            order.Date = DateTime.Now;
        //            order.IsFinaly = false;
        //            order.Sum = sumPrice;
        //        }
        //        _orders.InsertOrder(order);
        //        var listDetails = getListOrder();
        //        foreach (var item in listDetails)
        //        {
        //            _orderDetails.InsertOrderDetail(new OrderDetails()
        //            {
        //                Count = item.Count,
        //                OrderID = order.OrderID,
        //                Price = item.Price,
        //                ProductID = item.ProductID,
        //            });
        //        }

        //        if (DiscountId != "0")
        //        {
        //            int disId = Convert.ToInt32(DiscountId);
        //            Discount discountToUpdate = _discount.GetDiscountById(disId);
        //            discountToUpdate.Count--;
        //            _discount.Save();
        //        }
        //        System.Net.ServicePointManager.Expect100Continue = false;
        //        Zarin.PaymentGatewayImplementationServicePortTypeClient zp = new Zarin.PaymentGatewayImplementationServicePortTypeClient();
        //        string Authority;

        //        int Status = zp.PaymentRequest("5f648351-94a0-4b6d-ab96-3eef0d58a8b5", sumPrice, "نیو خرید ", "info@newkharid.com", "09339634557", ConfigurationManager.AppSettings["MyDomain"] + "/ShopCart/Verify/" + order.OrderID, out Authority);
        //        if (Status == 100)
        //        {
        //            Response.Redirect("https://www.zarinpal.com/pg/StartPay/" + Authority);

        //            ////test
        //            //return Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + Authority);
        //        }
        //        else
        //        {
        //            ViewBag.Error = "Error : " + Status;
        //            return RedirectToAction("Verify");

        //        }
        //        //TODO : Online Payment
        //        return null;
        //    }
        //    catch
        //    {
        //        return RedirectToAction("/ErrorPage/NotFound");
        //    }
        //}

        //public ActionResult Verify(int id)
        //{
        //    try
        //    {

        //        Orders order = _orders.GetOrdersById(id);
        //        if (Request.QueryString["Status"] != "" && Request.QueryString["Status"] != null && Request.QueryString["Authority"] != "" && Request.QueryString["Authority"] != null)
        //        {
        //            if (Request.QueryString["Status"].ToString().Equals("OK"))
        //            {
        //                int Amount = order.Sum;
        //                long RefID;
        //                System.Net.ServicePointManager.Expect100Continue = false;
        //                Zarin.PaymentGatewayImplementationServicePortTypeClient zp = new Zarin.PaymentGatewayImplementationServicePortTypeClient();

        //                int Status = zp.PaymentVerification("a282a431-19d8-43ee-ae50-e3d056519667", Request.QueryString["Authority"].ToString(), Amount, out RefID);
        //                if (Status == 100)
        //                {
        //                    order.IsFinaly = true;
        //                    ViewBag.IsSuccess = true;
        //                    ViewBag.RefId = RefID;
        //                    _orders.Save();
        //                    // Response.Write("Success!! RefId: " + RefID);
        //                    List<ShopCartItem> cart = Session["ShopCart"] as List<ShopCartItem>;
        //                    cart.Clear();
        //                    return Redirect("/UserPanel/Home/FactorView/" + id + "?FinalFactor=" + RefID);
        //                }
        //                else
        //                {
        //                    ViewBag.Status = Status;
        //                }
        //            }
        //            else
        //            {
        //                Response.Write("Error! Authority: " + Request.QueryString["Authority"].ToString() + " Status: " + Request.QueryString["Status"].ToString());
        //            }
        //        }
        //        else
        //        {
        //            Response.Write("Invalid Input");
        //        }
        //        return View();
        //    }
        //    catch
        //    {
        //        return RedirectToAction("/ErrorPage/NotFound");
        //    }

        //}
    }
}
