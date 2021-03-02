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
                        }).Single();
                        list.Add(new ShopCartItemVm()
                        {
                            Count = item.Count,
                            ProductID = item.ProductID,
                            ColorID = item.ColorID,
                            Name = product.Name,
                            ImageName = product.MainImage,
                            PriceAfterDiscount = product.PriceAfterDiscount,
                            PriceBeforeDiscount = product.PriceBeforeDiscount,
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

        public IActionResult Comparison()
        {
            return View();
        }

        public IActionResult Bookmarks()
        {
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
