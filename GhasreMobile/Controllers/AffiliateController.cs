using DataLayer.Models;
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
            TblConfig vonfig = db.Config.Get(i => i.Key == "StoreDescription").Single();
            return View(vonfig);
        }
        [Route("StoreView/{id}/{name}")]
        public IActionResult StoreView(int id,string name)
        {
            return View(db.Store.GetById(id));
        }

    }
}
