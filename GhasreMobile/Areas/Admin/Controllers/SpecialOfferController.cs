using GhasreMobile.Utilities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Models;
using Services.Services;
using ReflectionIT.Mvc.Paging;

namespace GhasreMobile.Areas.Admin.Controllers
{
    [Area("Admin")]
    [PermissionChecker("admin,employee")]
    public class SpecialOfferController : Controller
    {
        Core _core = new Core();
        public IActionResult Index(int page = 1)
        {
            IEnumerable<TblSpecialOffer> SpecialOffers = PagingList.Create(_core.SpecialOffer.Get().OrderByDescending(so => so.SpecialOfferId), 40, page);
            return View(SpecialOffers);
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
                _core.Save();
                return Redirect("/Admin/Product");
            }
            return View(specialOffer);
        }

        [HttpPost]
        public void Delete(int id)
        {
            _core.SpecialOffer.DeleteById(id);
            _core.Save();
        }

        public void Remove(int id)
        {
            if (_core.SpecialOffer.Get(s => s.ProductId == id).Count() > 0)
            {
                IEnumerable<TblSpecialOffer> specialOffers = _core.SpecialOffer.Get(s => s.ProductId == id);
                foreach (var item in specialOffers)
                {
                    _core.SpecialOffer.Delete(item);
                }
                _core.Save();
            }
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
