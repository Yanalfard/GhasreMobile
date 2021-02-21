using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GhasreMobile.Areas.User.Controllers
{
    [Area("User")]
    public class OrderController : Controller
    {
        // History
        public IActionResult Index()
        {
            return View();
        }

        // Make order
        public IActionResult Finalize()
        {
            return View();
        }

    }
}
