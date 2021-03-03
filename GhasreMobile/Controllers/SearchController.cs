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


namespace GhasreMobile.Controllers
{
    public class SearchController : Controller
    {
        private Core db = new Core();
        [Route("Search/{q?}/{name?}/{cat?}/{brand?}/{color?}/{colorIId?}/{minPrice?}/{maxPrice?}")]
        public IActionResult Index(string q = null, string name = null, int catId = 0, string cat = null, int brandId = 0, string brand = null, string color = null, int colorIId = 0, long minPrice = 0, long maxPrice = 0, bool available = false, bool discount = false)
        {
            ViewData["Title"] = q + name + cat + brand + color;
            ViewData["brandList"] = db.Brand.Get();
            ViewData["name"] = name;
            ViewData["brand"] = brand;
            ViewData["minPrice"] = minPrice;
            ViewData["maxPrice"] = maxPrice;
            List<TblProduct> list = db.Product.Get().ToList();
            if (q != null)
            {
                list = list.Where(i => i.SearchText.Contains(q)).ToList();
            }
            if (name != null)
            {
                list = list.Where(i => i.Name.Contains(name)).ToList();
            }
            if (cat != null)
            {
                list = list.Where(i => i.Catagory.Name.Contains(cat)).ToList();
            }
            if (catId != 0)
            {
                list = list.Where(i => i.CatagoryId == catId).ToList();
            }
            if (brand != null)
            {
                list = list.Where(i => i.Brand.Name.Contains(brand)).ToList();
            }
            if (brandId != 0)
            {
                list = list.Where(i => i.BrandId== brandId).ToList();
            }
            if (color != null)
            {
                list.AddRange(db.Color.Get(i => i.Name.Contains(color)).Select(i => i.Product).ToList());
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
            if (available != false)
            {
                list.AddRange(db.Color.Get(i => i.Count > 0).Select(i => i.Product).ToList());
            }
            if (discount != false)
            {
                list = list.Where(i => i.PriceAfterDiscount > 0).ToList();
            }
            return View(list.Distinct().OrderByDescending(i => i.TblColor.Sum(i => i.Count)));
        }

        //private static readonly string[] dbFake =
        //{
        //    "گوشی سامسونگ",
        //    "گوشی اپل",
        //    "لوازم جانبی اپل",
        //    "شیائومی",
        //    "اینگونه",
        //};

        [Route("q/{key}")]
        public IActionResult LayoutSearch(string key)
        {
            if (key.Length <= 2) return Ok("Invalid Key");
            var dbFake = db.Product.Get().Select(i => i.Name);
            return Json(dbFake.ToList().Where(i => i.Contains(key)));
        }

    }
}
