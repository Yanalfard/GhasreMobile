using DataLayer.Models;
using DataLayer.Security;
using DataLayer.Utilities;
using DataLayer.ViewModels;
using GhasreMobile.Utilities;
using GoogleReCaptcha.V3.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Session;
using System.Globalization;

namespace GhasreMobile.Controllers
{
    public class SearchController : Controller
    {
        readonly static int GlobalTake = 3;

        private Core db = new Core();
        [Route("Search/{q?}/{name?}/{cat?}/{brand?}/{color?}/{colorIId?}/{minPrice?}/{maxPrice?}")]
        public async Task<IActionResult> Index(int pageId = 1, string q = null, string name = null, int catId = 0, string cat = null, int brandId = 0, string brand = null, string color = null, string maxDate = null, string minDate = null, int colorIId = 0, long minPrice = 0, long maxPrice = 0, string available = null, string discount = null, string IsFractional = null)
        {
            try
            {
                ViewData["Title"] = q + name + cat + brand + color;
                ViewData["brandList"] = db.Brand.Get();
                ViewData["pageId"] = pageId;
                ViewData["name"] = name;
                ViewData["brand"] = brand;
                ViewData["minPrice"] = minPrice;
                ViewData["maxPrice"] = maxPrice;
                ViewData["maxDate"] = maxDate;
                ViewData["minDate"] = minDate;
                ViewData["IsFractional"] = IsFractional == "on" ? true : false;
                ViewData["discount"] = discount == "on" ? true : false;
                ViewData["available"] = available == "on" ? true : false;

                List<TblProduct> list = db.Product.Get(i => i.IsDeleted == false).ToList();
                if (q != null)
                {
                    list = list.Where(i => i.SearchText.ToLower().Contains(q.ToLower()) || i.Name.ToLower().Contains(q.ToLower())).ToList();
                }
                if (name != null)
                {
                    list = list.Where(i => i.Name.ToLower().Contains(name.ToLower())).ToList();
                }
                if (cat != null)
                {
                    list = list.Where(i => i.Catagory.Name.ToLower().Contains(cat.ToLower())).ToList();
                }
                if (catId != 0)
                {
                    list = list.Where(i => i.CatagoryId == catId).ToList();
                }
                if (brand != null)
                {
                    list = list.Where(i => i.Brand.Name.ToLower().Contains(brand.ToLower())).ToList();
                }
                if (brandId != 0)
                {
                    list = list.Where(i => i.BrandId == brandId).ToList();
                }
                if (color != null)
                {
                    list.AddRange(db.Color.Get(i => i.Name.ToLower().Contains(color)).Select(i => i.Product).ToList());
                }
                if (colorIId != 0)
                {
                    list.AddRange(db.Color.Get(i => i.ColorId == colorIId).Select(i => i.Product).ToList());
                }
                if (minPrice != 0)
                {
                    list = list.Where(i => i.PriceAfterDiscount > minPrice || i.PriceBeforeDiscount > minPrice).ToList();
                }
                if (maxPrice != 0)
                {
                    list = list.Where(i => i.PriceAfterDiscount < maxPrice || i.PriceBeforeDiscount < maxPrice).ToList();
                }
                if (maxDate != null)
                {
                    PersianCalendar pc = new PersianCalendar();
                    string[] dates = maxDate.Split('/');
                    if (dates.Length == 3)
                    {
                        try
                        {
                            DateTime dte = pc.ToDateTime(Convert.ToInt32(dates[0]), Convert.ToInt32(dates[1]), Convert.ToInt32(dates[2]), 0, 0, 0, 0);
                            list = list.Where(i => i.DateCreated <= dte).ToList();
                        }
                        catch (FormatException)
                        {
                            ViewData["maxDate"] = "";
                        }
                    }
                    else
                    {
                        ViewData["maxDate"] = "";
                    }
                }
                if (minDate != null)
                {
                    PersianCalendar pc = new PersianCalendar();
                    string[] dates = minDate.Split('/');
                    if (dates.Length == 3)
                    {
                        try
                        {
                            DateTime dte = pc.ToDateTime(Convert.ToInt32(dates[0]), Convert.ToInt32(dates[1]), Convert.ToInt32(dates[2]), 0, 0, 0, 0);
                            list = list.Where(i => i.DateCreated >= dte).ToList();
                        }
                        catch (FormatException)
                        {
                            ViewData["minDate"] = "";
                        }
                    }
                    else
                    {
                        ViewData["minDate"] = "";

                    }
                }
                if (available != null)
                {
                    list.AddRange(db.Color.Get(i => i.Count > 0).Select(i => i.Product).ToList());
                }
                if (discount != null)
                {
                    list = list.Where(i => i.PriceAfterDiscount > 0).ToList();
                }
                if (IsFractional != null)
                {
                    list = list.Where(i => i.IsFractional).ToList();
                }
                //Pagging
                int take = GlobalTake;
                int skip = (pageId - 1) * take;
                ViewBag.PageCount = list.Count() / take;
                return await Task.FromResult(View(list.Distinct().OrderByDescending(i => i.TblColor.Sum(i => i.Count)).Skip(skip).Take(take)));
            }
            catch
            {
                return await Task.FromResult(Redirect("404.html"));
            }
        }

