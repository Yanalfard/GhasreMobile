using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Models;
using Services.Services;

namespace GhasreMobile.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CommentController : Controller
    {
        Core _core = new Core();
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Info(int Id)
        {
            return ViewComponent("CommentInfoAdmin", new { Id = Id });
        }

        [HttpPost]
        public IActionResult ChangeStatus(int Id)
        {
            TblComment comment = _core.Comment.GetById(Id);
            comment.IsValid = !comment.IsValid;
            _core.Comment.Update(comment);
            _core.Comment.Save();
            return Redirect("/Comment");
        }
    }
}
