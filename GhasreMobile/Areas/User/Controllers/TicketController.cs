using DataLayer.Models;
using DataLayer.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GhasreMobile.Areas.User.Controllers
{
    [Area("User")]
    public class TicketController : Controller
    {
        Core db = new Core();
        TblClient SelectUser()
        {
            int userId = Convert.ToInt32(User.Claims.First().Value);
            TblClient selectUser = db.Client.GetById(userId);
            return selectUser;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> AllTicket()
        {
            return await Task.FromResult(ViewComponent("AllTicketUser", new { id = SelectUser().ClientId }));
        }
        [HttpPost]
        public async Task<IActionResult> SendTicket(SendTicketVm sendTicket)
        {
            if (ModelState.IsValid)
            {
                TblTicket addTicket = new TblTicket();
                addTicket.Title = sendTicket.Title;
                addTicket.Body= sendTicket.Body;
                addTicket.DateSubmited= DateTime.Now;
                addTicket.ClientId= SelectUser().ClientId;
                db.Ticket.Add(addTicket);
                db.Ticket.Save();
                return await Task.FromResult(RedirectToAction("Index"));

            }
            return await Task.FromResult(View(sendTicket));
        }
    }
}
