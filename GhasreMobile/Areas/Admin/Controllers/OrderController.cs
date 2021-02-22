using GhasreMobile.Utilities;
using Microsoft.AspNetCore.Mvc;
using DataLayer.Models;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReflectionIT.Mvc.Paging;

namespace GhasreMobile.Areas.Admin.Controllers
{
    [Area("Admin")]
    [PermissionChecker("admin")]
    public class OrderController : Controller
    {
        Core _core = new Core();

        public IActionResult Index(int page = 1)
        {
            IEnumerable<TblOrder> Orders = PagingList.Create(_core.Order.Get(), 10, page);
            return View(Orders);
        }

        public IActionResult Info(int id)
        {
            return ViewComponent("OrderInfoAdmin", new { id = id });
        }

        public void SendOrder(int id)
        {
            TblOrder order = _core.Order.GetById(id);
            order.Status = 1;
            _core.Order.Update(order);
            _core.Order.Save();
        }

        public void DoneOrder(int id)
        {
            TblOrder order = _core.Order.GetById(id);
            order.Status = 2;
            _core.Order.Update(order);
            _core.Order.Save();
        }
    }
}
