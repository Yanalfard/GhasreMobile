using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GhasreMobile.ViewComponents.Admin.Catagory
{
    public class SelectedCategoryAdmin : ViewComponent
    {
        Core _core = new Core();
        public async Task<IViewComponentResult> InvokeAsync()
        {
            ViewBag.Catagory = _core.Catagory.Get(c => c.ParentId == null);

            return await Task.FromResult((IViewComponentResult)View("/Areas/Admin/Views/Catagory/Components/SelectedCategory.cshtml"));
        }
    }
}
