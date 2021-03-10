using DataLayer.Models;
using DataLayer.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GhasreMobile.Controllers
{
    public class GalleryController : Controller
    {
        Core db = new Core();

        public async Task<IActionResult> Index()
        {
            try
            {
                return await Task.FromResult(View(db.Album.Get()));
            }
            catch
            {
                return await Task.FromResult(Redirect("404.html"));
            }
        }
        [Route("AlbumView/{id}/{name}")]
        public async Task<IActionResult> AlbumView(int id, string name)
        {
            try
            {
                return await Task.FromResult(View(db.Album.GetById(id)));
            }
            catch
            {
                return await Task.FromResult(Redirect("404.html"));
            }
        }

    }
}
