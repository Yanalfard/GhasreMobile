using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GhasreMobile.ViewComponents.Admin.PostOption
{
    public class CreatePostOptionAdmin : ViewComponent
    {
        Core _core = new Core();
        public async Task<IViewComponentResult> InvokeAsync(int? Id)
        {
            return await Task.FromResult((IViewComponentResult)View("/Areas/Admin/Views/PostOption/Components/Create.cshtml"));
        }
    }
}
