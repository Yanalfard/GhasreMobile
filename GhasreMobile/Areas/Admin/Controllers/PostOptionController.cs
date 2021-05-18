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
            IEnumerable<TblPostOption> postOptions = PagingList.Create(_core.PostOption.Get().OrderByDescending(p => p.PostOptionId), 30, page);
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
            postOption.IsActive = true;
            _core.PostOption.Add(postOption);
            _core.Save();
            return Redirect("/Admin/PostOption");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            return ViewComponent("EditPostOptionAdmin", new { id = id });
        }
        [HttpPost]
        public IActionResult Edit(TblPostOption postOption)
        {
            postOption.IsActive = true;
            _core.PostOption.Update(postOption);
            _core.Save();
            return Redirect("/Admin/PostOption");

        }
        public string Delete(int id)
        {
            try
            {
                TblPostOption post = _core.PostOption.GetById(id);
                if (post.TblOrder.Count() > 0)
                {
                    return "سفارشی برای این  وجود دارد";
                }
                else
                {

                    _core.PostOption.Delete(post);
                    _core.Save();
                    return "true";
                }
            }
            catch
            {
                return "خطا در حذف";
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
