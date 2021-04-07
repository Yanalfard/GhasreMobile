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
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

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
                    //int Amount = (int)charg.Amount;
                    //return Redirect("/User/Wallet/ChargeWallet?Amount=" + Amount);
                    TblWallet addWallet = new TblWallet();
                    addWallet.Amount = (int)charg.Amount;
                    addWallet.Date = DateTime.Now;
                    addWallet.Description = "شارژ حساب";
                    addWallet.IsDeposit = true;
                    addWallet.IsFinaly = false;
                    addWallet.ClientId = SelectUser().ClientId;
                    addWallet.OrderId = charg.OrderId;
                    db.Wallet.Add(addWallet);
                    db.Wallet.Save();

                    //توضیحات مورد نیاز پرداخت
                    var description = "قصر موبایل";
                    //آدرس برگشت از بانک به سایت
                    var returnurl = Domain + "/ShopCart/OnlinePayment/";
                    //مبلغ پرداخت
                    decimal Price = (int)charg.Amount * 10;
                    //شماره پرداخت به صورت منحصر به فرد 
                    //ایجاد می شود GetUniqueKey این شماره به وسیله تابع
                    long OrderId = addWallet.WalletId;
                    //به منظور فراخوانی تابع پرداخت MellatPayment ارسال اطلاعات به تابع  
                    //MellatPayment(returnurl, description, Price, OrderId);
                    //رفع مشکلات امنیتی
                    BypassCertificateError();
                    //شماره ترمینال اخذ شده از به پرداخت
                    string TerminalId = "5869122";
                    //نام کاربری اخذ شده از به پرداخت
                    string UserName = "gasremobile777";

                    //رمز عبور اخذ شده از به پرداخت
                    string UserPassword = "16986563";

                    Shaparak1.PaymentGatewayClient p = new Shaparak1.PaymentGatewayClient();
                    //فراخوانی تابع درخواست پرداخت

                    var result = await p.bpPayRequestAsync(Int64.Parse(TerminalId), UserName, UserPassword, OrderId, (long)Price, GetDate(), GetTime(), description, returnurl, "0");
                    //بررسی نتیجه برگشتی ار تابع پرداخت
                    //در صورت نول نبودن یعنی فراخوانی انجام شده
                    //در صورت نول بودن یعنی فراخوانی انجام نشده
                    if (result != null)
                    {
                        //پس از فراخوانی صحیح یک رشته از طرف بان ارسال می گررد که حاوی کد ارجاع و کد پیغام از طرف بانک است

                        //است که به صورت زیر تفکیک می گردد A0ds04545sd24545,0 مثلا کد برگشتی به صورت 
                        string res = result.Body.@return.ToString();
                        String[] resultArray = res.Split(',');

                        //در صورتی که کد پیغام 0 باشد یعنی عملیات پرداخت انجام پذیر است و با دستور زیر بررسی می گردد
                        if (int.Parse(res[0].ToString()) == 0)
                        {
                            //ارسال کد ارجاع به تابع جاوا اسکریبت به منظور هدایت به درگاه پرداخت
                            //در صفحه ایندکس نوشته شده است postRefId تابع جاوا اسکریبت
                            ViewBag.jscode = "<script language='javascript' type='text/javascript'> postRefId('" + resultArray[1] + "');</script>";
                            string addres = "https://bpm.shaparak.ir/pgwchannel/startpay.mellat?RefId=" + resultArray[1];
                            return Redirect(addres);
                        }
                        else
                        {
                            //در صورتی که کد برگشتی 0 نباشد این کد به تابع زیر ارسال شده و پیغامی متناسب با آن نمایش داده می شود
                            // ViewBag.message = Infrastructure.BMResult.BMResultText(result);
                            //ViewBag.jscode = "<script language='javascript' type='text/javascript'> postRefId('" + resultArray[1] + "');</script>";

                            ViewBag.message = "<script language='javascript' type='text/javascript'> runerror();</script>";
                        }

                    }
                    else
                    {
                        ViewBag.message = "در حال حاظر امکان اتصال به این درگاه وجود ندارد ";
                    }
                    return await Task.FromResult(View(charg));
                }
                return await Task.FromResult(View(charg));
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

                //توضیحات مورد نیاز پرداخت
                var description = "قصر موبایل";
                //آدرس برگشت از بانک به سایت
                var returnurl = Domain + "/ShopCart/OnlinePayment/";
                //مبلغ پرداخت
                decimal Price = (int)charge.Amount * 10;
                //شماره پرداخت به صورت منحصر به فرد 
                //ایجاد می شود GetUniqueKey این شماره به وسیله تابع
                long OrderId = addWallet.WalletId;
                //به منظور فراخوانی تابع پرداخت MellatPayment ارسال اطلاعات به تابع  
                //MellatPayment(returnurl, description, Price, OrderId);
                //رفع مشکلات امنیتی
                BypassCertificateError();
                //شماره ترمینال اخذ شده از به پرداخت
                string TerminalId = "5869122";
                //نام کاربری اخذ شده از به پرداخت
                string UserName = "gasremobile777";

                //رمز عبور اخذ شده از به پرداخت
                string UserPassword = "16986563";

                Shaparak1.PaymentGatewayClient p = new Shaparak1.PaymentGatewayClient();
                //فراخوانی تابع درخواست پرداخت

                var result = await p.bpPayRequestAsync(Int64.Parse(TerminalId), UserName, UserPassword, OrderId, (long)Price, GetDate(), GetTime(), description, returnurl, "0");
                //بررسی نتیجه برگشتی ار تابع پرداخت
                //در صورت نول نبودن یعنی فراخوانی انجام شده
                //در صورت نول بودن یعنی فراخوانی انجام نشده
                if (result != null)
                {
                    //پس از فراخوانی صحیح یک رشته از طرف بان ارسال می گررد که حاوی کد ارجاع و کد پیغام از طرف بانک است

                    //است که به صورت زیر تفکیک می گردد A0ds04545sd24545,0 مثلا کد برگشتی به صورت 
                    string res = result.Body.@return.ToString();
                    String[] resultArray = res.Split(',');

                    //در صورتی که کد پیغام 0 باشد یعنی عملیات پرداخت انجام پذیر است و با دستور زیر بررسی می گردد
                    if (int.Parse(res[0].ToString()) == 0)
                    {
                        //ارسال کد ارجاع به تابع جاوا اسکریبت به منظور هدایت به درگاه پرداخت
                        //در صفحه ایندکس نوشته شده است postRefId تابع جاوا اسکریبت
                        ViewBag.jscode = "<script language='javascript' type='text/javascript'> postRefId('" + resultArray[1] + "');</script>";
                        string addres = "https://bpm.shaparak.ir/pgwchannel/startpay.mellat?RefId=" + resultArray[1];
                        return Redirect(addres);
                    }
                    else
                    {
                        //در صورتی که کد برگشتی 0 نباشد این کد به تابع زیر ارسال شده و پیغامی متناسب با آن نمایش داده می شود
                        // ViewBag.message = Infrastructure.BMResult.BMResultText(result);
                        //ViewBag.jscode = "<script language='javascript' type='text/javascript'> postRefId('" + resultArray[1] + "');</script>";

                        ViewBag.message = "<script language='javascript' type='text/javascript'> runerror();</script>";
                    }

                }
                else
                {
                    ViewBag.message = "در حال حاظر امکان اتصال به این درگاه وجود ندارد ";
                }
                return await Task.FromResult(RedirectToAction("Charge"));
            }
            catch (Exception)
            {
                return await Task.FromResult(Redirect("404.html"));
            }

        }
        #region // تابع رفع پیغام های امنیتی
        void BypassCertificateError()
        {
            ServicePointManager.ServerCertificateValidationCallback +=
                delegate (
                    Object sender1,
                    X509Certificate certificate,
                    X509Chain chain,
                    SslPolicyErrors sslPolicyErrors)
                {
                    return true;
                };
        }
        #endregion

        #region // تابع دریافت تاریخ
        protected string GetDate()
        {
            return DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString().PadLeft(2, '0') +
                   DateTime.Now.Day.ToString().PadLeft(2, '0');
        }
        #endregion

        #region // تابع دریافت زمان
        protected string GetTime()
        {
            return DateTime.Now.Hour.ToString().PadLeft(2, '0') + DateTime.Now.Minute.ToString().PadLeft(2, '0') +
                   DateTime.Now.Second.ToString().PadLeft(2, '0');
        }
        #endregion

        #region // تابع دریافت آدرس سرور بانک ملت
        protected string GetPgwSite()
        {
            return "https://bpm.shaparak.ir/pgwchannel/startpay.mellat";
        }
        #endregion
        #region // تابع فراخوانی درخواست پرداخت بانک
        protected async void MellatPayment(string returnurl, string description, decimal Price, long OrderId)
        {
            try
            {
                //رفع مشکلات امنیتی
                BypassCertificateError();
                //شماره ترمینال اخذ شده از به پرداخت
                string TerminalId = "5869122";
                //نام کاربری اخذ شده از به پرداخت
                string UserName = "gasremobile777";

                //رمز عبور اخذ شده از به پرداخت
                string UserPassword = "16986563";

                Shaparak1.PaymentGatewayClient p = new Shaparak1.PaymentGatewayClient();
                //فراخوانی تابع درخواست پرداخت

                var result = await p.bpPayRequestAsync(Int64.Parse(TerminalId), UserName, UserPassword, OrderId, (long)Price, GetDate(), GetTime(), description, returnurl, "0");
                //بررسی نتیجه برگشتی ار تابع پرداخت
                //در صورت نول نبودن یعنی فراخوانی انجام شده
                //در صورت نول بودن یعنی فراخوانی انجام نشده
                if (result != null)
                {
                    //پس از فراخوانی صحیح یک رشته از طرف بان ارسال می گررد که حاوی کد ارجاع و کد پیغام از طرف بانک است

                    //است که به صورت زیر تفکیک می گردد A0ds04545sd24545,0 مثلا کد برگشتی به صورت 
                    string res = result.Body.@return.ToString();
                    String[] resultArray = res.Split(',');

                    //در صورتی که کد پیغام 0 باشد یعنی عملیات پرداخت انجام پذیر است و با دستور زیر بررسی می گردد
                    if (int.Parse(res[0].ToString()) == 0)
                    {
                        //ارسال کد ارجاع به تابع جاوا اسکریبت به منظور هدایت به درگاه پرداخت
                        //در صفحه ایندکس نوشته شده است postRefId تابع جاوا اسکریبت
                        ViewBag.jscode = "<script language='javascript' type='text/javascript'> postRefId('" + resultArray[1] + "');</script>";
                    }
                    else
                    {
                        //در صورتی که کد برگشتی 0 نباشد این کد به تابع زیر ارسال شده و پیغامی متناسب با آن نمایش داده می شود
                        // ViewBag.message = Infrastructure.BMResult.BMResultText(result);
                        //ViewBag.jscode = "<script language='javascript' type='text/javascript'> postRefId('" + resultArray[1] + "');</script>";

                        //  ViewBag.message = "خطا در ارسال به درگاه";
                        ViewBag.message = "<script language='javascript' type='text/javascript'> runerror();</script>";
                    }

                }
                else
                {
                    ViewBag.message = "در حال حاظر امکان اتصال به این درگاه وجود ندارد ";
                }


            }
            catch (Exception exp)
            {
                ViewBag.message = "Error: " + exp.Message;
            }
        }
        #endregion
    }
}