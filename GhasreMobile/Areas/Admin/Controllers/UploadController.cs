using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Models;
using System.IO;

namespace GhasreMobile.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UploadController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> UploadCkEditorAsync(IFormFile upload, string CKEditorFuncNum, string CKEditor, string langCode)
        {
            try
            {
                string vImagePath = String.Empty;
                string vMessage = String.Empty;
                string vFilePath = String.Empty;
                string vOutput = String.Empty;
                try
                {
                    if (upload != null && upload.Length > 0)
                    {
                        string vFileName = DateTime.Now.ToString("yyyyMMdd-HHMMssff") +
                                        Path.GetExtension(upload.FileName).ToLower();
                        string savePath = Path.Combine(
                            Directory.GetCurrentDirectory(), "wwwroot/Images/ckeditorimage", vFileName
                        );

                        using (var stream = new FileStream(savePath, FileMode.Create))
                        {
                            await upload.CopyToAsync(stream);
                        }

                        vImagePath = Url.Content("~/Images/ckeditorimage/" + vFileName);
                        vMessage = "عکس مورد نظر آپلود شد";
                    }
                }
                catch (Exception ex)
                {
                    var exe = ex;
                    vMessage = "There was an issue uploading";
                }
                vOutput = @"<html><body><script>window.parent.CKEDITOR.tools.callFunction(" + CKEditorFuncNum + ", \"" + vImagePath + "\", \"" + vMessage + "\");</script></body></html>";
                return Content(vOutput);
            }
            catch
            {
                return RedirectToAction("/ErrorPage/NotFound");
            }
        }
    }
}
