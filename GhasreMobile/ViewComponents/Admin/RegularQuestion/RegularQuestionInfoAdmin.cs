using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Services;

namespace GhasreMobile.ViewComponents.Admin.RegularQuestion
{
    public class RegularQuestionInfoAdmin : ViewComponent
    {
        Core _core = new Core();
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            return await Task.FromResult((IViewComponentResult)View("/Areas/Admin/Views/RegularQuestion/Components/RegularQuestionInfo.cshtml", _core.RegularQuestion.GetById(id)));
        }
    }
}
