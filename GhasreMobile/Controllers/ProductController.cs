using DataLayer.Models;
using DataLayer.Security;
using DataLayer.Utilities;
using DataLayer.ViewModels;
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

namespace GhasreMobile.Controllers
{
    public class ProductController : Controller
    {
        private Core db = new Core();
        [Route("Product/{id}/{name}")]
        public async Task<IActionResult> Index(int id,string name)
        {
            try
            {
                return await Task.FromResult(View(db.Product.GetById(id)));
            }
            catch
            {
                return await Task.FromResult(Redirect("ErrorPage"));
            }
        }
    }
}
