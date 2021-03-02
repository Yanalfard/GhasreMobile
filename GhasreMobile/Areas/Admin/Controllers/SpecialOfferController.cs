using GhasreMobile.Utilities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Models;
using Services.Services;

namespace GhasreMobile.Areas.Admin.Controllers
{
    [Area("Admin")]
    [PermissionChecker("admin,employee")]
    public class SpecialOfferController : Controller
    {
        Core _core = new Core();
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateSpecialOffer(TblSpecialOffer specialOffer, int Till)
        {
            if (ModelState.IsValid)
            {
                TblSpecialOffer NewspecialOffer = new TblSpecialOffer();
                NewspecialOffer.Discount = specialOffer.Discount;
                NewspecialOffer.ProductId = specialOffer.ProductId;
                NewspecialOffer.ValidTill = DateTime.Now.AddDays(Till);
                _core.SpecialOffer.Add(NewspecialOffer);
                _core.SpecialOffer.Save();
                return Redirect("/Admin/Product");
            }
            return View(specialOffer);
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
