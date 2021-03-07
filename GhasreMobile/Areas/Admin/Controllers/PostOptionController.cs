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
    public class PostOptionController : Controller
    {
        Core _core = new Core();
        public IActionResult Index(int page = 1)
        {
            IEnumerable<TblPostOption> postOptions = PagingList.Create(_core.PostOption.Get().OrderByDescending(p=>p.PostOptionId), 30, page);
            return View(postOptions);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return ViewComponent("CreatePostOptionAdmin");
        }
        [HttpPost]
        public IActionResult Create(TblPostOption postOption)
        {
            if (ModelState.IsValid)
            {
                _core.PostOption.Add(postOption);
                _core.PostOption.Save();
                return Redirect("/Admin/PostOption");
            }
            return View(postOption);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            return ViewComponent("EditPostOptionAdmin", new { id = id });
        }
        [HttpPost]
        public IActionResult Edit(TblPostOption postOption)
        {
            if (ModelState.IsValid)
            {
                _core.PostOption.Update(postOption);
                _core.PostOption.Save();
                return Redirect("/Admin/PostOption");
            }
            return View(postOption);
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
