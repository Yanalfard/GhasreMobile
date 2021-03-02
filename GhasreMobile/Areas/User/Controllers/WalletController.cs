using DataLayer.Models;
using DataLayer.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GhasreMobile.Areas.User.Controllers
{
    [Area("User")]
    public class WalletController : Controller
    {
        Core db = new Core();
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Charge()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Charge(ChargeWalletVm charge)
        {
            if (ModelState.IsValid)
            {
                
            }
            return View(charge);
        }
    }
}
