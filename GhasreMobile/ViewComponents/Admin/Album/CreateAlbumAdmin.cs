using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GhasreMobile.ViewComponents.Admin.Album
{
    public class CreateAlbumAdmin : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            return await Task.FromResult((IViewComponentResult)View("/Areas/Admin/Views/Album/Components/Create.cshtml"));
        }
    }
}
