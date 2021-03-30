using DataLayer.Models;
using DataLayer.Security;
using DataLayer.Utilities;
using DataLayer.ViewModels;
using GhasreMobile.Utilities;
using GoogleReCaptcha.V3.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Session;
using System.Globalization;

namespace GhasreMobile.Controllers
{
    public class BrandController : Controller
    {
        private Core db = new Core();
        readonly static int GlobalTake = 2;


        [Route("ShowBrand/{id}/{name?}/{pageId=1}")]
        public async Task<IActionResult> Index(int id, string name = "", int pageId = 1)
        {
            ViewData["name"] = name;
            ViewData["id"] = id;
            int take = GlobalTake;
            int skip = (pageId - 1) * take;
            List<TblProduct> list = db.Brand.GetById(id).TblProduct.ToList();
            ViewBag.PageCount = list.Count() / take;
            return await Task.FromResult(View(list.OrderByDescending(i => i.TblColor.Sum(i => i.Count)).Skip(skip).Take(take)));
        }
        [Route("ScrollBrand/{id}/{name?}/{pageId=1}")]
        public async Task<IActionResult> ScrollBrand(int id, string name = "", int pageId = 1)
        {
            ViewData["name"] = name;
            ViewData["id"] = id;
            int take = GlobalTake;
            int skip = (pageId - 1) * take;
            List<TblProduct> list = db.Brand.GetById(id).TblProduct.ToList();
            ViewBag.PageCount = list.Count() / take;
            return await Task.FromResult(View(list.OrderByDescending(i => i.TblColor.Sum(i => i.Count)).Skip(skip).Take(take)));
        }
    }
}
