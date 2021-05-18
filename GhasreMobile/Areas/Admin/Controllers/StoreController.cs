using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GhasreMobile.Utilities;
using DataLayer.Models;
using Services.Services;
using ReflectionIT.Mvc.Paging;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace GhasreMobile.Areas.Admin.Controllers
{
    [Area("Admin")]
    [PermissionChecker("admin")]
    public class StoreController : Controller
    {
        Core _core = new Core();
        public IActionResult Index(int page = 1, string Search = null)
        {
            if (!string.IsNullOrEmpty(Search))
            {
                IEnumerable<TblStore> stores = PagingList.Create(_core.Store.Get(s => s.Name.Contains(Search)), 40, page);
                return View(stores);
            }
            else
            {
                IEnumerable<TblStore> stores = PagingList.Create(_core.Store.Get().OrderByDescending(s => s.StoreId), 40, page);
                return View(stores);
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return ViewComponent("StoreCreateAdmin");
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(TblStore store, List<IFormFile> GalleryFile)
        {
            if (ModelState.IsValid)
            {
                _core.Store.Add(store);
                _core.Save();
                if (GalleryFile != null)
                {
                    foreach (var item in GalleryFile)
                    {
                        TblImage image = new TblImage();
                        image.Image = Guid.NewGuid().ToString() + Path.GetExtension(item.FileName);
                        string saveDirectory = Path.Combine(
                                            Directory.GetCurrentDirectory(), "wwwroot/Images/Store");
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
                        string thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Store/thumb", image.Image);
                        imgResizer.Image_resize(savePathAlbum, thumbPath, 300);
                        /// #endregion
                        _core.Image.Add(image);
                        _core.Save();
                        TblStoreImageRel imageRel = new TblStoreImageRel();
                        imageRel.StoreId = store.StoreId;
                        imageRel.ImageId = image.ImageId;
                        _core.StoreImageRel.Add(imageRel);
                        _core.Save();
                    }
                }
               
                return await Task.FromResult(Redirect("/Admin/Store"));

            }
            return View(store);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            return ViewComponent("StoreEditAdmin", new { id = id });
        }

        [HttpPost]
        public async Task<IActionResult> EditAsync(TblStore store, List<IFormFile> GalleryFile)
        {
            if (ModelState.IsValid)
            {
                if (GalleryFile != null)
                {
                    foreach (var item in GalleryFile)
                    {
                        TblImage image = new TblImage();
                        image.Image = Guid.NewGuid().ToString() + Path.GetExtension(item.FileName);
                        string saveDirectory = Path.Combine(
                                            Directory.GetCurrentDirectory(), "wwwroot/Images/Store");
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
                        string thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Store/thumb", image.Image);
                        imgResizer.Image_resize(savePathAlbum, thumbPath, 300);
                        /// #endregion
                        _core.Image.Add(image);
                        _core.Save();
                        TblStoreImageRel imageRel = new TblStoreImageRel();
                        imageRel.StoreId = store.StoreId;
                        imageRel.ImageId = image.ImageId;
                        _core.StoreImageRel.Add(imageRel);
                        _core.Save();
                    }
                }
                else
                {

                }
                _core.Store.Update(store);
                _core.Save();
                return await Task.FromResult(Redirect("/Admin/Store"));
            }
            return View(store);
        }

        public void Delete(int id)
        {
            TblStore store = _core.Store.GetById(id);
            if (store.TblStoreImageRel.Count() > 0)
            {
                foreach (var item in store.TblStoreImageRel)
                {
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Store", item.Image.Image);

                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                    var imagePath2 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Store/thumb", item.Image.Image);

                    if (System.IO.File.Exists(imagePath2))
                    {
                        System.IO.File.Delete(imagePath2);
                    }
                    _core.StoreImageRel.Delete(item);
                }
                _core.Save();

            }
            _core.Store.Delete(store);
            _core.Save();




        }

        [HttpPost]
        public IActionResult DeleteImage(int id)
        {
            TblStoreImageRel image = _core.StoreImageRel.GetById(id);
            TblImage tblImage = _core.Image.GetById(image.ImageId);
            var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Store", image.Image.Image);

            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
            var imagePath2 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Store/thumb", image.Image.Image);

            if (System.IO.File.Exists(imagePath2))
            {
                System.IO.File.Delete(imagePath2);
            }
            _core.StoreImageRel.Delete(image);
            _core.Save();
            _core.Image.Delete(tblImage);
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
