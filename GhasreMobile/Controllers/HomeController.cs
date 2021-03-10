using DataLayer.Models;
using DataLayer.ViewModels;
using GhasreMobile.Utilities;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GhasreMobile.Controllers
{
    public class HomeController : Controller
    {
        Core db = new Core();
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
                return await Task.FromResult(View());
            }
            catch
            {
                return await Task.FromResult(Redirect("404.html"));
            }
        }

        [Route("Contact")]
        public async Task<IActionResult> Contact()
        {
            try
            {
                ViewBag.Instagram = db.Config.Get(i => i.Key == "LinkInsta").SingleOrDefault().Value;
                ViewBag.Telegram = db.Config.Get(i => i.Key == "LinkTelegram").SingleOrDefault().Value;
                return await Task.FromResult(View());
            }
            catch
            {
                return await Task.FromResult(Redirect("404.html"));
            }
        }

        [Route("About")]
        public async Task<IActionResult> About()
        {
            try
            {
                ViewBag.Store = db.Store.Get();
                return await Task.FromResult(View(db.Config.Get(i => i.Key == "DarbareyeMa").SingleOrDefault()));
            }
            catch
            {
                return await Task.FromResult(Redirect("404.html"));
            }
        }

        [Route("Policies")]
        public async Task<IActionResult> Policies()
        {
            try
            {
                return await Task.FromResult(View(db.Config.Get(i => i.Key == "Gavanin").SingleOrDefault()));
            }
            catch
            {
                return await Task.FromResult(Redirect("404.html"));
            }
        }
        [Route("Questions")]
        public async Task<IActionResult> Questions()
        {
            try
            {
                return await Task.FromResult(View(db.RegularQuestion.Get()));
            }
            catch
            {
                return await Task.FromResult(Redirect("404.html"));
            }
        }
        public async Task<IActionResult> Error()
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
        [Route("Delivery")]
        public async Task<IActionResult> Delivery(DeliveryVm delivery)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TblDelivery addDelivery = new TblDelivery();
                    addDelivery.Name = delivery.Name;
                    addDelivery.TellNo = delivery.TellNo;
                    addDelivery.Address = delivery.Address;
                    addDelivery.Message = delivery.Message;
                    addDelivery.DateCreated = DateTime.Now;
                    addDelivery.IsAccepted = false;
                    db.Delivery.Add(addDelivery);
                    db.Delivery.Save();
                    return await Task.FromResult(PartialView(delivery));
                }
                return await Task.FromResult(PartialView(delivery));
            }
            catch
            {
                return await Task.FromResult(Redirect("404.html"));
            }
        }

        [HttpPost]
        [PermissionChecker("user,employee,admin")]
        public async Task<IActionResult> CommentReplay(CommentReplay comment)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var ipUser = Request.HttpContext.Connection.RemoteIpAddress;
                    TblComment addComment = new TblComment();
                    addComment.Body = comment.Body;
                    addComment.ClientId = SelectUser().ClientId;
                    addComment.DateCreated = DateTime.Now;
                    addComment.ParentId = comment.ParentId;
                    if (User.Identity.IsAuthenticated)
                    {
                        if (User.Claims.Last().Value != "user")
                        {
                            addComment.IsValid = true;
                        }
                    }
                    db.Comment.Add(addComment);
                    db.Comment.Save();
                    return await Task.FromResult(PartialView());
                }
                return await Task.FromResult(PartialView(comment));
            }
            catch
            {
                return await Task.FromResult(Redirect("404.html"));
            }
        }

    }
}
