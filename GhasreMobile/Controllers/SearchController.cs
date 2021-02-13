using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GhasreMobile.Controllers
{
    public class SearchController : Controller
    {
        public IActionResult Index()
        {
            return View();
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
