﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Models;
using ReflectionIT.Mvc.Paging;
using Services.Services;

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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TblProduct product,List<string> Color)
        {
            if (ModelState.IsValid)
            {

            }
            return View(product);
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
