using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GhasreMobile.Controllers
{
    public class AccountController : Controller
    {
        // Login
        public IActionResult Index()
        {
            return View();
        }

        // Register
        public IActionResult SignUp()
        {
            return View();
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
