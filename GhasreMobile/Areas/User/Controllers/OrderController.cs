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
    public class OrderController : Controller
    {
        // History
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
                return await Task.FromResult(View(db.Order.Get(i => i.IsPayed && i.IsFractional == false).OrderByDescending(i => i.DateSubmited)));
            }
            catch
            {
                return await Task.FromResult(Redirect("404.html"));
            }
        }

        public async Task<IActionResult> Fractionals()
        {
            try
            {
                return await Task.FromResult(View(db.Order.Get(i => i.IsFractional == true).OrderByDescending(i => i.DateSubmited)));
            }
            catch
            {
                return await Task.FromResult(Redirect("404.html"));
            }
        }

        // Make order
        public async Task<IActionResult> Finalize(string type = "")
        {
            try
            {
                ViewBag.FinalTextKharid = db.Config.Get(i => i.Key == "FinalTextKharid").SingleOrDefault().Value;
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
                            p.IsFractional
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
                else
                {
                    return await Task.FromResult(Redirect("/"));
                }
                var selectedDiscount = HttpContext.Session.GetComplexData<DiscountVm>("Discount");
                DiscountVm discount = new DiscountVm();
                long sumList = (long)list.Sum(i => i.Sum);
                discount.Balance = SelectUser().Balance;
                int SagfePost = Convert.ToInt32(db.Config.Get(i => i.Key == "SagfePost").Single().Value);

                if (selectedDiscount != null)
                {
                    discount.PostPrice = selectedDiscount.PostPrice;
                    discount.Discount = selectedDiscount.Discount;
                    discount.DiscountPrice = selectedDiscount.DiscountPrice;
                    discount.DiscountId = selectedDiscount.DiscountId;
                    discount.Name = selectedDiscount.Name;
                    discount.SumWithDiscount = selectedDiscount.SumWithDiscount;
                    discount.Sum = selectedDiscount.Sum;
                    discount.PostPriceId = selectedDiscount.PostPriceId;
                    discount.SagfePost = selectedDiscount.SagfePost;
                    if (selectedDiscount.Discount != 0)
                    {
                        discount.Sum = sumList - (long)(Math.Floor((double)(sumList * selectedDiscount.Discount / 100)));
                        discount.SumWithDiscount = sumList - (long)(Math.Floor((double)(sumList * selectedDiscount.Discount / 100)));
                        discount.DiscountPrice = sumList - discount.Sum;
                    }
                    else
                    {
                        discount.Sum = sumList;
                        discount.SumWithDiscount = sumList;
                    }
                }
                else
                {
                    discount.Sum = sumList;
                    discount.SumWithDiscount = sumList;
                    TblPostOption selectPost = db.PostOption.Get().FirstOrDefault();
                    if (selectPost != null)
                    {
                        discount.PostPrice = (int)selectPost.Price;
                        discount.SagfePost = SagfePost;
                        discount.PostPriceId = selectPost.PostOptionId;
                    }
                }
                discount.SumWithDiscount -= SelectUser().Balance;
                if (discount.SumWithDiscount <= 0)
                {
                    discount.SumWithDiscount = 0;
                }
                if (SagfePost >= discount.Sum)
                {
                    discount.Sum += discount.PostPrice;
                    discount.SumWithDiscount += discount.PostPrice;
                    TblPostOption selectPost = db.PostOption.GetById(discount.PostPriceId);
                    discount.PostPrice = (int)selectPost.Price;
                }
                else
                {
                    discount.PostPrice = 0;
                }

                if (list.Any(i => i.IsFractional == false))
                {
                    discount.IsFractional = false;
                }
                else
                {
                    discount.IsFractional = true;
                }
                HttpContext.Session.SetComplexData("Discount", discount);
                ViewBag.discountDarsad = discount.Discount;
                return await Task.FromResult(View(list));
            }
            catch
            {
                return await Task.FromResult(Redirect("404.html"));
            }
        }


        [HttpPost]
        public async Task<IActionResult> CheckDiscount(DiscountVm discoun)
        {
            try
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
                            //int SagfePost = Convert.ToInt32(db.Config.Get(i => i.Key == "SagfePost").Single().Value);

                            selectedDiscount.Discount = getDiscount.Discount;
                            selectedDiscount.Name = getDiscount.Name;
                            selectedDiscount.DiscountId = getDiscount.DiscountId;
                            // TblPostOption selectPost = db.PostOption.Get().First();
                            selectedDiscount.PostPrice = selectedDiscount.PostPrice;
                            selectedDiscount.SagfePost = selectedDiscount.SagfePost;
                            selectedDiscount.PostPriceId = selectedDiscount.PostPriceId;
                            HttpContext.Session.SetComplexData("Discount", selectedDiscount);
                        }
                    }
                    else
                    {
                        type = "3";
                    }
                    return await Task.FromResult(Redirect("/User/Order/Finalize?type=" + type.ToString()));
                }
                return await Task.FromResult(PartialView(discoun));
            }
            catch
            {
                return await Task.FromResult(Redirect("404.html"));
            }
        }



        public async Task<IActionResult> FinalVerfity()
        {
            try
            {
                ViewBag.ListPostOption = db.PostOption.Get().ToList();
                DiscountVm selectedDiscount = HttpContext.Session.GetComplexData<DiscountVm>("Discount");
                return await Task.FromResult(PartialView(selectedDiscount));
            }
            catch
            {
                return await Task.FromResult(Redirect("404.html"));
            }
        }
        public async Task<IActionResult> SetPost(int id)
        {
            try
            {
                TblPostOption selectPost = db.PostOption.GetById(id);
                if (selectPost != null)
                {
                    var selectedDiscount = HttpContext.Session.GetComplexData<DiscountVm>("Discount");
                    selectedDiscount.PostPrice = (int)selectPost.Price;
                    selectedDiscount.PostPriceId = selectPost.PostOptionId;
                    HttpContext.Session.SetComplexData("Discount", selectedDiscount);
                }
                return await Task.FromResult(Redirect("/User/Order/Finalize"));
            }
            catch
            {
                return await Task.FromResult(Redirect("404.html"));
            }
        }

        public async Task<IActionResult> OnlineOrder(string type = "")
        {
            try
            {
                ViewBag.OnlineOrder = type;
                return await Task.FromResult(View());
            }
            catch
            {
                return await Task.FromResult(Redirect("404.html"));
            }
        }
        [HttpPost]
        public async Task<IActionResult> OnlineOrder(OnlineOrderVm online)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TblOnlineOrder AddOnlineOrder = new TblOnlineOrder();
                    AddOnlineOrder.ClientId = SelectUser().ClientId;
                    AddOnlineOrder.Body = online.Body;
                    AddOnlineOrder.DateSubmited = DateTime.Now;
                    AddOnlineOrder.IsRead = false;
                    AddOnlineOrder.Title = online.Title;
                    db.OnlineOrder.Add(AddOnlineOrder);
                    db.OnlineOrder.Save();
                    var type = "true";
                    return Redirect("/User/Order/OnlineOrder?type=" + type.ToString());
                }
                return await Task.FromResult(View(online));
            }
            catch
            {
                return await Task.FromResult(Redirect("404.html"));
            }
        }

        public async Task<IActionResult> Fractional(int id)
        {
            try
            {
                HttpContext.Session.Clear();
                return await Task.FromResult(View(db.Order.GetById(id)));
            }
            catch
            {
                return await Task.FromResult(Redirect("404.html"));
            }
        }

    }
}
