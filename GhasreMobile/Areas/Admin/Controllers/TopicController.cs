using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Services;

namespace GhasreMobile.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TopicController : Controller
    {
        Core _core = new Core();
        
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Info(int id)
        {
            return ViewComponent("TopicInfoAdmin");
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _core.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
