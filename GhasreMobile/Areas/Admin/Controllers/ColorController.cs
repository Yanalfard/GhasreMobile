using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Models;
using Services.Services;
using ReflectionIT.Mvc.Paging;

namespace GhasreMobile.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ColorController : Controller
    {
        Core _core = new Core();
        public IActionResult Index(int page = 1)
        {
            IEnumerable<TblColor> colors = PagingList.Create(_core.Color.Get(), 10, page);
            return View(colors);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return ViewComponent("CreateColorAdmin");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TblColor color)
        {
            if (ModelState.IsValid)
            {
                _core.Color.Add(color);
                _core.Color.Save();
                return Redirect("/Color");
            }
            return View(color);
        }

        public IActionResult Edit(int Id)
        {
            return ViewComponent("EditColorAdmin", new { Id = Id });
        }

        public IActionResult Edit(TblColor color, int Page)
        {
            if (ModelState.IsValid)
            {
                _core.Color.Update(color);
                _core.Color.Save();
                return Redirect("/Color?page=" + Page);
            }
            return View(color);
        }
    }
}
