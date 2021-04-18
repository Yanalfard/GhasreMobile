using DataLayer.Models;
using GhasreMobile.Utilities;
using Microsoft.AspNetCore.Mvc;
using ReflectionIT.Mvc.Paging;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace GhasreMobile.Areas.Admin.Controllers
{
    [Area("Admin")]
    [PermissionChecker("admin,employee")]
    public class FractionalController : Controller
    {
        Core _core = new Core();
        public IActionResult Index(int page = 1, int OrderId = 0, string TellNo = null, string StartDate = null, string EndDate = null, int StatusSelected = -2)
        {
            IEnumerable<TblOrder> data = _core.Order.Get(i => i.IsFractional == true);
            if (!string.IsNullOrEmpty(TellNo))
            {
                data = data.Where(o => o.Client.TellNo.Contains(TellNo));
            }
            if (OrderId != 0)
            {
                data = data.Where(o => o.OrdeId == OrderId);
            }
            if (StatusSelected != -2)
            {
                data = data.Where(i => i.Status == StatusSelected).ToList();
            }
            if (StartDate != null)
            {
                PersianCalendar pc = new PersianCalendar();
                string[] Start = StartDate.Split('/');
                DateTime startTime = pc.ToDateTime(Convert.ToInt32(Start[0]), Convert.ToInt32(Start[1]), Convert.ToInt32(Start[2]), 0, 0, 0, 0).Date;
                data = data.Where(i => i.DateSubmited.Date >= startTime.Date).ToList();
            }
            if (EndDate != null)
            {
                PersianCalendar pc = new PersianCalendar();
                string[] Start = EndDate.Split('/');
                DateTime endTime = pc.ToDateTime(Convert.ToInt32(Start[0]), Convert.ToInt32(Start[1]), Convert.ToInt32(Start[2]), 0, 0, 0, 0).Date;
                data = data.Where(i => i.DateSubmited.Date <= endTime.Date).ToList();
            }

            return View(PagingList.Create(data.OrderByDescending(o => o.OrdeId), 40, page));
        }
    }
}
