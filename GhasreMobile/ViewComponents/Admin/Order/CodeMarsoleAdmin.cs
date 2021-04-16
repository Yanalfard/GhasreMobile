using DataLayer.Models;
using DataLayer.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GhasreMobile.ViewComponents.Admin.Order
{
    public class CodeMarsoleAdmin : ViewComponent
    {
        Core _core = new Core();
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            TblOrder order = _core.Order.GetById(id);
            CodeMarsoleVm selected = new CodeMarsoleVm();
            selected.OrdeId = order.OrdeId;
            selected.SentNo = order.SentNo;
            return await Task.FromResult((IViewComponentResult)View("/Areas/Admin/Views/Order/Components/CodeMarsole.cshtml", selected));
        }
    }
}
