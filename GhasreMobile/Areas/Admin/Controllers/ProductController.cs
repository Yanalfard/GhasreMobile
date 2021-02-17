using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Models;
using DataLayer.ViewModels;
using ReflectionIT.Mvc.Paging;
using Services.Services;
using Microsoft.AspNetCore.Http;

namespace GhasreMobile.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        Core _core = new Core();
        [HttpGet]
        public IActionResult Index(int page = 1)
        {
            IEnumerable<TblProduct> products = PagingList.Create(_core.Product.Get(c => !c.IsDeleted), 10, page);
            return View(products);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Parentcatagories = _core.Catagory.Get(c => c.ParentId == null);
            ViewBag.Brands = _core.Brand.Get();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public string Create(TblProduct product,List<string> Keyword,List<IFormFile> Gallery,List<CreatePropertyVm> Properties)
        {
            if (ModelState.IsValid)
            {
                
            }
            ViewBag.Parentcatagories = _core.Catagory.Get(c => c.ParentId == null);
            ViewBag.Brands = _core.Brand.Get();
            return "لطفا دسته بندی را وارد کنید";
        }


        public IActionResult ProperyList()
        {
            return ViewComponent("ProperyListAdmin");
        }

        public IActionResult Stock()
        {
            return ViewComponent("EditStokeAdmin");
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TblProduct product)
        {
            if (ModelState.IsValid)
            {

            }
            return View(product);
        }

    }
}
