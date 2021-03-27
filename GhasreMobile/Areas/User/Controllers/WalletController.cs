using DataLayer.Models;
using DataLayer.ViewModels;
using GhasreMobile.Utilities;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GhasreMobile;

namespace GhasreMobile.Areas.User.Controllers
{
    [Area("User")]
    [PermissionChecker("user,employee,admin")]
    public class WalletController : Controller
    {
        readonly string Domain = "https://localhost:44371";
        //readonly Domain = "https://gasremobile2004.com";


        Core db = new Core();
        TblClient SelectUser()
        {
            int userId = Convert.ToInt32(User.Claims.First().Value);
            TblClient selectUser = db.Client.GetById(userId);
            return selectUser;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                List<TblWallet> list = db.Wallet.Get(i => i.ClientId == SelectUser().ClientId && i.IsFinaly == true).OrderByDescending(i => i.Date).ToList();
                ViewBag.Balance = SelectUser().Balance;
                return await Task.FromResult(View(list));
            }
            catch
            {
                return await Task.FromResult(Redirect("404.html"));
            }
        }

        public async Task<IActionResult> Charge()
        {
           
            try
            {
                return await Task.FromResult(View());
            }
            catch (Exception)
            {
                return await Task.FromResult(Redirect("404.html"));
            }

        }
        public async Task<IActionResult> ChargeWallet(ChargeWalletVm charge)
        {
            try
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
                    addWallet.OrderId = charge.OrderId;
                    db.Wallet.Add(addWallet);
                    db.Wallet.Save();

                    #region Online Payment

                    var payment = new ZarinpalSandbox.Payment((int)charge.Amount);
                    var res = payment.PaymentRequest("شارژ کیف پول", Domain + "/OnlinePayment/" + addWallet.WalletId, "gasremobile2004@gmail.Com", "09357035985");

                    if (res.Result.Status == 100)
                    {
                        return Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + res.Result.Authority);
                    }
                    return null;
                    #endregion
                }
                return await Task.FromResult(View(charge));
            }
            catch (Exception)
            {
                return await Task.FromResult(Redirect("404.html"));
            }
           
        }
    }
}
