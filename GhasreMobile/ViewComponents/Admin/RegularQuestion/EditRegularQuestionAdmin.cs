using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GhasreMobile.ViewComponents.Admin.RegularQuestion
{
    public class EditRegularQuestionAdmin : ViewComponent
    {
        Core _core = new Core();
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            return await Task.FromResult((IViewComponentResult)View("/Areas/Admin/Views/RegularQuestion/Components/Edit.cshtml", _core.RegularQuestion.GetById(id)));
        }
    }
}
