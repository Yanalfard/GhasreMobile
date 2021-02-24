using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Services;

namespace GhasreMobile.ViewComponents.Admin.Discount
{
    public class DiscountEditAdmin : ViewComponent
    {
        Core _core = new Core();
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            return await Task.FromResult((IViewComponentResult)View("/Areas/Admin/Views/Discount/Components/Edit.cshtml", _core.Discount.GetById(id)));
        }
    }
}
