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
    [PermissionChecker("user,employee,admin")]
    public class CartController : Controller
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
                            shop.Sum = shop.Sum - (long)(Math.Floor((double)(shop.Sum * offer.Discount / 100)));
                        };
                        list.Add(shop);
                    }
                }

                return await Task.FromResult(View(list));
            }
            catch
            {
                return await Task.FromResult(Redirect("404.html"));
            }
        }

        [Route("User/Comparison")]
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

        [Route("User/Bookmarks")]
        public async Task<IActionResult> Bookmarks()
        {
            try
            {
                List<TblProduct> list = db.BookMark.Get(i => i.ClientId == SelectUser().ClientId).Select(i => i.Product).ToList();
                return await Task.FromResult(View(list));
            }
            catch
            {
                return await Task.FromResult(Redirect("404.html"));
            }
        }


    }
}
