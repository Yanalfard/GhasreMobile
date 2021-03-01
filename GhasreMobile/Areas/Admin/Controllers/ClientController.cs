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
        public IActionResult Index(int page = 1)
        {
            IEnumerable<TblClient> clients = PagingList.Create(_core.Client.Get(), 30, page);
            return View(clients);
        }

        public IActionResult Edit(int id)
        {
            return ViewComponent("ClientEditAdmin", new { id = id });
        }

        public IActionResult Edit(TblClient client)
        {
            if (ModelState.IsValid)
            {
                _core.Client.Update(client);
                _core.Client.Save();
                return Redirect("/Admin/client");
            }
            return View(client);
        }
    }
}
