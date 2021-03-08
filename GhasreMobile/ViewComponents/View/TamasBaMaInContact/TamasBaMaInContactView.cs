using DataLayer.Models;
using DataLayer.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GhasreMobile.ViewComponents.View.TamasBaMaInContact
{
    public class TamasBaMaInContactView : ViewComponent
    {
        private Core db = new Core();
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)View("~/Views/Shared/Components/TamasBaMaInContactView/TamasBaMaInContactView.cshtml", db.Config.Get(i => i.Key == "TamasBaMa").Single()));
        }
    }
}
