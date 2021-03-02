using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Models;

namespace GhasreMobile.ViewComponents.Admin.Product
{
    public class SpecialOfferAddAdmin : ViewComponent
    {
        Core _core = new Core();
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            TblProduct product = _core.Product.GetById(id);
            ViewBag.Product = product.Name;
            ViewBag.ProductId = product.ProductId;
            return await Task.FromResult((IViewComponentResult)View("/Areas/Admin/Views/Product/Components/AddSpecialOffer.cshtml"));
        }
    }
}
