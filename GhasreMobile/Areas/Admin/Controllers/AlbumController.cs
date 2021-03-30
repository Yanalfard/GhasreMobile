using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Models;
using ReflectionIT.Mvc.Paging;
using Services.Services;
using System.IO;

namespace GhasreMobile.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AlbumController : Controller
    {
        Core _core = new Core();
        public IActionResult Index(int page = 1)
        {
            IEnumerable<TblAlbum> albums = PagingList.Create(_core.Album.Get().OrderByDescending(o => o.AlbumId), 30, page);
            return View(albums);
        }
        public IActionResult Show(int id)
        {
            return ViewComponent("AlbumShowAdmin", new { id = id });
        }

        [HttpPost]
        public IActionResult DeleteImage(int id)
        {
            TblImage image = _core.Image.GetById(id);

            var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/ProductAlbum", image.Image);

            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }

            TblProductImageRel imageRel = _core.ProductImageRel.Get(ir => ir.ImageId == image.ImageId).SingleOrDefault();
            _core.ProductImageRel.DeleteById(imageRel);
            _core.Image.Delete(image);
            _core.ProductImageRel.Save();
            _core.Image.Save();
            return Redirect("/Admin/Album");
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
