using DataLayer.Models;
using DataLayer.Security;
using DataLayer.Utilities;
using DataLayer.ViewModels;
using GoogleReCaptcha.V3.Interface;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GhasreMobile.Controllers
{
    public class AccountController : Controller
    {
        Core db = new Core();
        private readonly ICaptchaValidator _captchaValidator;

        public AccountController(ICaptchaValidator captchaValidator)
        {
            _captchaValidator = captchaValidator;
        }

        // Login
        [Route("Login")]
        public IActionResult Login()
        {
            return View();
        }
        [Route("Login")]
        [HttpPost]
        public IActionResult LoginAsync(LoginVm login)
        {
            if (ModelState.IsValid)
            {

            }
            return View(login);
        }
        // Register
        [Route("Register")]
        public async Task<IActionResult> Register()
        {
            return await Task.FromResult(View());
        }
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> RegisterAsync(RegisterVm register)
        {
            try
            {
                if (!await _captchaValidator.IsCaptchaPassedAsync(register.Captcha))
                {
                    ModelState.AddModelError("TellNo", "ورود غیر مجاز");
                    return View(register);
                }
                if (ModelState.IsValid)
                {
                    if (!db.Client.Get().Any(i => i.TellNo == register.TellNo))
                    {
                        var CodeCreator = Guid.NewGuid().ToString();
                        string Code = CodeCreator.Substring(CodeCreator.Length - 5);
                        TblClient addUser = new TblClient();
                        addUser.DateCreated = DateTime.Now;
                        addUser.Auth = Code;
                        addUser.RoleId = 1;
                        addUser.Balance = 0;
                        addUser.IsActive = false;
                        addUser.Password = PasswordHelper.EncodePasswordMd5(register.Password);
                        addUser.TellNo = register.TellNo;
                        db.Client.Add(addUser);
                        db.Client.Save();
                        string message = addUser.Auth;
                        await Sms.SendSms(addUser.TellNo, message, "GhasrMobileRegister");
                        return await Task.FromResult(Redirect("/Account/Validation?tellNo=" + addUser.TellNo));
                    }
                    else
                    {
                        ModelState.AddModelError("TellNo","شماره تلفن تکراریست");
                    }
                }
                return await Task.FromResult(View(register));
            }
            catch (Exception)
            {
                return await Task.FromResult(Redirect("ErrorPage"));
            }

        }
        public IActionResult ForgotPassword()
        {
            return View();
        }

        public IActionResult ChangePassword()
        {
            return View();
        }

        public IActionResult Verify()
        {
            return View();
        }

    }
}
