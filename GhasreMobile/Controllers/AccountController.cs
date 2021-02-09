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

    }
}
