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
    }
}
