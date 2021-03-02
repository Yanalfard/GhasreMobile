using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Models;
using ReflectionIT.Mvc.Paging;
using Services.Services;
using GhasreMobile.Utilities;

namespace GhasreMobile.Areas.Admin.Controllers
{
    [Area("Admin")]
    [PermissionChecker("admin,employee")]
    public class OnlineOrderController : Controller
    {
        Core _core = new Core();
        public IActionResult Index(int page = 1)
        {
            IEnumerable<TblOnlineOrder> onlineOrders = PagingList.Create(_core.OnlineOrder.Get(), 30, page);
            return View(onlineOrders);
        }
        public IActionResult Info(int id)
        {
            return ViewComponent("OnlineOrderInfoAdmin", new { id = id });
        }
    }
}
