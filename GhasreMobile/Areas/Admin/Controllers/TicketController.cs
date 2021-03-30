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
            IEnumerable<TblTicket> data = _core.Ticket.Get();

            if (SearchInputId != 0)
            {
                data = data.Where(s => s.TicketId == SearchInputId);
            }

            if (!string.IsNullOrEmpty(SearchInputTelNo))
            {
                data = data.Where(s => s.Client.TellNo.Contains(SearchInputTelNo));
            }

            return View(PagingList.Create(data, 40, page));

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
            }
            _core.Ticket.Save();
            TblTicket ticket = new TblTicket();
            ticket.DateSubmited = DateTime.Now;
            ticket.ClientId = ClientId;
            ticket.Title = "Admin";
            ticket.IsAnswer = true;
            ticket.IsAnswerd = true;
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
