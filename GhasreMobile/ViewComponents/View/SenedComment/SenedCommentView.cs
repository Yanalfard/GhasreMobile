using DataLayer.Models;
using DataLayer.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GhasreMobile.ViewComponents.View.SenedComment
{
    public class SenedCommentView : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            SendCommentVm commentVm = new SendCommentVm();
            return await Task.FromResult((IViewComponentResult)View("~/Views/Shared/Components/SenedCommentView/SenedCommentView.cshtml", commentVm));
        }
    }
}
