using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Services;

namespace GhasreMobile.ViewComponents.Admin.OnlineOrder
{
    public class OnlineOrderInfoAdmin : ViewComponent
    {
        Core _core = new Core();
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            return await Task.FromResult((IViewComponentResult)View("/Areas/Admin/Views/OnlineOrder/Components/Info.cshtml", _core.OnlineOrder.GetById(id)));
        }
    }
}
