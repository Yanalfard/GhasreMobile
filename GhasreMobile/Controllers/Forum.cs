using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GhasreMobile.Controllers
{
    public class Forum : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ForumView()
        {
            return View();
        }

        public IActionResult NewForum()
        {
            return View();
        }

    }
}
