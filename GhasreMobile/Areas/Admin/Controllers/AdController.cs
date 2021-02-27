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

namespace GhasreMobile.Areas.Admin.Controllers
{
    [Area("Admin")]
    [PermissionChecker("admin")]
    public class AdController : Controller
    {
        Core _core = new Core();
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return ViewComponent("AdCreateAdmin");
        }

        public async Task<IActionResult> CreateAsync(TblAd ad, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    ad.Image = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string savePath = Path.Combine(
                                            Directory.GetCurrentDirectory(), "wwwroot/Images/Ad", ad.Image
                                        );

                    using (var stream = new FileStream(savePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
                ad.PositionId = 0;
                _core.Ad.Add(ad);
                _core.Ad.Save();
                return Redirect("/Admin/Ad");
            }
            return View(ad);
        }

        public IActionResult Edit(int id)
        {
            return ViewComponent("AdEditAdmin", new { id = id });
        }

        public async Task<IActionResult> EditAsync(TblAd ad, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                TblAd EditAd = _core.Ad.GetById(ad.AdId);

                if (file != null)
                {
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Ad", EditAd.Image);

                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
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
                _core.Ad.Save();
                return Redirect("/Admin/Ad");
            }
            return View(ad);
        }
    }
}
