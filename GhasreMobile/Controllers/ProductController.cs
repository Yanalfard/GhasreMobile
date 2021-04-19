using DataLayer.Models;
using DataLayer.Security;
using DataLayer.Utilities;
using DataLayer.ViewModels;
using GhasreMobile.Utilities;
using GoogleReCaptcha.V3.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Session;

namespace GhasreMobile.Controllers
{
    public class ProductController : Controller
    {
        private Core db = new Core();
        TblClient SelectUser()
        {
            int userId = Convert.ToInt32(User.Claims.First().Value);
            TblClient selectUser = db.Client.GetById(userId);
            return selectUser;
        }
        [Route("Product/{id}/{name}")]
        public async Task<IActionResult> Index(int id, string name)
        {
            try
            {
                ViewBag.IsBookMark = false;
                if (User.Identity.IsAuthenticated)
                {
                    if (db.BookMark.Get().Any(i => i.ProductId == id && i.ClientId == SelectUser().ClientId))
                    {
                        ViewBag.IsBookMark = true;
                    }
                }
                return await Task.FromResult(View(db.Product.GetById(id)));
            }
            catch
            {
                return await Task.FromResult(Redirect("404.html"));
            }
        }

        [HttpPost]
        [PermissionChecker("user,employee,admin")]
        public async Task<IActionResult> SendComment(SendCommentVm comment)
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

                    if (User.Identity.IsAuthenticated)
                    {
                        if (User.Claims.Last().Value != "user")
                        {
                            addComment.IsValid = true;
                        }
                    }
                    db.Comment.Add(addComment);
                    db.Save();
                    TblProductCommentRel addCommentRel = new TblProductCommentRel();
                    addCommentRel.ProductId = comment.ProductId;
                    addCommentRel.CommentId = addComment.CommentId;
                    db.ProductCommentRel.Add(addCommentRel);
                    db.Save();
                    TblRate addRate = new TblRate();
                    addRate.ClientId = SelectUser().ClientId;
                    addRate.ProductId = comment.ProductId;
                    addRate.Ip = ipUser.ToString();
                    addRate.Rate = comment.Rate;
                    db.Rate.Add(addRate);
                    db.Save();
                    return await Task.FromResult(PartialView());
                }
                return await Task.FromResult(PartialView(comment));
            }
            catch
            {
                return await Task.FromResult(Redirect("404.html"));
            }
        }

        [PermissionChecker("user,employee,admin")]
        public async Task<string> BookMark(int id)
        {
            try
            {
                if (db.BookMark.Get().Any(i => i.ProductId == id && i.ClientId == SelectUser().ClientId))
                {
                    TblBookMark deleteBookMark = db.BookMark.Get(i => i.ProductId == id && i.ClientId == SelectUser().ClientId).Single();
                    db.BookMark.Delete(deleteBookMark);
                    db.Save();
                    return await Task.FromResult("false");
                }
                else
                {
                    TblBookMark addBookMark = new TblBookMark();
                    addBookMark.ClientId = SelectUser().ClientId;
                    addBookMark.ProductId = id;
                    db.BookMark.Add(addBookMark);
                    db.Save();
                    return await Task.FromResult("true");
                }
            }
            catch
            {
                return await Task.FromResult("false");
            }
           

        }
        [PermissionChecker("user,employee,admin")]
        public async Task<string> AlertWhenReady(int id)
        {
            try
            {
                if (!db.AlertWhenReady.Get().Any(i => i.ProductId == id && i.ClientId == SelectUser().ClientId))
                {
                    TblAlertWhenReady addAlertWhenReady = new TblAlertWhenReady();
                    addAlertWhenReady.ClientId = SelectUser().ClientId;
                    addAlertWhenReady.ProductId = id;
                    db.AlertWhenReady.Add(addAlertWhenReady);
                    db.Save();
                    return await Task.FromResult("true");
                }
                return await Task.FromResult("false");
            }
            catch
            {
                return await Task.FromResult("false");
            }
        }
    }
}
