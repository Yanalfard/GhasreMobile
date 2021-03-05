using DataLayer.Models;
using DataLayer.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GhasreMobile.ViewComponents.User.CheckDiscount
{
    public class CheckDiscountUser : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            DiscountVm list = new DiscountVm();
            return await Task.FromResult((IViewComponentResult)View("~/Areas/User/Views/Shared/Components/CheckDiscountUser/CheckDiscountUser.cshtml", list));
        }
    }
}
