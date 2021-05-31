using DataLayer.Models;
using DataLayer.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GhasreMobile.ViewComponents.View.TelegramInstaView
{
    public class TelegramInstaView : ViewComponent
    {
        private Core db = new Core();
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<TblConfig> list = new List<TblConfig>();
            TblConfig tel = db.Config.Get(i => i.Key == "LinkTelegram").SingleOrDefault();
            TblConfig ins = db.Config.Get(i => i.Key == "LinkInsta").SingleOrDefault();
            TblConfig Whatsapp = db.Config.Get(i => i.Key == "Whatsapp").SingleOrDefault();
            list.Add(tel);
            list.Add(ins);
            list.Add(Whatsapp);
            return await Task.FromResult((IViewComponentResult)View("~/Views/Shared/Components/TelegramInstaView/TelegramInstaView.cshtml", list));
        }
    }
}
