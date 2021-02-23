using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GhasreMobile.ViewComponents.Admin.Store
{
    public class StoreCreateAdmin:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)View("/Areas/Admin/Views/Store/Components/Create.cshtml"));
        }
    }
}
