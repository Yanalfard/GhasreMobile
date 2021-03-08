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

        public IActionResult Index()
        {
            return View(db.Album.Get());
        }
        [Route("AlbumView/{id}/{name}")]
        public IActionResult AlbumView(int id, string name)
        {
            return View(db.Album.GetById(id));
        }

    }
}
