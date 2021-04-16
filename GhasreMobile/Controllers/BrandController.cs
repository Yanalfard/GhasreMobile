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
        readonly static int GlobalTake = 12;


        [Route("ShowBrand/{id=0}/{name?}/{pageId=1}")]
        public async Task<IActionResult> Index(int id = 0, string name = "", int pageId = 1)
        {
            try
            {
                ViewData["name"] = name;
                ViewData["id"] = id;
                List<TblProduct> list = new List<TblProduct>();
                TblBrand selectedBrand = db.Brand.GetById(id);
                if (selectedBrand != null)
                {
                    list = selectedBrand.TblProduct.Where(i=>i.IsDeleted==false).ToList();
                }
                else
                {
                    list = db.Product.Get(i => i.IsDeleted == false).ToList();
                }
                int take = GlobalTake;
                int skip = (pageId - 1) * take;
                ViewBag.PageCount = list.Count() / take;
                return await Task.FromResult(View(list.OrderByDescending(i => i.TblColor.Sum(i => i.Count)).Skip(skip).Take(take)));

            }
            catch
            {
                return await Task.FromResult(Redirect("404.html"));
            }
        }
        public async Task<IActionResult> ScrollBrand(int id = 0, string name = "", int pageId = 1)
        {
            try
            {
                ViewData["name"] = name;
                ViewData["id"] = id;
                List<TblProduct> list = new List<TblProduct>();
                TblBrand selectedBrand = db.Brand.GetById(id);
                if (selectedBrand != null)
                {
                    list = selectedBrand.TblProduct.ToList();
                }
                else
                {
                    list = db.Product.Get().ToList();
                }
                int take = GlobalTake;
                int skip = (pageId - 1) * take;
                ViewBag.PageCount = list.Count() / take;
                return await Task.FromResult(PartialView(list.OrderByDescending(i => i.TblColor.Sum(i => i.Count)).Skip(skip).Take(take)));

            }
            catch
            {
                return await Task.FromResult(Redirect("404.html"));
            }

        }
    }
}
