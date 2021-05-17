using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace GhasreMobile.ViewComponents.View.ShowProductsIn
{
    public class ShowProductsInView : ViewComponent
    {
        private Core db = new Core();
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            TblProduct selectedProduct = db.Product.GetById(id);
            List<TblProductKeywordRel> keysOfSelectedProduct = db.ProductKeywordRel.Get(i => i.ProductId == id).ToList();
            List<TblProduct> productsResult = new List<TblProduct>();
            List<TblProductKeywordRel> allKeys = new List<TblProductKeywordRel>();
            foreach (TblProductKeywordRel i in keysOfSelectedProduct)
            {
                productsResult.AddRange(db.ProductKeywordRel.Get(j => j.KeywordId == i.KeywordId).Select(i => i.Product).ToList());
            }
            //productsResult.AddRange(selectedProduct.Brand.TblProduct);
            //productsResult.AddRange(selectedProduct.Catagory.TblProduct);
            productsResult = productsResult.Distinct().Where(i => i.ProductId != id).ToList();
            return await Task.FromResult((IViewComponentResult)View("~/Views/Shared/Components/ShowProductsInView/ShowProductsInView.cshtml", productsResult.Take(15)));
        }
    }
}
