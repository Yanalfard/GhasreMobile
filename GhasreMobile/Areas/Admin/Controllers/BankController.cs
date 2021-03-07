using GhasreMobile.Utilities;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Models;
using ReflectionIT.Mvc.Paging;

namespace GhasreMobile.Areas.Admin.Controllers
{
    [Area("Admin")]
    [PermissionChecker("admin")]
    public class BankController : Controller
    {
        Core _core = new Core();

        [HttpGet]
        public IActionResult Index(int page = 1)
        {
            IEnumerable<TblWallet> wallets = PagingList.Create(_core.Wallet.Get().OrderByDescending(o => o.WalletId), 30, page);
            return View(wallets);
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
