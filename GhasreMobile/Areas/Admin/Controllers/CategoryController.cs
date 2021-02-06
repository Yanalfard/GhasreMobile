using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Models;
using Services.Services;
namespace GhasreMobile.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        Core _core = new Core();
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create(int Id)
        {
            return ViewComponent("CreateCategory", new { Id = Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TblCatagory catagory)
        {
            if (ModelState.IsValid)
            {
                _core.Catagory.Add(catagory);
                _core.Catagory.Save();
                return Redirect("/Admin/Category");
            }
            return View(catagory);
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            return ViewComponent("EditCategory", new { Id = Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TblCatagory catagory)
        {
            if (ModelState.IsValid)
            {
                _core.Catagory.Update(catagory);
                _core.Catagory.Save();
                return Redirect("/Admin/Category");
            }
            return View(catagory);
        }

        [HttpGet]
        public IActionResult Child()
        {
            return View();
        }
    }
}
