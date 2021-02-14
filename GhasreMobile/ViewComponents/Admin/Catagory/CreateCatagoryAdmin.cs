using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Models;

namespace GhasreMobile.ViewComponents.Admin.Catagory
{
    public class CreateCatagoryAdmin : ViewComponent
    {
        Core _core = new Core();
        public async Task<IViewComponentResult> InvokeAsync(int? Id)
        {
            if (Id == null)
            {
                return await Task.FromResult((IViewComponentResult)View("/Areas/Admin/Views/Catagory/Components/Create.cshtml"));
            }
            else
            {
                TblCatagory catagory = _core.Catagory.GetById(Id);
                ViewBag.ParentId = catagory.CatagoryId;
                ViewBag.Name = catagory.Name;
                return await Task.FromResult((IViewComponentResult)View("/Areas/Admin/Views/Catagory/Components/Create.cshtml"));

            }
        }
    }
}
