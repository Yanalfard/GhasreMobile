using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GhasreMobile.ViewComponents.View.Blogs
{
    public class BlogsView : ViewComponent
    {
        private Core db = new Core();
        public async Task<IViewComponentResult> InvokeAsync()
        {
            Random r = new Random();
            return await Task.FromResult((IViewComponentResult)View("~/Views/Shared/Components/BlogsView/BlogsView.cshtml", db.Blog.Get().OrderByDescending(i=>i.DateCreated).Take(40)));
        }
    }
}
