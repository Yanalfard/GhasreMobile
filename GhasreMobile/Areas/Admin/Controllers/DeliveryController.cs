using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GhasreMobile.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DeliveryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Info()
        {
            return ViewComponent("DeliveryInfoAdmin");
        }
    }
}
