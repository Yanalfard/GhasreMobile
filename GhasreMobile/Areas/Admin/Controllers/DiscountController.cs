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
    public class DiscountController : Controller
    {
        Core _core = new Core();
        public IActionResult Index(int page = 1)
        {
            IEnumerable<TblDiscount> discounts = PagingList.Create(_core.Discount.Get(), 30, page);
            return View(discounts);
        }
        public IActionResult Create()
        {
            return ViewComponent("DiscountCreateAdmin");
        }

        public IActionResult Create(TblDiscount discountt, int Till)
        {
            if (ModelState.IsValid)
            {
                TblDiscount Newdiscount = new TblDiscount();
                Newdiscount.Name = discountt.Name;
                Newdiscount.Discount = discountt.Discount;
                Newdiscount.Count = discountt.Count;
                Newdiscount.ValidTill = DateTime.Now.AddDays(Till);
                _core.Discount.Add(Newdiscount);
                _core.Discount.Save();
                return Redirect("/Admin/Discount");
            }
            return View(discountt);
        }

        public IActionResult Edit(int id)
        {
            return ViewComponent("DiscountEditAdmin", new { id = id });
        }

        public IActionResult Edit(TblDiscount Editdiscount, int Till)
        {
            if (ModelState.IsValid)
            {
                _core.Discount.Update(Editdiscount);
                _core.Discount.Save();
                return Redirect("/Admin/Discount");
            }
            return View(Editdiscount);
        }
    }
}
