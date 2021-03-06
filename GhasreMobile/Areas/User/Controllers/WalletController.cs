﻿using DataLayer.Models;
using DataLayer.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GhasreMobile.Areas.User.Controllers
{
    [Area("User")]
    public class WalletController : Controller
    {
        Core db = new Core();
        TblClient SelectUser()
        {
            int userId = Convert.ToInt32(User.Claims.First().Value);
            TblClient selectUser = db.Client.GetById(userId);
            return selectUser;
        }
        public IActionResult Index()
        {
            ViewBag.Wallet = db.Wallet.Get(i => i.ClientId == SelectUser().ClientId);
            return View(SelectUser().Balance);
        }

        public IActionResult Charge()
        {
            return View();
        }
        public IActionResult ChargeWallet(ChargeWalletVm charge)
        {
            if (ModelState.IsValid)
            {
                TblWallet addWallet = new TblWallet();
                addWallet.Amount = (int)charge.Amount;
                addWallet.Date = DateTime.Now;
                addWallet.Description = "شارژ حساب";
                addWallet.IsDeposit = true;
                addWallet.IsFinaly = false;
                addWallet.ClientId = SelectUser().ClientId;
                addWallet.OrderId = addWallet.OrderId;
                db.Wallet.Add(addWallet);
                db.Wallet.Save();

                #region Online Payment

                var payment = new ZarinpalSandbox.Payment((int)charge.Amount);
                var res = payment.PaymentRequest("شارژ کیف پول", "https://localhost:44371/OnlinePayment/" + addWallet.WalletId, "Info@topLearn.Com", "09197070750");

                if (res.Result.Status == 100)
                {
                    return Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + res.Result.Authority);
                }
                return null;
                #endregion
            }
            return View(charge);
        }
    }
}
