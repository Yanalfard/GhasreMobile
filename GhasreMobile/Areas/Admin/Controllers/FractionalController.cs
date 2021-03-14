﻿using DataLayer.Models;
using GhasreMobile.Utilities;
using Microsoft.AspNetCore.Mvc;
using ReflectionIT.Mvc.Paging;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GhasreMobile.Areas.Admin.Controllers
{
    [Area("Admin")]
    [PermissionChecker("admin,employee")]
    public class FractionalController : Controller
    {
        Core _core = new Core();
        public IActionResult Index(int page = 1, int OrderId = 0, string TellNo = null)
        {
            if (!string.IsNullOrEmpty(TellNo) && OrderId == 0)
            {
                IEnumerable<TblOrder> Orders = PagingList.Create(_core.Order.Get(od => od.Client.TellNo.Contains(TellNo) && od.IsFractional.Value), 40, page);
                return View(Orders);
            }
            if (!string.IsNullOrEmpty(TellNo) && OrderId != 0)
            {
                IEnumerable<TblOrder> Orders = PagingList.Create(_core.Order.Get(od => od.Client.TellNo.Contains(TellNo) && od.OrdeId == OrderId && od.IsFractional.Value), 40, page);
                return View(Orders);
            }
            else if (string.IsNullOrEmpty(TellNo) && OrderId != 0)
            {
                IEnumerable<TblOrder> Orders = PagingList.Create(_core.Order.Get(o => o.OrdeId == OrderId && o.IsFractional.Value), 40, page);
                return View(Orders);
            }
            else
            {
                IEnumerable<TblOrder> Orders = PagingList.Create(_core.Order.Get(o => o.IsFractional.Value).OrderByDescending(o => o.OrdeId), 40, page);
                return View(Orders);
            }
        }
    }
}