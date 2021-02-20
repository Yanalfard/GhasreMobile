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
    [PermissionChecker("admin")]
    public class CommentController : Controller
    {
        Core _core = new Core();
        [HttpGet]
        public IActionResult Index(int page = 1)
        {
            IEnumerable<TblComment> comments = PagingList.Create(_core.Comment.Get(), 30, page);
            return View(comments);
        }

        public IActionResult Info(int id)
        {
            return ViewComponent("CommentInfoAdmin", new { id = id });
        }

        [HttpPost]
        public IActionResult ChangeStatus(int id)
        {
            TblComment comment = _core.Comment.GetById(id);
            comment.IsValid = !comment.IsValid;
            _core.Comment.Update(comment);
            _core.Comment.Save();
            return Redirect("/Comment");
        }
    }
}
