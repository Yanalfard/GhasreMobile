﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GhasreMobile.Areas.User.Controllers
{
    [Area("User")]
    public class WalletController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
