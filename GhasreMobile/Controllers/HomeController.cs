using DataLayer.Models;
using DataLayer.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GhasreMobile.Controllers
{
    public class HomeController : Controller
    {
        Core db = new Core();

        public IActionResult Index()
        {
            return View();
        }

        [Route("Contact")]
        public IActionResult Contact()
        {
            return View();
        }
        [Route("/About")]
        public IActionResult About()
        {
            return View();
        }
        [Route("ErrorPage")]
        public async Task<IActionResult> ErrorPage()
        {
            return await Task.FromResult(View());
        }

        [HttpPost]
        [Route("Delivery")]
        public async Task<IActionResult> Delivery(DeliveryVm delivery)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TblDelivery addDelivery = new TblDelivery();
                    addDelivery.Name = delivery.Name;
                    addDelivery.TellNo = delivery.TellNo;
                    addDelivery.Address = delivery.Address;
                    addDelivery.Message = delivery.Message;
                    addDelivery.DateCreated = DateTime.Now;
                    addDelivery.IsAccepted = false;
                    db.Delivery.Add(addDelivery);
                    db.Delivery.Save();
                    return await Task.FromResult(PartialView(delivery));
                }
                return await Task.FromResult(PartialView(delivery));
            }
            catch 
            {
                return await Task.FromResult(Redirect("ErrorPage"));
            }
        }

    }
}
