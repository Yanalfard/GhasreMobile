﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GhasreMobile.Areas.User.Controllers
{
    [Area("User")]
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Route("User/Comparison")]
        public IActionResult Comparison()
        {
            return View();
        }

        [Route("User/Bookmarks")]
        public IActionResult Bookmarks()
        {
            return View();
        }


    }
}
