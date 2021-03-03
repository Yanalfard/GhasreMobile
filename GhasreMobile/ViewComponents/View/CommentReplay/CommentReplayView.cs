using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GhasreMobile.ViewComponents.View.CommentReplay
{
    public class CommentReplayView : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            DataLayer.ViewModels.CommentReplay list= new DataLayer.ViewModels.CommentReplay();
            return await Task.FromResult((IViewComponentResult)View("~/Views/Shared/Components/CommentReplayView/CommentReplayView.cshtml",list));
        }
    }
}
