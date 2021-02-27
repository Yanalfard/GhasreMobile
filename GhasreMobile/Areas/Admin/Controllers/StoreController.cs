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
                IEnumerable<TblStore> stores = PagingList.Create(_core.Store.Get(s => s.Name.Contains(Search)), 30, page);
                return View(stores);
            }
            else
            {
                IEnumerable<TblStore> stores = PagingList.Create(_core.Store.Get(), 30, page);
                return View(stores);
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return ViewComponent("StoreCreateAdmin");
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(TblStore store, IFormFile MainImage)
        {
            if (ModelState.IsValid)
            {
                if (MainImage != null)
                {
                    store.MainImage = Guid.NewGuid().ToString() + Path.GetExtension(MainImage.FileName);
                    string savePath = Path.Combine(
                                            Directory.GetCurrentDirectory(), "wwwroot/Images/Store", store.MainImage
                                        );

                    using (var stream = new FileStream(savePath, FileMode.Create))
                    {
                        await MainImage.CopyToAsync(stream);
                    };
                }
                _core.Store.Add(store);
                _core.Store.Save();
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
        public async Task<IActionResult> EditAsync(TblStore store, IFormFile MainImage)
        {
            if (ModelState.IsValid)
            {
                if (MainImage != null)
                {
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Store", store.MainImage);

                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }

                    store.MainImage = Guid.NewGuid().ToString() + Path.GetExtension(MainImage.FileName);
                    string savePath = Path.Combine(
                                            Directory.GetCurrentDirectory(), "wwwroot/Images/Store", store.MainImage
                                        );

                    using (var stream = new FileStream(savePath, FileMode.Create))
                    {
                        await MainImage.CopyToAsync(stream);
                    };
                }
                _core.Store.Update(store);
                _core.Store.Save();
                return await Task.FromResult(Redirect("/Admin/Store"));
            }
            return View(store);
        }

        public void Delete(int id)
        {
            TblStore store = _core.Store.GetById(id);
            var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Store", store.MainImage);

            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }

            _core.Store.Delete(store);
            _core.Store.Save();
        }
    }
}
