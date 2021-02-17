using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace GhasreMobile.ViewComponents.View.DeliveryInContact
{
    public class DeliveryInContactView : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)View("~/Views/Shared/Components/DeliveryInContactView/DeliveryInContactView.cshtml"));
        }
    }
}
