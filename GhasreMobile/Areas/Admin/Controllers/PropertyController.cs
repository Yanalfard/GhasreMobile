using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Services;
using DataLayer.Models;

namespace GhasreMobile.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PropertyController : Controller
    {
        Core _core = new Core();
        public IActionResult Index()
        {
            return View();
        }

        public string Create(string Name)
        {
            if (_core.Property.Get().Any(p => p.Name == Name))
            {
                return "ویژگی تکراری میباشد";
            }
            else
            {
                TblProperty property = new TblProperty();
                property.Name = Name;
                _core.Property.Add(property);
                _core.Property.Save();
                return "true";
            }
        }
    }
}
