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
        public IActionResult Index()
        {
            // TblClient user = new TblClient();
            // user.Name = "mr 11111111";
            // user.TellNo = "111111111";
            // TblClient user2 = new TblClient();
            // user.Name = "mr 222222222";
            // user.TellNo = "2222222";

            // List<TblClient> list = new List<TblClient>();
            // list.Add(user2);
            // list.Add(user);
            // var objComplex = new TblClient();
            ////HttpContext.Session.SetObject("ComplexObject", list);
            // HttpContext.Session.SetComplexData("ShopCart", list);
            return View();
        }

        [Route("Contact")]
        public IActionResult Contact()
        {
            // var objComplex = HttpContext.Session.GetObject<List<TblClient>>("ComplexObject");
            //var objComplex = HttpContext.Session.GetComplexData<List<TblClient>>("ShopCart");
            //List<TblClient> list = objComplex;
            //if (objComplex != null)
            //{
            //    //list = sessions;
            //}
            return View();
        }
        [Route("About")]
        public IActionResult About()
        {
            return View(db.Config.Get(i => i.Key == "DarbareyeMa").Single());
        }
        [Route("ErrorPage")]
        public async Task<IActionResult> ErrorPage()
        {
            return await Task.FromResult(View());
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
                return await Task.FromResult(Redirect("ErrorPage"));
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
                return await Task.FromResult(Redirect("ErrorPage"));
            }
        }

    }
}
