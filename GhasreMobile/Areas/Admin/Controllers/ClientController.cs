using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GhasreMobile.Utilities;
using DataLayer.Models;
using Services.Services;
using ReflectionIT.Mvc.Paging;

namespace GhasreMobile.Areas.Admin.Controllers
{
    [Area("Admin")]
    [PermissionChecker("admin")]
    public class ClientController : Controller
    {
        Core _core = new Core();
        public IActionResult Index(int page = 1, string Name = null, string TelNo = null)
        {
            if (!string.IsNullOrEmpty(Name))
            {
                IEnumerable<TblClient> clients = PagingList.Create(_core.Client.Get(c => c.Name.Contains(Name)), 40, page);
                return View(clients);
            }

            if (!string.IsNullOrEmpty(TelNo))
            {
                IEnumerable<TblClient> clients = PagingList.Create(_core.Client.Get(c => c.Name.Contains(Name)), 40, page);
                return View(clients);
            }
            else
            {
                IEnumerable<TblClient> clients = PagingList.Create(_core.Client.Get().OrderByDescending(b => b.ClientId), 40, page);
                return View(clients);
            }

        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            return ViewComponent("ClientEditAdmin", new { id = id });
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
