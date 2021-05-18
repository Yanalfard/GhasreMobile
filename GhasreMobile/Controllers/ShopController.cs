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
    public class ShopController : ControllerBase
    {
        Core db = new Core();

        // GET: api/Shop
        [HttpGet]
        public int Get()
        {
            List<ShopCartItem> list = new List<ShopCartItem>();
            var sessions = HttpContext.Session.GetComplexData<List<ShopCartItem>>("ShopCart");
            if (sessions != null)
            {
                list = sessions as List<ShopCartItem>;
            }
            int id = list.Sum(l => l.Count);
            return list.Sum(l => l.Count);
        }
        // GET: api/Shop/5
        [HttpGet("{id}/{colorId}")]
        public int Get(int id, int colorId)
        {
            TblColor selectedProduct = db.Color.GetById(colorId);
            TblProduct selectedP = db.Product.GetById(id);
            if (selectedP.TblColor.Any(i => i.ColorId == colorId))
            {
                if (selectedP.PriceBeforeDiscount > 0)
                {
                    if (selectedProduct.Count > 0)
                    {
                        List<ShopCartItem> list = new List<ShopCartItem>();
                        var sessions = HttpContext.Session.GetComplexData<List<ShopCartItem>>("ShopCart");
                        if (sessions != null)
                        {
                            list = sessions as List<ShopCartItem>;
                        }
                        if (list.Any(p => p.ColorID == colorId))
                        {
                            int index = list.FindIndex(p => p.ColorID == colorId);
                            if (selectedProduct != null)
                            {
                                int count = selectedProduct.Count - list[index].Count;
                                if (count > 0 && selectedProduct.ProductId == id && selectedProduct.ColorId == colorId)
                                {
                                    list[index].Count += 1;
                                }
                            }
                        }
                        else
                        {
                            list.Add(new ShopCartItem()
                            {
                                ProductID = id,
                                ColorID = colorId,
                                Count = 1
                            });
                        }
                        HttpContext.Session.SetComplexData("ShopCart", list);
                    }
                }
            }

            return Get();
        }


    }
}
