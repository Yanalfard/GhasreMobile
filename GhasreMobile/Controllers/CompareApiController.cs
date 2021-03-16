using DataLayer.Models;
using DataLayer.ViewModels;
using GhasreMobile.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace GhasreMobile.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompareApiController : ControllerBase
    {
        Core db = new Core();

        // GET: api/Shop
        [HttpGet]
        public int Get()
        {

            List<CompareItemVm> list = new List<CompareItemVm>();
            var Session = HttpContext.Session.GetComplexData<List<CompareItemVm>>("Compare");
            if (Session != null)
            {
                list = Session as List<CompareItemVm>;
            }
            return list.Count;
        }
        // GET: api/Shop/5
        [HttpGet("{id}")]
        public int Get(int id)
        {
            List<CompareItemVm> list = new List<CompareItemVm>();
            var Session = HttpContext.Session.GetComplexData<List<CompareItemVm>>("Compare");
            if (Session != null)
            {
                list = Session as List<CompareItemVm>;
            }
            if (!list.Any(p => p.ProductID == id))
            {
                var product = db.Product.Get(p => p.ProductId == id).Select(p => new { p.Name, p.MainImage, p.PriceAfterDiscount, p.PriceBeforeDiscount, p.TblColor,p.TblSpecialOffer }).Single();
                list.Add(new CompareItemVm()
                {
                    ProductID = id,
                    Name = product.Name,
                    ImageName = product.MainImage,
                    Brand = product.MainImage,
                    PriceBeforeDiscount = product.PriceBeforeDiscount,
                    PriceAfterDiscount = product.PriceAfterDiscount,
                    SumProduct = product.TblColor.Sum(i => i.Count),
                    SpecialOffer = product.TblSpecialOffer.Count > 0 && product.TblSpecialOffer.SingleOrDefault().ValidTill >= DateTime.Now ? true : false,
                    SpecialOfferDiscount = product.TblSpecialOffer.Count > 0 && product.TblSpecialOffer.SingleOrDefault().ValidTill >= DateTime.Now ? (int)product.TblSpecialOffer.SingleOrDefault().Discount : 0,
                });
            }
            HttpContext.Session.SetComplexData("Compare", list);
            return Get();
        }


    }
}
