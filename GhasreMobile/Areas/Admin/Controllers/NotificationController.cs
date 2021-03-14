using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GhasreMobile.Utilities;
using Services.Services;
using DataLayer.Models;
using ReflectionIT.Mvc.Paging;


namespace GhasreMobile.Areas.Admin.Controllers
{
    [Area("Admin")]
    [PermissionChecker("admin")]
    public class NotificationController : Controller
    {
        Core _core = new Core();

        public IActionResult Index(int page = 1, string Search = null)
        {
            if (!string.IsNullOrEmpty(Search))
            {
                IEnumerable<TblNotification> notifications = PagingList.Create(_core.Notification.Get(n => n.Client.TellNo.Contains(Search)), 30, page);
                return View(notifications);
            }
            else
            {
                IEnumerable<TblNotification> notifications = PagingList.Create(_core.Notification.Get().OrderByDescending(n => n.NotificationId), 50, page);
                return View(notifications);
            }

        }

        [HttpGet]
        public IActionResult Create()
        {
            return ViewComponent("CreateNotificationAdmin");
        }

        [HttpPost]
        public IActionResult Create(bool SendAll, bool SendEmployees, int? UserId, string Text)
        {
            TblClient Sender = _core.Client.Get(c => c.TellNo == User.Identity.Name.ToString()).Single();
            if (!SendAll)
            {
                if (SendEmployees)
                {
                    IEnumerable<TblClient> clients = _core.Client.Get(i => i.RoleId == 2);
                    foreach (var item in clients)
                    {
                        TblNotification notification = new TblNotification();
                        notification.ClientId = item.ClientId;
                        notification.IsSeen = false;
                        notification.SenderId = Sender.ClientId;
                        notification.Message = Text;
                        _core.Notification.Add(notification);
                    }
                    _core.Notification.Save();
                    return Redirect("/Admin/Notification");
                }
                else
                {
                    TblNotification notification = new TblNotification();
                    notification.ClientId = UserId.Value;
                    notification.IsSeen = false;
                    notification.SenderId = Sender.ClientId;
                    notification.Message = Text;
                    _core.Notification.Add(notification);
                    _core.Notification.Save();
                    return Redirect("/Admin/Notification");
                }
            }
            else
            {
                IEnumerable<TblClient> clients = _core.Client.Get();
                foreach (var item in clients)
                {
                    TblNotification notification = new TblNotification();
                    notification.ClientId = item.ClientId;
                    notification.IsSeen = false;
                    notification.SenderId = Sender.ClientId;
                    notification.Message = Text;
                    _core.Notification.Add(notification);
                }
                _core.Notification.Save();
                return Redirect("/Admin/Notification");
            }
        }

        public IActionResult NotificationInfo(int id)
        {
            return ViewComponent("NotificationInfoAdmin", new { id = id });
        }

        public int ReturnUser(string TelNo)
        {
            TblClient client = _core.Client.Get(c => c.TellNo == TelNo).SingleOrDefault();

            if (client != null)
            {
                return client.ClientId;
            }
            else
            {
                return 0;
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
