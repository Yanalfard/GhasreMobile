using DataLayer.Models;
using DataLayer.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GhasreMobile.ViewComponents.View.WhatsappUnderPage
{
    public class WhatsappUnderPage : ViewComponent
    {
        private Core db = new Core();
        public async Task<IViewComponentResult> InvokeAsync()
        {
            TblConfig Whatsapp = db.Config.Get(i => i.Key == "Whatsapp").SingleOrDefault();
            return await Task.FromResult((IViewComponentResult)View("~/Views/Shared/Components/WhatsappUnderPage/WhatsappUnderPage.cshtml", Whatsapp));
        }
    }
}
