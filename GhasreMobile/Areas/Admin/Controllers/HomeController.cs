using GhasreMobile.Utilities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.ViewModels;
using DataLayer.Models;
using Services.Services;

namespace GhasreMobile.Areas.Admin.Controllers
{
    [Area("Admin")]
    [PermissionChecker("admin,employee")]
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            Core _core = new Core();
            IEnumerable<TblClient> clients = _core.Client.Get(v => v.IsActive);
            IEnumerable<TblOrder> orders = _core.Order.Get();
            IEnumerable<TblTicket> tickets = _core.Ticket.Get();
            return View(new AdminDashboardVm()
            {
                CustomersCount = clients.Where(c => c.RoleId == 1).Count(),
                EmployeesCount = clients.Where(c => c.RoleId == 2).Count(),
                AgenciesCount = _core.Store.Get().Count(),
                BrandsCount = _core.Brand.Get().Count(),
                AllOrderCount = orders.Count(),
                OrderSucssesCount = orders.Where(o => o.IsPayed).Count(),
                OrderCancelCount = orders.Where(o => !o.IsPayed).Count(),
                OnlineOrderCount = _core.OnlineOrder.Get().Count(),
                allTicketCount = tickets.Count(),
                AllNotificationCount=_core.Notification.Get().Count(),
                IsSeenTicketCount = tickets.Where(t => t.IsAnswerd).Count(),
                NotSeenTicketCount = tickets.Where(t => !t.IsAnswerd).Count(),
                AllTopicCount = _core.Topic.Get().Count(),
                AllPostCount = _core.Blog.Get().Count(),
                AllRegularQuestionCount = _core.RegularQuestion.Get().Count()
            });
        }
    }
}
