using DataLayer.Models;
using DataLayer.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GhasreMobile.ViewComponents.View.Ad
{
    public class AdView : ViewComponent
    {
        private Core db = new Core();
        public async Task<IViewComponentResult> InvokeAsync(string swap, int number = -1)
        {
            TblAd list = new TblAd();
            if (number == -1)
            {
                List<TblAd> ads = db.Ad.Get().ToList();
                Random ran = new Random();
                if (ads.Count > 0)
                {
                    list = ads[ran.Next(ads.Count)];
                }
            }
            else
            {
                // id
                list = db.Ad.GetById(number);
            }
            ViewData["Swap"] = swap;
            return await Task.FromResult((IViewComponentResult)View("~/Views/Shared/Components/AdView/AdView.cshtml", list));

        }
    }
}
