using DataLayer.Models;
using DataLayer.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GhasreMobile.ViewComponents.View.SpecialOffer
{
    public class SpecialOfferViem : ViewComponent
    {
        private Core db = new Core();
        public async Task<IViewComponentResult> InvokeAsync(string swap, int number = -1)
        {
        
            TblSpecialOffer list = new TblSpecialOffer();
            if (number == -1)
            {
                List<TblSpecialOffer> offer = db.SpecialOffer.Get(i => i.ValidTill > DateTime.Now && i.Product.IsDeleted == false && i.Product.TblColor.Sum(i => i.Count) > 0).ToList();
                Random ran = new Random();
                if (offer.Count > 0)
                {
                    list = offer[ran.Next(offer.Count)];
                }
            }
            else
            {
                // id
                list = db.SpecialOffer.GetById(number);
            }
            ViewData["swap"] = swap;
            return await Task.FromResult((IViewComponentResult)
                View("~/Views/Shared/Components/SpecialOfferViem/SpecialOfferViem.cshtml", list));
        }
    }
}
