using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GhasreMobile.Utilities;
using DataLayer.Models;
using ReflectionIT.Mvc.Paging;
using Services.Services;

namespace GhasreMobile.Areas.Admin.Controllers
{
    [Area("Admin")]
    [PermissionChecker("admin,employee")]
    public class TicketController : Controller
    {
        Core _core = new Core();
        public IActionResult Index(int page = 1, int SearchInputId = 0, string SearchInputTelNo = null)
        {

            if (SearchInputId != 0)
            {
                IEnumerable<TblTicket> SearchIdtickets = PagingList.Create(_core.Ticket.Get(s => s.TicketId == SearchInputId), 40, page);
                return View(SearchIdtickets);
            }

            if (!string.IsNullOrEmpty(SearchInputTelNo))
            {
                IEnumerable<TblTicket> SearchTelNo = PagingList.Create(_core.Ticket.Get(t => t.Client.TellNo.Contains(SearchInputTelNo)), 40, page);
                return View(SearchInputTelNo);
            }

            IEnumerable<TblTicket> tickets = PagingList.Create(_core.Ticket.Get().OrderByDescending(t => t.TicketId), 40, page);
            return View(tickets);

        }

        public IActionResult InnerTicket(int id)
        {
            return View(_core.Ticket.Get(t => t.ClientId == id));
        }

        public IActionResult SendMassage(int ClientId, string Body)
        {
            IEnumerable<TblTicket> tickets = _core.Ticket.Get(t => t.ClientId == ClientId);
            foreach (var item in tickets)
            {
                TblTicket ticketuser = _core.Ticket.GetById(item.TicketId);
                ticketuser.IsAnswerd = true;
                _core.Ticket.Update(ticketuser);
                _core.Ticket.Save();
            }
            TblTicket ticket = new TblTicket();
            ticket.DateSubmited = DateTime.Now;
            ticket.ClientId = ClientId;
            ticket.Title = "Admin";
            ticket.IsAnswer = true;
            ticket.IsAnswerd = false;
            ticket.Body = Body;
            _core.Ticket.Add(ticket);
            _core.Ticket.Save();
            return Redirect("/Admin/Ticket/InnerTicket/" + ClientId);
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
