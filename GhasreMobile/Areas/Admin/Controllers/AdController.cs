using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GhasreMobile.Utilities;
using DataLayer.Models;
using Services.Services;
using Microsoft.AspNetCore.Http;
using System.IO;
using ReflectionIT.Mvc.Paging;

namespace GhasreMobile.Areas.Admin.Controllers
{
    [Area("Admin")]
    [PermissionChecker("admin")]
    public class AdController : Controller
    {
        Core _core = new Core();
        public IActionResult Index(int page = 1)
        {
            IEnumerable<TblAd> ads = PagingList.Create(_core.Ad.Get(a => a.AdId != 18).OrderByDescending(o => o.AdId), 30, page);
            return View(ads);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return ViewComponent("AdCreateAdmin");
        }

        public async Task<IActionResult> CreateAsync(TblAd ad, IFormFile Image)
        {
            if (ModelState.IsValid)
            {
                if (Image != null && Image.IsImages() && Image.Length < 3000000)
                {
                    string saveDirectory = Path.Combine(
                                                    Directory.GetCurrentDirectory(), "wwwroot/Images/Ad");
                    ad.Image = Guid.NewGuid().ToString() + Path.GetExtension(Image.FileName);
                    string savePath = Path.Combine(
                                            Directory.GetCurrentDirectory(), saveDirectory, ad.Image
                                        );
                    if (!Directory.Exists(saveDirectory))
                    {
                        Directory.CreateDirectory(saveDirectory);
                    }

                    using (var stream = new FileStream(savePath, FileMode.Create))
                    {
                        await Image.CopyToAsync(stream);
                    }
                }
                ad.PositionId = 0;
                _core.Ad.Add(ad);
                _core.Save();
                return Redirect("/Admin/Ad");
            }
            return View(ad);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            return ViewComponent("AdEditAdmin", new { id = id });
        }

        [HttpPost]
        public async Task<IActionResult> EditAsync(TblAd ad, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                TblAd EditAd = _core.Ad.GetById(ad.AdId);

                if (file != null && file.IsImages() && file.Length < 3000000)
                {
                    try
                    {
                        var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Ad", EditAd.Image);
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }
                    catch
                    {

                    }

                    EditAd.Image = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string savePath = Path.Combine(
                                            Directory.GetCurrentDirectory(), "wwwroot/Images/Ad", EditAd.Image
                                        );

                    using (var stream = new FileStream(savePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
                EditAd.Label = ad.Label;
                EditAd.Link = ad.Link;
                EditAd.PositionId = 0;
                _core.Ad.Update(EditAd);
                _core.Save();
                return Redirect("/Admin/Ad");
            }
            return View(ad);
        }

        [HttpPost]
        public void Delete(int id)
        {
            var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Ad", _core.Ad.GetById(id).Image);

            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
            _core.Ad.DeleteById(id);
            _core.Save();
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
