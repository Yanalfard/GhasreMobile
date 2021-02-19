using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GhasreMobile.ViewComponents.View.Slider
{
    public class SliderView : ViewComponent
    {
        private Core db = new Core();
        public async Task<IViewComponentResult> InvokeAsync()
        {

            DateTime dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            List<TblBannerAndSlide> list = db.BannerAndSlide.Get(i => i.IsActive && i.ActiveTill >= dt).ToList();
            return await Task.FromResult((IViewComponentResult)View("~/Views/Shared/Components/SliderView/SliderView.cshtml", list));
        }
    }
}
