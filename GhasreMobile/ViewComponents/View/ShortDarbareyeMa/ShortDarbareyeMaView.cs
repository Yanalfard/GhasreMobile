using DataLayer.Models;
using DataLayer.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GhasreMobile.ViewComponents.View.ShortDarbareyeMa
{
    public class ShortDarbareyeMaView : ViewComponent
    {
        private Core db = new Core();
        public async Task<IViewComponentResult> InvokeAsync()
        {
            TblConfig ShortDarbareyeMa = db.Config.Get(i => i.Key == "ShortDarbareyeMa").SingleOrDefault();
            return await Task.FromResult((IViewComponentResult)View("~/Views/Shared/Components/ShortDarbareyeMaView/ShortDarbareyeMaView.cshtml", ShortDarbareyeMa));
        }
    }
}
