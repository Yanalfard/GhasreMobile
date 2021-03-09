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
        public async Task<IActionResult> Index()
        {
            try
            {
                TblConfig vonfig = db.Config.Get(i => i.Key == "StoreDescription").Single();
                return await Task.FromResult(View(vonfig));
            }
            catch
            {
                return Redirect("404.html");
            }
        }
        [Route("StoreView/{id}/{name}")]
        public async Task<IActionResult> StoreView(int id, string name)
        {
            try
            {
                return await Task.FromResult(View(db.Store.GetById(id)));
            }
            catch
            {
                return Redirect("404.html");
            }
        }

    }
}
