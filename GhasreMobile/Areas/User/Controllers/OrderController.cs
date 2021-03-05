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
        public IActionResult Finalize(string type = "")
        {
            try
            {
                ViewBag.typeDiscount = type;
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
                var selectedDiscount = HttpContext.Session.GetComplexData<DiscountVm>("Discount");
                DiscountVm discount = new DiscountVm();
                long sumList = (long)list.Sum(i => i.Sum);
                if (selectedDiscount != null)
                {
                    discount.Discount = selectedDiscount.Discount;
                    discount.DiscountId = selectedDiscount.DiscountId;
                    discount.Name = selectedDiscount.Name;
                    discount.Sum = selectedDiscount.Sum;
                    if (selectedDiscount.Discount != 0)
                    {
                        discount.Sum = sumList - (long)(Math.Floor((double)(sumList * selectedDiscount.Discount / 100)));
                    }
                    else
                    {
                        discount.Sum = sumList;
                    }
                }
                else
                {
                    discount.Sum = sumList;
                }
                HttpContext.Session.SetComplexData("Discount", discount);
                return View(list);
            }
            catch
            {
                return RedirectToAction("/ErrorPage/NotFound");
            }
        }


        [HttpPost]
        public IActionResult CheckDiscount(DiscountVm discoun)
        {
            if (ModelState.IsValid)
            {
                var type = "0";
                bool isDiscount = db.Discount.Get(i => i.Name.Contains(discoun.Name)).Any();
                if (isDiscount)
                {
                    TblDiscount getDiscount = db.Discount.Get(i => i.Name.Contains(discoun.Name)).Single();
                    if (getDiscount.ValidTill < DateTime.Now)
                    {
                        type = "1";
                    }
                    else if (getDiscount.Count <= 0)
                    {
                        type = "2";
                    }
                    else
                    {
                        type = "Success";
                        var selectedDiscount = HttpContext.Session.GetComplexData<DiscountVm>("Discount");
                        selectedDiscount.Discount = getDiscount.Discount;
                        selectedDiscount.Name = getDiscount.Name;
                        HttpContext.Session.SetComplexData("Discount", selectedDiscount);
                    }
                }
                else
                {
                    type = "3";
                }
                return Redirect("/User/Order/Finalize?type=" + type.ToString());
            }
            return PartialView(discoun);

        }

    }
}
