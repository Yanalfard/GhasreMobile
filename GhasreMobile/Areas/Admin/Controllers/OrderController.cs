﻿using GhasreMobile.Utilities;
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
    [PermissionChecker("admin,employee")]
    public class OrderController : Controller
    {
        Core _core = new Core();

        public IActionResult Index(int page = 1, int OrderId = 0, string TellNo = null)
        {
            if (!string.IsNullOrEmpty(TellNo) && OrderId == 0)
            {
                IEnumerable<TblOrder> Orders = PagingList.Create(_core.Order.Get(od => od.Client.TellNo.Contains(TellNo)), 40, page);
                return View(Orders);
            }
            if (!string.IsNullOrEmpty(TellNo) && OrderId != 0)
            {
                IEnumerable<TblOrder> Orders = PagingList.Create(_core.Order.Get(od => od.Client.TellNo.Contains(TellNo) && od.OrdeId == OrderId), 40, page);
                return View(Orders);
            }
            else if (string.IsNullOrEmpty(TellNo) && OrderId != 0)
            {
                IEnumerable<TblOrder> Orders = PagingList.Create(_core.Order.Get(o => o.OrdeId == OrderId), 40, page);
                return View(Orders);
            }
            else
            {
                IEnumerable<TblOrder> Orders = PagingList.Create(_core.Order.Get().OrderByDescending(o => o.OrdeId), 40, page);
                return View(Orders);
            }
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
