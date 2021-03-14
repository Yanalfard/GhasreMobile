using Microsoft.AspNetCore.Mvc;
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
    [PermissionChecker("admin,employee")]
    public class PropertyController : Controller
    {
        Core _core = new Core();
        public IActionResult Index(int page=1)
        {
            IEnumerable<TblProperty> properties = PagingList.Create(_core.Property.Get().OrderByDescending(p=>p.PropertyId), 10, page);
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

        public string Delete(int id)
        {
            TblProperty property = _core.Property.GetById(id);
            if (property.TblProductPropertyRel.Count() == 0)
            {
                return "ویژگی در محصولی استفاده شده است";
            }
            else
            {
                _core.Property.DeleteById(id);
                _core.Property.Save();
                return "true";
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _core.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
