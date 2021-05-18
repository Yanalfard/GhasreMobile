using DataLayer.Models;
using GhasreMobile.Utilities;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GhasreMobile.ViewComponents.View.Product
{
    public class ProductView : ViewComponent
    {
        private Core db = new Core();
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<TblBrand> list = new List<TblBrand>();
            list = db.Brand.Get(i => i.TblProduct.Any()).OrderBy(i => i.TblProduct.Count()).OrderByDescending(i => i.TblProduct.Count()).ToList();
            return await Task.FromResult((IViewComponentResult)View("~/Views/Shared/Components/ProductView/ProductView.cshtml", list));
        }
        //public async Task<IViewComponentResult> InvokeAsync(int? id = 0)
        //{
        //    List<TblBrand> list = new List<TblBrand>();
        //    if (id == 0)
        //    {
        //        list = db.Brand.Get(i => i.TblProduct.Any()).OrderBy(i => i.TblProduct.Count()).Skip(1).OrderByDescending(i => i.TblProduct.Count()).ToList();
        //    }
        //    else
        //    {
        //        list = db.Brand.Get(i=>i.TblProduct.Any()).OrderBy(i => i.TblProduct.Count()).Take(1).OrderByDescending(i => i.TblProduct.Count()).ToList();
        //    }
        //    return await Task.FromResult((IViewComponentResult)View("~/Views/Shared/Components/ProductView/ProductView.cshtml", list));
        //}
    }
}
