using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GhasreMobile.Controllers
{
    public class BlogController : Controller
    {
        private Core db = new Core();
        public async Task<IActionResult> Index()
        {
            return await Task.FromResult(View(db.Blog.Get()));
        }
        [Route("ViewBlog/{id}/{title}")]
        public async Task<IActionResult> ViewBlog(int id, string title)
        {
            return await Task.FromResult(View(db.Blog.GetById(id)));
        }

    }
}
