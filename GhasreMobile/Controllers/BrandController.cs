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

        [Route("ShowBrand/{id}/{name}")]
        public IActionResult Index(int id, string name = "")
        {
            return View(db.Brand.GetById(id));
        }
    }
}
