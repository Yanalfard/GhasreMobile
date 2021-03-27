using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GhasreMobile.Utilities;
using DataLayer.Models;
using Services.Services;
using ReflectionIT.Mvc.Paging;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace GhasreMobile.Areas.Admin.Controllers
{
    [Area("Admin")]
    [PermissionChecker("admin,employee")]
    public class ClientController : Controller
    {
        Core _core = new Core();
        public IActionResult Index(int page = 1, string Name = null, string TelNo = null)
        {
            IEnumerable<TblClient> data = _core.Client.Get();

            if (!string.IsNullOrEmpty(Name))
            {
                data = data.Where(c => c.Name.Contains(Name));
            }

            if (!string.IsNullOrEmpty(TelNo))
            {
                data = data.Where(c => c.TellNo.Contains(TelNo));
            }

            return View(PagingList.Create(data.OrderByDescending(b => b.ClientId), 40, page));


        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            return ViewComponent("ClientEditAdmin", new { id = id });
        }

        [HttpPost]
        public string IsActive(int id)
        {
            TblClient client = _core.Client.GetById(id);
            client.IsActive = !client.IsActive;
            _core.Client.Update(client);
            _core.Client.Save();
            return "true";
        }

        [HttpPost]
        public IActionResult Edit(int ClientId, string Name, int RoleId, int Balance)
        {
            TblClient client = _core.Client.GetById(ClientId);
            client.Name = Name;
            client.RoleId = RoleId;
            client.Balance = Balance;
            if (client.Balance < Balance)
            {
                TblWallet wallet = new TblWallet();
                wallet.IsDeposit = true;
                wallet.ClientId = ClientId;
                wallet.Date = DateTime.Now;
                wallet.Amount = Balance - (int)client.Balance;
                wallet.Description = "شارژ حساب توسط مدیر";
                wallet.IsFinaly = true;
                _core.Wallet.Add(wallet);
                _core.Wallet.Save();
            }
            if (client.Balance > Balance)
            {
                TblWallet wallet = new TblWallet();
                wallet.IsDeposit = false;
                wallet.IsFinaly = true;
                wallet.ClientId = ClientId;
                wallet.Date = DateTime.Now;
                wallet.Amount = (int)client.Balance - Balance;
                wallet.Description = "برداشت از حساب توسط مدیر";
                _core.Wallet.Add(wallet);
                _core.Wallet.Save();
            }
            _core.Client.Update(client);
            _core.Client.Save();
            return Redirect("/Admin/client");
        }

        public async Task<IActionResult> LoginAsUser(int id)
        {

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            TblClient client = _core.Client.GetById(id);

            var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier,client.ClientId.ToString()),
                        new Claim(ClaimTypes.Name,client.TellNo),
                        new Claim(ClaimTypes.Role,_core.Role.GetById(client.RoleId).Name.Trim()),
                    };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            var properties = new AuthenticationProperties
            {
                IsPersistent = false
            };
            await HttpContext.SignInAsync(principal, properties);

            return await Task.FromResult(Redirect("/"));

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _core.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
