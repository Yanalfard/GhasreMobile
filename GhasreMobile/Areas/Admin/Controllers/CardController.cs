using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GhasreMobile.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return ViewComponent("CardCreateAdmin");
        }
        public IActionResult Edit()
        {
            return ViewComponent("CardEditAdmin");
        }
    }
}
