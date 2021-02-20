using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GhasreMobile.ViewComponents.Admin.Product
{
    public class PropertyListAdmin:ViewComponent
    {
        Core _core = new Core();
        public async Task<IViewComponentResult> InvokeAsync(int? Id)
        {
            if(Id == null || Id == 0)
            {
                
                return await Task.FromResult((IViewComponentResult)View("/Areas/Admin/Views/Product/Components/PropertyListAdmin.cshtml", _core.Property.Get()));
            }
            else
            {
                ViewBag.PropertyId = Id;
                return await Task.FromResult((IViewComponentResult)View("/Areas/Admin/Views/Product/Components/PropertyListAdmin.cshtml", _core.Property.Get()));

            }
        }
    }
}
