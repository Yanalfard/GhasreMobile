using DataLayer.Models;
using DataLayer.ViewModels;
using GhasreMobile.Utilities;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GhasreMobile.Areas.User.Controllers
{
    [Area("User")]
    public class OrderController : Controller
    {
        // History
        Core db = new Core();
        public IActionResult Index()
        {
            return View();
        }

        // Make order
        public IActionResult Finalize()
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

    }
}
