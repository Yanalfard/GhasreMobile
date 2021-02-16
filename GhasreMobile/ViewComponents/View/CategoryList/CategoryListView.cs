using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GhasreMobile.ViewComponents.View.CategoryList
{
    public class CategoryListView : ViewComponent
    {
        private Core db = new Core();
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)View("~/Views/Shared/Components/CategoryListView/CategoryListView.cshtml", db.Catagory.Get().OrderByDescending(i => i.CatagoryId)));
        }
    }
}
