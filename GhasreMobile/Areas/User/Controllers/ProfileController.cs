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
        public async Task<IActionResult> Index()
        {
            try
            {
                List<TblNotification> list = db.Notification.Get(i => i.ClientId == SelectUser().ClientId).ToList();
                ViewBag.Notification = list;
                foreach (var item in list.Where(i => i.IsSeen == false))
                {
                    item.IsSeen = true;
                    db.Notification.Update(item);
                }
                db.Save();
                return await Task.FromResult(View(SelectUser()));
            }
            catch
            {
                return await Task.FromResult(Redirect("404.html"));
            }
        }

        public async Task<IActionResult> ChangePassword()
        {

            try
            {
                return await Task.FromResult(View());
            }
            catch
            {
                return await Task.FromResult(Redirect("404.html"));
            }
        }
        [HttpPost]
        public async Task<IActionResult> ChangePasswordAsync(ResetChangePasswordVm change)
        {
            try
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
                        db.Save();
                        return await Task.FromResult(Redirect("/User/Profile/Index?ResetPass=true"));
                    }
                }
                return await Task.FromResult(PartialView("ChangePassword", change));
            }
            catch
            {
                return await Task.FromResult(Redirect("404.html"));
            }

        }
        public string ShowImage()
        {
            TblClient selectedClient = db.Client.GetById(SelectUser().ClientId);
            string Image = selectedClient.MainImage;
            return selectedClient.MainImage;
        }
        [HttpPost]
        public async Task<IActionResult> EditName(string name)
        {
            try
            {
                TblClient selectedClient = db.Client.GetById(SelectUser().ClientId);
                selectedClient.Name = name;
                db.Client.Update(selectedClient);
                db.Save();
                return await Task.FromResult(RedirectToAction("Index"));
            }
            catch
            {
                return await Task.FromResult(Redirect("404.html"));
            }
        }
        public async Task<string> UploadImage(TblClient client)
        {
            string result = string.Empty;
            try
            {
                var files = Request.Form.Files.First();
                if (files != null && files.IsImages() && files.Length < 3000000)
                {
                    TblClient selectedClient = db.Client.GetById(SelectUser().ClientId);
                    if (selectedClient.MainImage != null)
                    {
                        var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/User/", selectedClient.MainImage);
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                        var imagePath2 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/User/thumb/", selectedClient.MainImage);
                        if (System.IO.File.Exists(imagePath2))
                        {
                            System.IO.File.Delete(imagePath2);
                        }
                        client.MainImage = Guid.NewGuid().ToString() + Path.GetExtension(files.FileName);
                        string savePath = Path.Combine(
                            Directory.GetCurrentDirectory(), "wwwroot/Images/User/", client.MainImage
                        );
                        using (var stream = new FileStream(savePath, FileMode.Create))
                        {
                            await files.CopyToAsync(stream);
                        }
                        /// #region resize Image
                        ImageConvertor imgResizer = new ImageConvertor();
                        string thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/User/thumb", client.MainImage);
                        imgResizer.Image_resize(savePath, thumbPath, 300);
                        /// #endregion
                        selectedClient.MainImage = client.MainImage;
                        db.Client.Update(selectedClient);
                        db.Save();
                        return await Task.FromResult("true");
                    }
                }
            }
            catch
            {
                return await Task.FromResult("false");
            }
            return await Task.FromResult("false");
        }
    }
}
