using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GhasreMobile.Utilities;

namespace GhasreMobile.Areas.Admin.Controllers
{
    [Area("Admin")]
    [PermissionChecker("admin")]
    public class StoreController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return ViewComponent("StoreCreateAdmin");
        }

        public IActionResult Edit()
        {
            return ViewComponent("StoreEditAdmin");
        }

        public IActionResult Delete()
        {
            return View();
        }
    }
}
