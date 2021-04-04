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
        // readonly string Domain = "https://localhost:44371";
        readonly string Domain = "https://gasremobile2004.com";


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

        public async Task<IActionResult> Charg()
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
        [HttpPost]
        public async Task<IActionResult> Charg(ChargeWalletVm charg)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int Amount = (int)charg.Amount;
                    return Redirect("/User/Wallet/ChargeWallet?Amount=" + Amount);
                }
                return await Task.FromResult(View(charg));
            }
            catch (Exception)
            {
                return await Task.FromResult(Redirect("404.html"));
            }

        }

        public string SetDefaultDate()
        {
            return DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Day.ToString().PadLeft(2, '0');

        }
        public string SetDefaultTime()
        {
            return DateTime.Now.Hour.ToString().PadLeft(2, '0') + DateTime.Now.Minute.ToString().PadLeft(2, '0') + DateTime.Now.Second.ToString().PadLeft(2, '0');
        }
        public static readonly string CallBackUrl = "https://gasremobile2004.com/OnlinePayment/";
        public static readonly string TerminalId = "5869122";
        public static readonly string UserName = "gasremobile777";
        public static readonly string UserPassword = "16986563";
        public async Task<IActionResult> ChargeWallet(ChargeWalletVm charge)
        {
            try
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

                #region OnlinePayment
                //var payment = new ZarinpalSandbox.Payment((int)charge.Amount);
                //var res = payment.PaymentRequest("شارژ کیف پول", Domain + "/OnlinePayment/" + addWallet.WalletId, "gasremobile2004@gmail.Com", "09357035985");

                //if (res.Result.Status == 100)
                //{
                //    return Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + res.Result.Authority);
                //}
                #endregion


                #region Shaparak
                // var payment = new Shaparak1.PaymentGatewayClient();
                // string c = payment.ToString();
                // var result = await payment.bpPayRequestAsync(5869122, "gasremobile777", "16986563", addWallet.WalletId, (int)charge.Amount, $"{addWallet.Date.Year}/{addWallet.Date.Month}/{addWallet.Date.Day}", $"{addWallet.Date.Hour}:{addWallet.Date.Minute}:{addWallet.Date.Second}", "پرداخت قصر موبایل", Domain + "/OnlinePayment/", 0);
                // var result = await payment.bpPayRequestAsync(5869122, "gasremobile777", "16986563", addWallet.WalletId, (int)charge.Amount, SetDefaultDate(), SetDefaultTime(), "پرداخت قصر موبایل", Domain + "/OnlinePayment/", addWallet.ClientId.ToString());


                Shaparak1.PaymentGatewayClient bp = new Shaparak1.PaymentGatewayClient();
                int number = 0;
                var result =await bp.bpPayRequestAsync(Int64.Parse(TerminalId), UserName, UserPassword, long.Parse(addWallet.WalletId.ToString()), long.Parse(charge.Amount.ToString()), SetDefaultDate(), SetDefaultTime(), "1000File.com", CallBackUrl, number.ToString());
                



                //string CallBackUrl = Domain + "/OnlinePayment/";
                //Shaparak1.PaymentGatewayClient bp = new Shaparak1.PaymentGatewayClient();
                //var result = await bp.bpPayRequestAsync(5869122, "gasremobile777", "16986563", addWallet.WalletId, (int)charge.Amount, SetDefaultDate(), SetDefaultTime(), "1000File.com", CallBackUrl, "0");
                //string[] res = result.ToString().Split(',');
                //if (res[0] == "0")
                //{
                //    //db.UpdatePayment("پرداخت نشده", res[1], "", payment_id.Value);
                //    ViewBag.jscode = "<script>postRefId('" + res[1] + "')</script>";
                //}
                //else
                //{
                //    ViewBag.message = "خطای " + res[0] + " در ارتباط با بانک";
                //}

                #endregion

                return await Task.FromResult(RedirectToAction("Charge"));
            }
            catch (Exception)
            {
                return await Task.FromResult(Redirect("404.html"));
            }

        }
    }
}
