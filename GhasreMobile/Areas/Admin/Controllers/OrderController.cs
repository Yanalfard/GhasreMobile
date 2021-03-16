﻿using GhasreMobile.Utilities;
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
            List<TblOrder> orders = _core.Order.Get().ToList();
            int count = orders.Count;
            if (ordersInAdmin.InPageCount == 0)
            {
                if (ordersInAdmin.OrderId != 0)
                {
                    orders = orders.Where(i => i.OrdeId == ordersInAdmin.OrderId).ToList();
                    count = orders.Count();
                }
                if (ordersInAdmin.TellNo != null)
                {
                    orders = orders.Where(i => i.Client.TellNo == ordersInAdmin.TellNo).ToList();
                    count = orders.Count();
                }
                if (ordersInAdmin.StartDate != null)
                {
                    PersianCalendar pc = new PersianCalendar();
                    string[] Start = ordersInAdmin.StartDate.Split('/');
                    DateTime startTime = pc.ToDateTime(Convert.ToInt32(Start[0]), Convert.ToInt32(Start[1]), Convert.ToInt32(Start[2]), 0, 0, 0, 0);
                    orders = orders.Where(i => i.DateSubmited >= startTime).ToList();
                    count = orders.Count();
                }
                if (ordersInAdmin.EndDate != null)
                {
                    PersianCalendar pc = new PersianCalendar();
                    string[] Start = ordersInAdmin.EndDate.Split('/');
                    DateTime endTime = pc.ToDateTime(Convert.ToInt32(Start[0]), Convert.ToInt32(Start[1]), Convert.ToInt32(Start[2]), 0, 0, 0, 0);
                    orders = orders.Where(i => i.DateSubmited <= endTime).ToList();
                    count = orders.Count();
                }
                ViewBag.pageid = ordersInAdmin.PageId;

                ViewBag.PageCount = count / 18;

                ViewBag.InPageCount = ordersInAdmin.InPageCount;

                ViewBag.OrderId = ordersInAdmin.OrderId;
                ViewBag.TellNo = ordersInAdmin.TellNo;
                ViewBag.StartDate = ordersInAdmin.StartDate;
                ViewBag.EndDate = ordersInAdmin.EndDate;

                var skip = (ordersInAdmin.PageId - 1) * 18;

                return View(orders.Skip(skip).Take(18));
            }
            else
            {
                if (ordersInAdmin.OrderId != 0)
                {
                    orders = orders.Where(i => i.OrdeId == ordersInAdmin.OrderId).ToList();
                    count = orders.Count();
                }
                if (ordersInAdmin.TellNo != null)
                {
                    orders = orders.Where(i => i.Client.TellNo == ordersInAdmin.TellNo).ToList();
                    count = orders.Count();
                }
                if (ordersInAdmin.StartDate != null)
                {
                    PersianCalendar pc = new PersianCalendar();
                    string[] Start = ordersInAdmin.StartDate.Split('/');
                    DateTime startTime = pc.ToDateTime(Convert.ToInt32(Start[0]), Convert.ToInt32(Start[1]), Convert.ToInt32(Start[2]), 0, 0, 0, 0);
                    orders = orders.Where(i => i.DateSubmited >= startTime).ToList();
                    count = orders.Count();
                }
                if (ordersInAdmin.EndDate != null)
                {
                    PersianCalendar pc = new PersianCalendar();
                    string[] Start = ordersInAdmin.EndDate.Split('/');
                    DateTime endTime = pc.ToDateTime(Convert.ToInt32(Start[0]), Convert.ToInt32(Start[1]), Convert.ToInt32(Start[2]), 0, 0, 0, 0);
                    orders = orders.Where(i => i.DateSubmited <= endTime).ToList();
                    count = orders.Count();
                }
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
