using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Models;

namespace GhasreMobile.Areas.Admin.Controllers
{
    public class UploadController : Controller
    {
        [HttpPost]
        public string UploadGalleryImage(IFormFile File)
        {
            return "";
            //TblImage NewImage = new TblImage();
            //NewImage. = Guid.NewGuid().ToString() + Path.GetExtension(BannerFile.FileName);
            //string savePath = Path.Combine(
            //    Directory.GetCurrentDirectory(), "wwwroot/Upload/Banner1", ability.BannerImageUrl1
            //);

            //using (var stream = new FileStream(savePath, FileMode.Create))
            //{
            //    await BannerFile.CopyToAsync(stream);
            //}
        }
    }
}
