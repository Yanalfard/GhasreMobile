using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Services;

namespace GhasreMobile.ViewComponents.Admin.Product
{
    public class CreateColorAdmin : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int Id)
        {
            Core _core = new Core();
            ViewBag.Name = _core.Product.GetById(Id).Name;
            return await Task.FromResult((IViewComponentResult)View("/Areas/Admin/Views/Product/Components/CreateColor.cshtml"));
        }
    }
}
