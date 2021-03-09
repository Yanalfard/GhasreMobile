using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Models;
using Services.Services;
using ReflectionIT.Mvc.Paging;
using GhasreMobile.Utilities;

namespace GhasreMobile.Areas.Admin.Controllers
{
    [Area("Admin")]
    [PermissionChecker("admin,employee")]
    public class CommentController : Controller
    {
        Core _core = new Core();
        [HttpGet]
        public IActionResult Index(int page = 1)
        {
            IEnumerable<TblComment> comments = PagingList.Create(_core.Comment.Get().OrderByDescending(c => !c.IsValid), 30, page);
            return View(comments);
        }

        public IActionResult Info(int id)
        {
            return ViewComponent("CommentInfoAdmin", new { id = id });
        }

        public string Delete(int id)
        {
            if (_core.Comment.Get(c => c.ParentId == id).Count() > 0)
            {
                return "شما نمیتوانید این نظر را حذف کنید. پاسخی برای کامنت ثبت شده است";
            }
            else
            {
                _core.Comment.DeleteById(id);
                _core.Comment.Save();
                return "true";
            }
        }

        [HttpPost]
        public void ChangeStatus(int id)
        {
            TblComment comment = _core.Comment.GetById(id);
            comment.IsValid = !comment.IsValid;
            _core.Comment.Update(comment);
            _core.Comment.Save();
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
