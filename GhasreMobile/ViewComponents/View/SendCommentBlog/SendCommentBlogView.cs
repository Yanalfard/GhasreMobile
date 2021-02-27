using DataLayer.Models;
using DataLayer.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GhasreMobile.ViewComponents.View.SendCommentBlog
{
    public class SendCommentBlogView : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            SendCommentVm commentVm = new SendCommentVm();
            commentVm.BlogId = id;
            return await Task.FromResult((IViewComponentResult)View("~/Views/Shared/Components/SendCommentBlogView/SendCommentBlogView.cshtml", commentVm));
        }
    }
}
