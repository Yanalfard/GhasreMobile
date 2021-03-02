using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GhasreMobile.Controllers
{
    public class AffiliateController : Controller
    {
        private Core db = new Core();
        public IActionResult Index()
        {
            return View(db.Store.Get());
        }
        [Route("StoreView/{id}/{name}")]
        public IActionResult StoreView(int id,string name)
        {
            return View(db.Store.GetById(id));
        }

    }
}
