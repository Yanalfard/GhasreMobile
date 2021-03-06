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
        TblClient SelectUser()
        {
            int userId = Convert.ToInt32(User.Claims.First().Value);
            TblClient selectUser = db.Client.GetById(userId);
            return selectUser;
        }
        public IActionResult Index()
        {
            return View(db.Order.Get(i=>i.IsPayed));
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
                    TblPostOption selectPost = db.PostOption.Get().First();
                    discount.PostPrice = (int)selectPost.Price;
                    discount.SagfePost = SagfePost;
                    discount.PostPriceId = selectPost.PostOptionId;
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
                }
                else
                {
                    discount.PostPrice = 0;
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
                return Redirect("/User/Order/Finalize?type=" + type.ToString());
            }
            return PartialView(discoun);

        }



        public IActionResult FinalVerfity()
        {
            ViewBag.ListPostOption = db.PostOption.Get().ToList();
            DiscountVm selectedDiscount = HttpContext.Session.GetComplexData<DiscountVm>("Discount");
            return PartialView(selectedDiscount);
        }
        public IActionResult SetPost(int id)
        {
            TblPostOption selectPost = db.PostOption.GetById(id);
            if (selectPost != null)
            {
                var selectedDiscount = HttpContext.Session.GetComplexData<DiscountVm>("Discount");
                //int SagfePost = Convert.ToInt32(db.Config.Get(i => i.Key == "SagfePost").Single().Value);
                //selectedDiscount.Discount = getDiscount.Discount;
                //selectedDiscount.Name = getDiscount.Name;
                //selectedDiscount.DiscountId = getDiscount.DiscountId;
                selectedDiscount.PostPrice = (int)selectPost.Price;
                //  selectedDiscount.SagfePost = selectedDiscount.SagfePost;
                selectedDiscount.PostPriceId = selectPost.PostOptionId;
                HttpContext.Session.SetComplexData("Discount", selectedDiscount);
            }
            return Redirect("/User/Order/Finalize");
        }

    }
}
