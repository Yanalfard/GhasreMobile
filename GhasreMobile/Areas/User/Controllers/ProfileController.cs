using DataLayer.Models;
using DataLayer.Security;
using DataLayer.ViewModels;
using GhasreMobile.Utilities;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GhasreMobile.Areas.User.Controllers
{
    [Area("User")]
    [PermissionChecker("user,employee,admin")]
    public class ProfileController : Controller
    {
        private Core db = new Core();
        TblClient SelectUser()
        {
            int userId = Convert.ToInt32(User.Claims.First().Value);
            TblClient selectUser = db.Client.GetById(userId);
            return selectUser;
        }
        public IActionResult Index()
        {
            List<TblNotification> list = db.Notification.Get(i => i.ClientId == SelectUser().ClientId).ToList();
            ViewBag.Notification = list;
            foreach (var item in list.Where(i => i.IsSeen == false))
            {
                item.IsSeen = true;
                db.Notification.Update(item);
            }
            db.Notification.Save();
            return View(SelectUser());
        }

        public async Task<IActionResult> ChangePassword()
        {
            return await Task.FromResult(View());
        }
        [HttpPost]
        public async Task<IActionResult> ChangePasswordAsync(ResetChangePasswordVm change)
        {
            if (ModelState.IsValid)
            {
                TblClient updateUser = db.Client.GetById(SelectUser().ClientId);
                string pass = PasswordHelper.EncodePasswordMd5(change.OldPassword);
                if (pass != updateUser.Password)
                {
                    ModelState.AddModelError("OldPassword", "رمز قدیمی اشتباه است");
                }
                else
                {
                    updateUser.Password = PasswordHelper.EncodePasswordMd5(change.Password);
                    db.Client.Update(updateUser);
                    db.Client.Save();
                    return await Task.FromResult(Redirect("/User/Profile/Index?ResetPass=true"));
                }
            }
            return await Task.FromResult(PartialView("ChangePassword", change));
        }
        public string ShowImage()
        {
            TblClient selectedClient = db.Client.GetById(SelectUser().ClientId);
            string Image = selectedClient.MainImage;
            return selectedClient.MainImage;
        }
        public async Task<string> UploadImage(TblClient client)
        {
            string result = string.Empty;
            try
            {
                var files = Request.Form.Files.First();
                if (files != null && files.IsImage() && files.Length < 20485760)
                {
                    client.MainImage = Guid.NewGuid().ToString() + Path.GetExtension(files.FileName);
                    string savePath = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot/Images/User/", client.MainImage
                    );
                    using (var stream = new FileStream(savePath, FileMode.Create))
                    {
                        await files.CopyToAsync(stream);
                    }
                    TblClient selectedClient = db.Client.GetById(SelectUser().ClientId);
                    if (selectedClient.MainImage != null)
                    {
                        var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/User/", selectedClient.MainImage);
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }
                    selectedClient.MainImage = client.MainImage;
                    db.Client.Update(selectedClient);
                    db.Client.Save();
                    return "true";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return "false";
        }
    }
}
