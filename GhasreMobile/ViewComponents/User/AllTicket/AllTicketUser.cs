using DataLayer.Models;
using DataLayer.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GhasreMobile.ViewComponents.User.AllTicket
{
    public class AllTicketUser : ViewComponent
    {
        Core db = new Core();
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            List<TblTicket> list = db.Ticket.Get(i => i.ClientId == id).ToList();
            return await Task.FromResult((IViewComponentResult)View("~/Areas/User/Views/Shared/Components/AllTicketUser/AllTicketUser.cshtml", list));
        }
    }
}
