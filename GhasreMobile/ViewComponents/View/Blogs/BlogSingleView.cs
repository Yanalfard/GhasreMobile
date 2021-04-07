using DataLayer.Models;
using DataLayer.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GhasreMobile.ViewComponents.View.Blogs
{
    public class BlogSingleView : ViewComponent
    {
        private Core db = new Core();
        public async Task<IViewComponentResult> InvokeAsync(string swap, int number = -1)
        {
            TblBlog list = new TblBlog();
            if (number == -1)
            {
                List<TblBlog> ads = db.Blog.Get().ToList();
                Random ran = new Random();
                if (ads.Count > 0)
                {
                    list = ads[ran.Next(ads.Count)];
                }
            }
            else
            {
                // id
                list = db.Blog.GetById(number);
            }
            ViewData["Swap"] = swap;
            return await Task.FromResult((IViewComponentResult)View("~/Views/Shared/Components/BlogSingleView/BlogSingleView.cshtml", list));

        }
    }
}
