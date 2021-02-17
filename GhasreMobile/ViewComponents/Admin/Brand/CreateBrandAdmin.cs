using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GhasreMobile.ViewComponents.Admin.Brand
{
    public class CreateBrandAdmin : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int? Id)
        {
            return await Task.FromResult((IViewComponentResult)View("/Areas/Admin/Views/Brand/Components/Create.cshtml"));
        }
    }
}
