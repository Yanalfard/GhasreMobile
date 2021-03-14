using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Models;
using Services.Services;
using ReflectionIT.Mvc.Paging;
using GhasreMobile.Utilities;

namespace GhasreMobile.Areas.Admin.Controllers
{
    [Area("Admin")]
    [PermissionChecker("admin,employee")]
    public class BrandController : Controller
    {
        Core _core = new Core();
        public IActionResult Index(int page = 1)
        {
            IEnumerable<TblBrand> brands = PagingList.Create(_core.Brand.Get().OrderByDescending(b => b.BrandId), 30, page);
            return View(brands);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return ViewComponent("CreateBrandAdmin");
        }

        [HttpPost]
        public IActionResult Create(TblBrand brand)
        {
            if (ModelState.IsValid)
            {
                _core.Brand.Add(brand);
                _core.Brand.Save();
                return Redirect("/Admin/Brand");
            }
            return View(brand);
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            return ViewComponent("EditBrandAdmin", new { Id = Id });
        }

        public IActionResult Edit(TblBrand brand)
        {
            if (ModelState.IsValid)
            {
                _core.Brand.Update(brand);
                _core.Brand.Save();
                return Redirect("/Admin/Brand");
            }
            return View(brand);
        }

        public string Delete(int id)
        {
            TblBrand brand = _core.Brand.GetById(id);
            if (brand.TblProduct.Count() == 0)
            {
                return "برند مورد نظر در محصولی استفاده شده است";
            }
            else
            {
                _core.Brand.DeleteById(id);
                _core.Brand.Save();
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
