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
using DataLayer.ViewModels;

namespace GhasreMobile.Areas.Admin.Controllers
{
    [Area("Admin")]
    [PermissionChecker("admin,employee")]
    public class OrderController : Controller
    {
        Core _core = new Core();

        public IActionResult Index(OrdersInAdminVm ordersInAdmin)
        {
            ViewBag.OrderId = ordersInAdmin.OrderId;
            ViewBag.TellNo = ordersInAdmin.TellNo;
            ViewBag.StartDate = ordersInAdmin.StartDate;
            ViewBag.EndDate = ordersInAdmin.EndDate;
            ViewBag.StatusSelected = ordersInAdmin.StatusSelected;
            List<TblOrder> orders = _core.Order.Get(i => i.IsFractional == false && i.IsPayed).ToList();
            int count = orders.Count;
            if (ordersInAdmin.InPageCount == 0)
            {
                if (ordersInAdmin.OrderId != 0)
                {
                    orders = orders.Where(i => i.OrdeId == ordersInAdmin.OrderId).ToList();
                }
                if (ordersInAdmin.StatusSelected != -2)
                {
                    orders = orders.Where(i => i.Status == ordersInAdmin.StatusSelected).ToList();
                }
                if (ordersInAdmin.TellNo != null)
                {
                    orders = orders.Where(i => i.Client.TellNo.Contains(ordersInAdmin.TellNo)).ToList();

                }
                if (ordersInAdmin.StartDate != null)
                {
                    PersianCalendar pc = new PersianCalendar();
                    string[] Start = ordersInAdmin.StartDate.Split('/');
                    DateTime startTime = pc.ToDateTime(Convert.ToInt32(Start[0]), Convert.ToInt32(Start[1]), Convert.ToInt32(Start[2]), 0, 0, 0, 0).Date;
                    orders = orders.Where(i => i.DateSubmited.Date >= startTime.Date).ToList();
                }
                if (ordersInAdmin.EndDate != null)
                {
                    PersianCalendar pc = new PersianCalendar();
                    string[] Start = ordersInAdmin.EndDate.Split('/');
                    DateTime endTime = pc.ToDateTime(Convert.ToInt32(Start[0]), Convert.ToInt32(Start[1]), Convert.ToInt32(Start[2]), 0, 0, 0, 0).Date;
                    orders = orders.Where(i => i.DateSubmited.Date <= endTime.Date).ToList();
                }


                count = orders.Count();


                ViewBag.pageid = ordersInAdmin.PageId;

                ViewBag.PageCount = count / 18;

                ViewBag.InPageCount = ordersInAdmin.InPageCount;

                ViewBag.OrderId = ordersInAdmin.OrderId;
                ViewBag.TellNo = ordersInAdmin.TellNo;
                ViewBag.StartDate = ordersInAdmin.StartDate;
                ViewBag.EndDate = ordersInAdmin.EndDate;
                ViewBag.StatusSelected = ordersInAdmin.StatusSelected;
                var skip = (ordersInAdmin.PageId - 1) * 18;

                return View(orders.OrderByDescending(o => o.OrdeId).Skip(skip).Take(18));
            }
            else
            {
                if (ordersInAdmin.OrderId != 0)
                {
                    orders = orders.Where(i => i.OrdeId == ordersInAdmin.OrderId).ToList();

                }
                if (ordersInAdmin.StatusSelected != -2)
                {
                    orders = orders.Where(i => i.Status == ordersInAdmin.StatusSelected).ToList();
                }
                if (ordersInAdmin.TellNo != null)
                {
                    orders = orders.Where(i => i.Client.TellNo.Contains(ordersInAdmin.TellNo)).ToList();

                }
                if (ordersInAdmin.StartDate != null)
                {
                    PersianCalendar pc = new PersianCalendar();
                    string[] Start = ordersInAdmin.StartDate.Split('/');
                    DateTime startTime = pc.ToDateTime(Convert.ToInt32(Start[0]), Convert.ToInt32(Start[1]), Convert.ToInt32(Start[2]), 0, 0, 0, 0).Date;
                    orders = orders.Where(i => i.DateSubmited.Date >= startTime.Date).ToList();
                }
                if (ordersInAdmin.EndDate != null)
                {
                    PersianCalendar pc = new PersianCalendar();
                    string[] Start = ordersInAdmin.EndDate.Split('/');
                    DateTime endTime = pc.ToDateTime(Convert.ToInt32(Start[0]), Convert.ToInt32(Start[1]), Convert.ToInt32(Start[2]), 0, 0, 0, 0).Date;
                    orders = orders.Where(i => i.DateSubmited.Date <= endTime.Date).ToList();

                }

                count = orders.Count();

                ViewBag.pageid = ordersInAdmin.PageId;

                ViewBag.PageCount = count / ordersInAdmin.InPageCount;

                ViewBag.InPageCount = ordersInAdmin.InPageCount;
                var skip = (ordersInAdmin.PageId - 1) * ordersInAdmin.InPageCount;
                return View(orders.Skip(skip).Take(ordersInAdmin.InPageCount));
            }


        }
        public IActionResult AllOrder(OrdersInAdminVm ordersInAdmin)
        {
            ViewBag.OrderId = ordersInAdmin.OrderId;
            ViewBag.TellNo = ordersInAdmin.TellNo;
            ViewBag.StartDate = ordersInAdmin.StartDate;
            ViewBag.EndDate = ordersInAdmin.EndDate;
            ViewBag.StatusSelected = ordersInAdmin.StatusSelected;
            List<TblOrder> orders = _core.Order.Get().ToList();
            int count = orders.Count;
            if (ordersInAdmin.InPageCount == 0)
            {
                if (ordersInAdmin.OrderId != 0)
                {
                    orders = orders.Where(i => i.OrdeId == ordersInAdmin.OrderId).ToList();
                }
                if (ordersInAdmin.StatusSelected != -2)
                {
                    orders = orders.Where(i => i.Status == ordersInAdmin.StatusSelected).ToList();
                }
                if (ordersInAdmin.TellNo != null)
                {
                    orders = orders.Where(i => i.Client.TellNo.Contains(ordersInAdmin.TellNo)).ToList();

                }
                if (ordersInAdmin.StartDate != null)
                {
                    PersianCalendar pc = new PersianCalendar();
                    string[] Start = ordersInAdmin.StartDate.Split('/');
                    DateTime startTime = pc.ToDateTime(Convert.ToInt32(Start[0]), Convert.ToInt32(Start[1]), Convert.ToInt32(Start[2]), 0, 0, 0, 0).Date;
                    orders = orders.Where(i => i.DateSubmited.Date >= startTime.Date).ToList();
                }
                if (ordersInAdmin.EndDate != null)
                {
                    PersianCalendar pc = new PersianCalendar();
                    string[] Start = ordersInAdmin.EndDate.Split('/');
                    DateTime endTime = pc.ToDateTime(Convert.ToInt32(Start[0]), Convert.ToInt32(Start[1]), Convert.ToInt32(Start[2]), 0, 0, 0, 0).Date;
                    orders = orders.Where(i => i.DateSubmited.Date <= endTime.Date).ToList();
                }


                count = orders.Count();


                ViewBag.pageid = ordersInAdmin.PageId;

                ViewBag.PageCount = count / 18;

                ViewBag.InPageCount = ordersInAdmin.InPageCount;

                ViewBag.OrderId = ordersInAdmin.OrderId;
                ViewBag.TellNo = ordersInAdmin.TellNo;
                ViewBag.StartDate = ordersInAdmin.StartDate;
                ViewBag.EndDate = ordersInAdmin.EndDate;
                ViewBag.StatusSelected = ordersInAdmin.StatusSelected;

                var skip = (ordersInAdmin.PageId - 1) * 18;

                return View(orders.OrderByDescending(o => o.OrdeId).Skip(skip).Take(18));
            }
            else
            {
                if (ordersInAdmin.OrderId != 0)
                {
                    orders = orders.Where(i => i.OrdeId == ordersInAdmin.OrderId).ToList();

                }
                if (ordersInAdmin.StatusSelected != -2)
                {
                    orders = orders.Where(i => i.Status == ordersInAdmin.StatusSelected).ToList();
                }
                if (ordersInAdmin.TellNo != null)
                {
                    orders = orders.Where(i => i.Client.TellNo.Contains(ordersInAdmin.TellNo)).ToList();

                }
                if (ordersInAdmin.StartDate != null)
                {
                    PersianCalendar pc = new PersianCalendar();
                    string[] Start = ordersInAdmin.StartDate.Split('/');
                    DateTime startTime = pc.ToDateTime(Convert.ToInt32(Start[0]), Convert.ToInt32(Start[1]), Convert.ToInt32(Start[2]), 0, 0, 0, 0).Date;
                    orders = orders.Where(i => i.DateSubmited.Date >= startTime.Date).ToList();

                }
                if (ordersInAdmin.EndDate != null)
                {
                    PersianCalendar pc = new PersianCalendar();
                    string[] Start = ordersInAdmin.EndDate.Split('/');
                    DateTime endTime = pc.ToDateTime(Convert.ToInt32(Start[0]), Convert.ToInt32(Start[1]), Convert.ToInt32(Start[2]), 0, 0, 0, 0).Date;
                    orders = orders.Where(i => i.DateSubmited.Date <= endTime.Date).ToList();

                }

                count = orders.Count();

                ViewBag.pageid = ordersInAdmin.PageId;

                ViewBag.PageCount = count / ordersInAdmin.InPageCount;

                ViewBag.InPageCount = ordersInAdmin.InPageCount;
                var skip = (ordersInAdmin.PageId - 1) * ordersInAdmin.InPageCount;
                return View(orders.Skip(skip).Take(ordersInAdmin.InPageCount));
            }


        }

