using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace GhasreMobile.ViewComponents.View.ShowProductsIn
{
    public class ShowProductsInView : ViewComponent
    {
        private Core db = new Core();
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            return await Task.FromResult((IViewComponentResult)View("~/Views/Shared/Components/ShowProductsInView/ShowProductsInView.cshtml", db.Product.Get(i => i.BrandId == id)));
        }
    }
}
