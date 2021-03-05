﻿using DataLayer.Models;
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
        public async Task<IViewComponentResult> InvokeAsync()
        {
            TblSpecialOffer list = new TblSpecialOffer();

            List<TblSpecialOffer> offer = db.SpecialOffer.Get().ToList();
            Random ran = new Random();
            if (offer.Count > 0)
            {
                list = offer[ran.Next(offer.Count)];
            }
            return await Task.FromResult((IViewComponentResult)View("~/Views/Shared/Components/SpecialOfferViem/SpecialOfferViem.cshtml", list));
        }
    }
}