        public IActionResult Info(int id)
        {
            return ViewComponent("OrderInfoAdmin", new { id = id });
        }
        public IActionResult CodeMarsole(int id)
        {
            return ViewComponent("CodeMarsoleAdmin", new { id = id });
        }
        [HttpPost]
        public IActionResult CodeMarsole(CodeMarsoleVm code)
        {
            if (ModelState.IsValid)
            {
                TblOrder selectedOrder = _core.Order.GetById(code.OrdeId);
                if (selectedOrder != null)
                {
                    selectedOrder.SentNo = code.SentNo;
                    _core.Order.Update(selectedOrder);
                    _core.Save();
                    return Redirect(code.ReturnUrl);
                }
            }
            return PartialView("CodeMarsole", code);
        }
        public async Task SendOrderAsync(int id)
        {
            TblOrder order = _core.Order.GetById(id);
            order.Status = 1;
            await Sms.SendSms(order.Client.TellNo, order.OrdeId.ToString(), "GhasrMobileSendOrder");
            _core.Order.Update(order);
            _core.Save();
        }

        public async Task DoneOrder(int id)
        {
            TblOrder order = _core.Order.GetById(id);
            order.Status = 2;
            await Sms.SendSms(order.Client.TellNo, order.OrdeId.ToString(), "GhasrMobileDoneOrder");
            _core.Order.Update(order);
            _core.Save();
        }

        public void ChangeSendOrderStatus(int id)
        {
            TblOrder Order = _core.Order.GetById(id);
            Order.Status = 0;
            _core.Order.Update(Order);
            _core.Save();
        }

        public void Payed(int id)
        {
            TblOrder order = _core.Order.GetById(id);
            order.IsPayed = !order.IsPayed;
            _core.Order.Update(order);
            _core.Save();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _core.Dispose();
            }
            base.Dispose(disposing);
        }

        public async Task<string> RedreshFractionalAsync(int id)
        {
            TblOrder order = _core.Order.GetById(id);
            order.Status = 0;
            _core.Order.Update(order);
            _core.Save();
            return await Task.FromResult("true");
        }
        public async Task<string> DeleteFractional(int id)
        {
            TblOrder order = _core.Order.GetById(id);
            order.Status = -1;
            _core.Order.Update(order);
            _core.Save();
            return await Task.FromResult("true");
        }

    }
}