        [Route("SearchProduct/{q?}/{name?}/{cat?}/{brand?}/{color?}/{colorIId?}/{minPrice?}/{maxPrice?}")]
        public async Task<IActionResult> SearchProduct(int pageId = 1, string q = null, string name = null, int catId = 0, string cat = null, int brandId = 0, string brand = null, string color = null, string maxDate = null, string minDate = null, int colorIId = 0, long minPrice = 0, long maxPrice = 0, string available = null, string discount = null, string IsFractional = null)
        {
            try
            {
                ViewData["Title"] = q + name + cat + brand + color;
                ViewData["brandList"] = db.Brand.Get();
                ViewData["name"] = name;
                ViewData["brand"] = brand;
                ViewData["minPrice"] = minPrice;
                ViewData["maxPrice"] = maxPrice;
                ViewData["maxDate"] = maxDate;
                ViewData["minDate"] = minDate;
                ViewData["IsFractional"] = IsFractional == "on" ? true : false;
                ViewData["discount"] = discount == "on" ? true : false;
                ViewData["available"] = available == "on" ? true : false;

                List<TblProduct> list = db.Product.Get(i => i.IsDeleted == false).ToList();
                if (q != null)
                {
                    list = list.Where(i => i.SearchText.ToLower().Contains(q.ToLower()) || i.Name.ToLower().Contains(q.ToLower())).ToList();
                }
                if (name != null)
                {
                    list = list.Where(i => i.Name.ToLower().Contains(name.ToLower())).ToList();
                }
                if (cat != null)
                {
                    list = list.Where(i => i.Catagory.Name.ToLower().Contains(cat.ToLower())).ToList();
                }
                if (catId != 0)
                {
                    list = list.Where(i => i.CatagoryId == catId).ToList();
                }
                if (brand != null)
                {
                    list = list.Where(i => i.Brand.Name.ToLower().Contains(brand.ToLower())).ToList();
                }
                if (brandId != 0)
                {
                    list = list.Where(i => i.BrandId == brandId).ToList();
                }
                if (color != null)
                {
                    list.AddRange(db.Color.Get(i => i.Name.ToLower().Contains(color)).Select(i => i.Product).ToList());
                }
                if (colorIId != 0)
                {
                    list.AddRange(db.Color.Get(i => i.ColorId == colorIId).Select(i => i.Product).ToList());
                }
                if (minPrice != 0)
                {
                    list = list.Where(i => i.PriceAfterDiscount > minPrice || i.PriceBeforeDiscount > minPrice).ToList();
                }
                if (maxPrice != 0)
                {
                    list = list.Where(i => i.PriceAfterDiscount < maxPrice || i.PriceBeforeDiscount < maxPrice).ToList();
                }
                if (maxDate != null)
                {
                    PersianCalendar pc = new PersianCalendar();
                    string[] dates = maxDate.Split('/');
                    if (dates.Length == 3)
                    {
                        try
                        {
                            DateTime dte = pc.ToDateTime(Convert.ToInt32(dates[0]), Convert.ToInt32(dates[1]), Convert.ToInt32(dates[2]), 0, 0, 0, 0);
                            list = list.Where(i => i.DateCreated <= dte).ToList();
                        }
                        catch (FormatException)
                        {
                            ViewData["maxDate"] = "";
                        }
                    }
                    else
                    {
                        ViewData["maxDate"] = "";
                    }
                }
                if (minDate != null)
                {
                    PersianCalendar pc = new PersianCalendar();
                    string[] dates = minDate.Split('/');
                    if (dates.Length == 3)
                    {
                        try
                        {
                            DateTime dte = pc.ToDateTime(Convert.ToInt32(dates[0]), Convert.ToInt32(dates[1]), Convert.ToInt32(dates[2]), 0, 0, 0, 0);
                            list = list.Where(i => i.DateCreated >= dte).ToList();
                        }
                        catch (FormatException)
                        {
                            ViewData["minDate"] = "";
                        }
                    }
                    else
                    {
                        ViewData["minDate"] = "";

                    }
                }
                if (available != null)
                {
                    list.AddRange(db.Color.Get(i => i.Count > 0).Select(i => i.Product).ToList());
                }
                if (discount != null)
                {
                    list = list.Where(i => i.PriceAfterDiscount > 0).ToList();
                }
                if (IsFractional != null)
                {
                    list = list.Where(i => i.IsFractional).ToList();
                }
                //Pagging
                int take = GlobalTake;
                int skip = (pageId - 1) * take;
                ViewBag.PageCount = list.Count() / take;
                List<TblProduct> listProduct = list.Distinct().OrderByDescending(i => i.TblColor.Sum(i => i.Count)).Skip(skip).Take(take).ToList();
                if (listProduct.Count == 0)
                {
                    return await Task.FromResult(Json(false));
                }
                return await Task.FromResult(PartialView(listProduct));
            }
            catch
            {
                return await Task.FromResult(Redirect("404.html"));
            }
        }



        [Route("q/{key}")]
        public IActionResult LayoutSearch(string key)
        {
            if (key.Length <= 2) return Ok("Invalid Key");
            var dbFake = db.Product.Get().Select(i => i.Name.ToLower());
            return Json(dbFake.ToList().Where(i => i.Contains(key)));
        }

    }
}
