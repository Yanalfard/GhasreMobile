using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Models;
using ReflectionIT.Mvc.Paging;
using Services.Services;
using System.IO;
using Microsoft.AspNetCore.Http;
using GhasreMobile.Utilities;

namespace GhasreMobile.Areas.Admin.Controllers
{
    [Area("Admin")]
    [PermissionChecker("admin")]
    public class AlbumController : Controller
    {
        Core _core = new Core();
        public IActionResult Index(int page = 1)
        {
            IEnumerable<TblAlbum> albums = PagingList.Create(_core.Album.Get(a => !a.IsProduct).OrderByDescending(o => o.AlbumId), 30, page);
            return View(albums);
        }
        public IActionResult Show(int id)
        {
            return ViewComponent("AlbumShowAdmin", new { id = id });
        }
        [HttpGet]
        public IActionResult Create()
        {
            return ViewComponent("CreateAlbumAdmin");
        }
        [HttpPost]
        public async Task<IActionResult> Create(string Name, List<IFormFile> GalleryFile)
        {
            TblAlbum album = new TblAlbum();
            album.Name = Name;
            album.IsProduct = false;
            _core.Album.Add(album);
            _core.Save();
            if (GalleryFile != null)
            {
                foreach (var item in GalleryFile)
                {
                    if (item.IsImages() && item.Length < 3000000)
                    {
                        TblImage image = new TblImage();
                        image.AlbumId = album.AlbumId;
                        image.Image = Guid.NewGuid().ToString() + Path.GetExtension(item.FileName);
                        string saveDirectory = Path.Combine(
                                            Directory.GetCurrentDirectory(), "wwwroot/Images/Album");
                        string savePathAlbum = Path.Combine(
                                            Directory.GetCurrentDirectory(), saveDirectory, image.Image);

                        if (!Directory.Exists(saveDirectory))
                        {
                            Directory.CreateDirectory(saveDirectory);
                        }

                        using (var stream = new FileStream(savePathAlbum, FileMode.Create))
                        {
                            await item.CopyToAsync(stream);
                        }
                        /// #region resize Image
                        ImageConvertor imgResizer = new ImageConvertor();
                        string thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Album/thumb", image.Image);
                        imgResizer.Image_resize(savePathAlbum, thumbPath, 300);
                        /// #endregion
                        _core.Image.Add(image);
                    }

                }
                _core.Save();
            }
            return Redirect("/Admin/Album");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            return ViewComponent("EditAlbumAdmin", new { id = id });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, string Name, List<IFormFile> GalleryFile)
        {
            TblAlbum album = _core.Album.GetById(id);
            album.Name = Name;
            if (GalleryFile != null)
            {
                foreach (var item in GalleryFile)
                {
                    if (item.IsImages() && item.Length < 3000000)
                    {
                        TblImage image = new TblImage();
                        image.AlbumId = album.AlbumId;
                        image.Image = Guid.NewGuid().ToString() + Path.GetExtension(item.FileName);
                        string saveDirectory = Path.Combine(
                                            Directory.GetCurrentDirectory(), "wwwroot/Images/Album");
                        string savePathAlbum = Path.Combine(
                                            Directory.GetCurrentDirectory(), saveDirectory, image.Image);

                        if (!Directory.Exists(saveDirectory))
                        {
                            Directory.CreateDirectory(saveDirectory);
                        }

                        using (var stream = new FileStream(savePathAlbum, FileMode.Create))
                        {
                            await item.CopyToAsync(stream);
                        }
                        /// #region resize Image
                        ImageConvertor imgResizer = new ImageConvertor();
                        string thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Album/thumb", image.Image);
                        imgResizer.Image_resize(savePathAlbum, thumbPath, 300);
                        /// #endregion
                        _core.Image.Add(image);
                    }


                }
                _core.Save();
            }
            return Redirect("/Admin/Album");
        }

        [HttpPost]
        public void Delete(int id)
        {
            IEnumerable<TblImage> images = _core.Image.Get(i => i.AlbumId == id);
            if (images.Count() > 0)
            {
                foreach (var item in images)
                {
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Album", item.Image);

                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                    var imagePath2 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Album/thumb", item.Image);

                    if (System.IO.File.Exists(imagePath2))
                    {
                        System.IO.File.Delete(imagePath2);
                    }
                    _core.Image.Delete(item);
                }
                _core.Save();
            }
            _core.Album.DeleteById(id);
            _core.Save();
        }

        [HttpPost]
        public IActionResult DeleteImage(int id)
        {
            TblImage image = _core.Image.GetById(id);

            var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Album", image.Image);

            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
            var imagePath2 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Album/thumb", image.Image);

            if (System.IO.File.Exists(imagePath2))
            {
                System.IO.File.Delete(imagePath2);
            }
            _core.Image.Delete(image);
            _core.Save();
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
