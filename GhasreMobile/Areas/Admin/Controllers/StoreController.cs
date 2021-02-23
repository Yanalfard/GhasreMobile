using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GhasreMobile.Utilities;
using DataLayer.Models;
using Services.Services;
using ReflectionIT.Mvc.Paging;

namespace GhasreMobile.Areas.Admin.Controllers
{
    [Area("Admin")]
    [PermissionChecker("admin")]
    public class StoreController : Controller
    {
        Core _core = new Core();
        public IActionResult Index(int page = 1, string Search = null)
        {
            if (!string.IsNullOrEmpty(Search))
            {
                IEnumerable<TblStore> stores = PagingList.Create(_core.Store.Get(s=>s.Name.Contains(Search)), 30, page);
                return View(stores);
            }
            else
            {
                IEnumerable<TblStore> stores = PagingList.Create(_core.Store.Get(), 30, page);
                return View(stores);
            }
        }

        public IActionResult Create()
        {
            return ViewComponent("StoreCreateAdmin");
        }

        public IActionResult Edit()
        {
            return ViewComponent("StoreEditAdmin");
        }

        public void Delete()
        {

        }
    }
}
