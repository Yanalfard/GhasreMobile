using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GhasreMobile.Areas.Admin.Controllers
{
    public class UploadController : Controller
    {
        [HttpPost]
        public IActionResult UploadGalleryImage(IFormFile File)
        {
            return Ok();
        }
    }
}
