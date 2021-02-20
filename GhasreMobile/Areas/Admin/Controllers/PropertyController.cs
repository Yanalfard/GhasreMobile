﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Services;
using DataLayer.Models;
using ReflectionIT.Mvc.Paging;
using GhasreMobile.Utilities;

namespace GhasreMobile.Areas.Admin.Controllers
{
    [Area("Admin")]
    [PermissionChecker("admin")]
    public class PropertyController : Controller
    {
        Core _core = new Core();
        public IActionResult Index(int page=1)
        {
            IEnumerable<TblProperty> properties = PagingList.Create(_core.Property.Get(), 10, page);
            return View(properties);
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
