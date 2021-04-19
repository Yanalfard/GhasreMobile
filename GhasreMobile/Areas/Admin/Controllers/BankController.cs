using GhasreMobile.Utilities;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Models;
using ReflectionIT.Mvc.Paging;
using System.Globalization;

namespace GhasreMobile.Areas.Admin.Controllers
{
    [Area("Admin")]
    [PermissionChecker("admin")]
    public class BankController : Controller
    {
        Core _core = new Core();

        [HttpGet]
        public IActionResult Index(int page = 1, string startDate = null, string endDate = null)
        {




            if (startDate != null && endDate != null)
            {
                PersianCalendar pc = new PersianCalendar();
                string[] Start = startDate.Split('/');

                DateTime sdte = pc.ToDateTime(Convert.ToInt32(Start[0]), Convert.ToInt32(Start[1]), Convert.ToInt32(Start[2]), 0, 0, 0, 0).Date;

                //////End Date
                string[] End = endDate.Split('/');

                DateTime edte = pc.ToDateTime(Convert.ToInt32(End[0]), Convert.ToInt32(End[1]), Convert.ToInt32(End[2]), 0, 0, 0, 0).Date;

                /////
                IEnumerable<TblWallet> wallets = PagingList.Create(_core.Wallet.Get(b => b.Date.Date >= sdte && b.Date.Date <= edte).OrderByDescending(o => o.WalletId), 60, page);
                return View(wallets);
            }
            else
            {
                IEnumerable<TblWallet> wallets = PagingList.Create(_core.Wallet.Get().OrderByDescending(o => o.WalletId), 30, page);
                return View(wallets);
            }
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
