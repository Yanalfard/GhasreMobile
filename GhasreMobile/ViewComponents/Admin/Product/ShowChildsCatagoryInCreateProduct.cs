using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GhasreMobile.ViewComponents.Admin.Product
{
    public class ShowChildsCatagoryInCreateProduct : ViewComponent
    {
        Core _core = new Core();
        public async Task<IViewComponentResult> InvokeAsync(int? Id)
        {
            var Catagory = _core.Catagory.GetById(Id);
            ViewBag.ParentName = Catagory.Name;
            ViewBag.Id = Catagory.CatagoryId;
            return await Task.FromResult((IViewComponentResult)View("/Areas/Admin/Views/Product/Components/ShowChildsCatagory.cshtml", _core.Catagory.Get(c => c.ParentId == Id)));
        }
    }
}
