using GhasreMobile.Utilities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Services.Services;
using ReflectionIT.Mvc.Paging;

namespace GhasreMobile.Areas.Admin.Controllers
{
    [Area("Admin")]
    [PermissionChecker("admin")]
    public class SliderController : Controller
    {
        Core _core = new Core();
        [HttpGet]
        public IActionResult Index(int page = 1)
        {
            IEnumerable<TblBannerAndSlide> AllSlider = PagingList.Create(_core.BannerAndSlide.Get().OrderByDescending(bas => bas.BannerAndSlideId), 30, page);
            return View(AllSlider);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return ViewComponent("CreateSliderAdmin");
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(TblBannerAndSlide slider, IFormFile ImageUrl, int SliderTime)
        {
            if (ModelState.IsValid)
            {
                if (_core.BannerAndSlide.Get().Count() > 5)
                {

                    TblBannerAndSlide FirstSlider = _core.BannerAndSlide.Get().First();
                    string saveDirectory = Path.Combine(
                                                    Directory.GetCurrentDirectory(), "wwwroot/Images/Slider");
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), saveDirectory, FirstSlider.ImageUrl);

                    if (!Directory.Exists(saveDirectory))
                    {
                        Directory.CreateDirectory(saveDirectory);
                    }

                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                    _core.BannerAndSlide.Delete(FirstSlider);


                    TblBannerAndSlide NewSlider = new TblBannerAndSlide();
                    NewSlider.Name = slider.Name;
                    NewSlider.ActiveTill = DateTime.Now.AddDays(SliderTime);
                    NewSlider.IsActive = true;
                    NewSlider.Link = slider.Link;
                    NewSlider.ImageUrl = Guid.NewGuid().ToString() + Path.GetExtension(ImageUrl.FileName);
                    string savePath = Path.Combine(
                                            Directory.GetCurrentDirectory(), "wwwroot/Images/Slider", NewSlider.ImageUrl
                                        );

                    using (var stream = new FileStream(savePath, FileMode.Create))
                    {
                        await ImageUrl.CopyToAsync(stream);
                    };
                    _core.BannerAndSlide.Add(NewSlider);
                    _core.BannerAndSlide.Save();
                    return Redirect("/Admin/Slider");
                }
                else
                {
                    TblBannerAndSlide NewSlider = new TblBannerAndSlide();
                    NewSlider.Name = slider.Name;
                    NewSlider.ActiveTill = DateTime.Now.AddDays(SliderTime);
                    NewSlider.IsActive = true;
                    NewSlider.Link = slider.Link;
                    NewSlider.ImageUrl = Guid.NewGuid().ToString() + Path.GetExtension(ImageUrl.FileName);
                    string savePath = Path.Combine(
                                            Directory.GetCurrentDirectory(), "wwwroot/Images/Slider", NewSlider.ImageUrl
                                        );

                    using (var stream = new FileStream(savePath, FileMode.Create))
                    {
                        await ImageUrl.CopyToAsync(stream);
                    };
                    _core.BannerAndSlide.Add(NewSlider);
                    _core.BannerAndSlide.Save();
                    return Redirect("/Admin/Slider");
                }
            }
            return View(slider);
        }

        public async Task<string> RemoveSlider(int id)
        {
            TblBannerAndSlide slider = _core.BannerAndSlide.GetById(id);

            var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Slider", slider.ImageUrl);

            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }

            _core.BannerAndSlide.DeleteById(id);
            _core.BannerAndSlide.Save();
            return await Task.FromResult("true");
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
