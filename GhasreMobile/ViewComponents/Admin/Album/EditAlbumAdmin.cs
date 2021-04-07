using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Models;
using Services.Services;

namespace GhasreMobile.ViewComponents.Admin.Album
{
    public class EditAlbumAdmin:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            Core core = new Core();
            return await Task.FromResult((IViewComponentResult)View("/Areas/Admin/Views/Album/Components/Edit.cshtml",core.Album.GetById(id)));
        }
    }
}
