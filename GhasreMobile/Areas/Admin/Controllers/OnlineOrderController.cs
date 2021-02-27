using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GhasreMobile.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OnlineOrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Info(int id)
        {
            return ViewComponent("OnlineOrderInfoAdmin");
        }
    }
}
