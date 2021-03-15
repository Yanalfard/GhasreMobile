using DataLayer.Models;
using DataLayer.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace GhasreMobile.ViewComponents.View.Store
{
    public class StoreView : ViewComponent
    {
        private Core db = new Core();
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)View("~/Views/Shared/Components/StoreView/StoreView.cshtml", db.Store.Get().OrderByDescending(i => i.StoreId)));
        }
    }
}
