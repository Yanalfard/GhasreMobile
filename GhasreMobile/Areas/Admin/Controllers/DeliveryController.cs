using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Models;
using Services.Services;
using ReflectionIT.Mvc.Paging;
using GhasreMobile.Utilities;

namespace GhasreMobile.Areas.Admin.Controllers
{
    [Area("Admin")]
    [PermissionChecker("admin,employee")]
    public class DeliveryController : Controller
    {
        Core _core = new Core();
        public IActionResult Index(int page = 1, string Search = null)
        {
            if (!string.IsNullOrEmpty(Search))
            {
                IEnumerable<TblDelivery> deliveries = PagingList.Create(_core.Delivery.Get(d=>d.TellNo.Contains(Search)), 30, page);
                return View(deliveries);
            }
            else
            {
                IEnumerable<TblDelivery> deliveries = PagingList.Create(_core.Delivery.Get(), 30, page);
                return View(deliveries);
            }
        }

        public IActionResult Info(int id)
        {
            return ViewComponent("DeliveryInfoAdmin", new { id = id });
        }

        public void ChangeStatus(int id)
        {
            TblDelivery delivery = _core.Delivery.GetById(id);
            delivery.IsAccepted = !delivery.IsAccepted;
            _core.Delivery.Update(delivery);
            _core.Delivery.Save();
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
