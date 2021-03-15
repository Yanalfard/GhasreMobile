using GhasreMobile.Utilities;
using Microsoft.AspNetCore.Mvc;
using DataLayer.Models;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReflectionIT.Mvc.Paging;
using System.Globalization;
using DataLayer.Utilities;

namespace GhasreMobile.Areas.Admin.Controllers
{
    [Area("Admin")]
    [PermissionChecker("admin,employee")]
    public class OrderController : Controller
    {
        Core _core = new Core();

        public IActionResult Index(int page = 1, int OrderId = 0, string TellNo = null, string StartDate = null, string EndDate = null)
        {

            List<TblOrder> orders = _core.Order.Get().ToList();
            if (OrderId != 0)
            {
                orders = orders.Where(i => i.OrdeId == OrderId).ToList();
            }
            if (TellNo != null)
            {
                orders = orders.Where(i => i.Client.TellNo == TellNo).ToList();
            }
            if (StartDate != null)
            {
                PersianCalendar pc = new PersianCalendar();
                string[] Start = StartDate.Split('/');
                DateTime startTime = pc.ToDateTime(Convert.ToInt32(Start[0]), Convert.ToInt32(Start[1]), Convert.ToInt32(Start[2]), 0, 0, 0, 0);
                orders = orders.Where(i => i.DateSubmited >= startTime).ToList();
            }
            if (EndDate != null)
            {
                PersianCalendar pc = new PersianCalendar();
                string[] Start = EndDate.Split('/');
                DateTime endTime = pc.ToDateTime(Convert.ToInt32(Start[0]), Convert.ToInt32(Start[1]), Convert.ToInt32(Start[2]), 0, 0, 0, 0);
                orders = orders.Where(i => i.DateSubmited <= endTime).ToList();
            }
            return View(PagingList.Create(orders, 50, page));

        }

        public IActionResult Info(int id)
        {
            return ViewComponent("OrderInfoAdmin", new { id = id });
        }

        public async Task SendOrderAsync(int id)
        {
            TblOrder order = _core.Order.GetById(id);
            order.Status = 1;
            await Sms.SendSms(order.Client.TellNo, order.OrdeId.ToString(), "GhasrMobileSendOrder");
            _core.Order.Update(order);
            _core.Order.Save();
        }

        public async Task DoneOrder(int id)
        {
            TblOrder order = _core.Order.GetById(id);
            order.Status = 2;
            await Sms.SendSms(order.Client.TellNo, order.OrdeId.ToString(), "GhasrMobileDoneOrder");
            _core.Order.Update(order);
            _core.Order.Save();
        }

        public void ChangeSendOrderStatus(int id)
        {
            TblOrder Order = _core.Order.GetById(id);
            Order.Status = 0;
            _core.Order.Update(Order);
            _core.Order.Save();
        }

        public void Payed(int id)
        {
            TblOrder order = _core.Order.GetById(id);
            order.IsPayed = !order.IsPayed;
            _core.Order.Update(order);
            _core.Order.Save();
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
