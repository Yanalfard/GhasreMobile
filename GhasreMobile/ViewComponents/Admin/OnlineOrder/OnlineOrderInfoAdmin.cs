using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Services;
using DataLayer.Models;

namespace GhasreMobile.ViewComponents.Admin.OnlineOrder
{
    public class OnlineOrderInfoAdmin : ViewComponent
    {
        Core _core = new Core();
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            TblOnlineOrder onlineOrder = _core.OnlineOrder.GetById(id);
            onlineOrder.IsRead = true;
            _core.OnlineOrder.Update(onlineOrder);
            _core.Save();
            return await Task.FromResult((IViewComponentResult)View("/Areas/Admin/Views/OnlineOrder/Components/Info.cshtml", _core.OnlineOrder.GetById(id)));
        }
    }
}
