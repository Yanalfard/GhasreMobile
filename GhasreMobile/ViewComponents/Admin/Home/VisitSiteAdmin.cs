using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Services;
using DataLayer.ViewModels;
using DataLayer.Models;

namespace GhasreMobile.ViewComponents.Admin.Home
{
    public class VisitSiteAdmin : ViewComponent
    {
        Core _core = new Core();
        public async Task<IViewComponentResult> InvokeAsync()
        {
            DateTime dtNow = DateTime.Now.Date;
            DateTime yesterday = dtNow.AddDays(-1).Date;
            List<TblVisit> selectAllVisit = _core.Visit.Get().ToList();
            VisitSiteVm visit = new VisitSiteVm();
            visit.VisitSum = selectAllVisit.Count();
            visit.VisitToday = selectAllVisit.Count(v => v.Date.Date == dtNow);
            visit.VisitYesterday = selectAllVisit.Count(v => v.Date.Date == yesterday);
            return await Task.FromResult((IViewComponentResult)View("/Areas/Admin/Views/Home/Components/VisitSite.cshtml", visit));
        }
    }
}
