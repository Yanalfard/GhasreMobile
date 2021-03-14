using DataLayer.Models;
using DataLayer.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GhasreMobile.ViewComponents.View.Search
{
    public class SearchView : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            SearchVm search = new SearchVm();
            return await Task.FromResult((IViewComponentResult)View("~/Views/Shared/Components/SearchView/SearchView.cshtml", search));
        }
    }
}
