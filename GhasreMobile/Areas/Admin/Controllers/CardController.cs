﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Models;
using Services.Services;

namespace GhasreMobile.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CardController : Controller
    {
        Core _core = new Core();
        public IActionResult Index()
        {
            return View(_core.BankAccounts.Get());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return ViewComponent("CardCreateAdmin");
        }

        [HttpPost]
        public IActionResult Create(TblBankAccounts bankAccount)
        {
            if (ModelState.IsValid)
            {
                _core.BankAccounts.Add(bankAccount);
                _core.BankAccounts.Save();
                return Redirect("/Admin/Card");
            }
            return View(bankAccount);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            return ViewComponent("CardEditAdmin", id);
        }

        [HttpPost]
        public IActionResult Edit(TblBankAccounts bankAccount)
        {
            if (ModelState.IsValid)
            {
                _core.BankAccounts.Update(bankAccount);
                _core.BankAccounts.Save();
                return Redirect("/Admin/Card");
            }
            return View(bankAccount);
        }

        [HttpPost]
        public void ChangeStatus(int id)
        {
            TblBankAccounts bankAccounts = _core.BankAccounts.GetById(id);
            bankAccounts.IsActive = !bankAccounts.IsActive;
            _core.BankAccounts.Update(bankAccounts);
            _core.BankAccounts.Save();
        }
    }
}
