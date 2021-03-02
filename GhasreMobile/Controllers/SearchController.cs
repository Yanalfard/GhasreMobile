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
        public IActionResult Index()
        {
            return View(db.Product.Get());
        }

        private static readonly string[] dbFake =
        {
            "گوشی سامسونگ",
            "گوشی اپل",
            "لوازم جانبی اپل",
            "شیائومی",
            "اینگونه",
        };

        [Route("q/{key}")]
        public IActionResult LayoutSearch(string key)
        {
            if (key.Length <= 2) return Ok("Invalid Key");

            return Json(dbFake.ToList().Where(i => i.Contains(key)));
        }

    }
}
