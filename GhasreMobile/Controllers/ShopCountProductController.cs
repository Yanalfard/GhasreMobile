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
    public class ShopCountProductController : ControllerBase
    {
        // GET: api/Shop/5
        [HttpGet("{id}")]
        public int Get(int id)
        {
            List<ShopCartItem> list = new List<ShopCartItem>();
            var sessions = HttpContext.Session.GetComplexData<List<ShopCartItem>>("ShopCart");
            if (sessions != null)
            {
                list = sessions as List<ShopCartItem>;
            }
            if (list.Any(p => p.ColorID == id))
            {
                int index = list.FindIndex(p => p.ColorID == id);
                return list[index].Count;
            }
            else
            {
                return 0;
            }
        }

    }
}
