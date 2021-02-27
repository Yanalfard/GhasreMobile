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
                return await Task.FromResult(View(db.Product.GetById(id)));
            }
            catch
            {
                return await Task.FromResult(Redirect("ErrorPage"));
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
                    db.Comment.Save();
                    TblProductCommentRel addCommentRel = new TblProductCommentRel();
                    addCommentRel.ProductId = comment.ProductId;
                    addCommentRel.CommentId = addComment.CommentId;
                    db.ProductCommentRel.Add(addCommentRel);
                    db.ProductCommentRel.Save();
                    TblRate addRate = new TblRate();
                    addRate.ClientId = SelectUser().ClientId;
                    addRate.ProductId = comment.ProductId;
                    addRate.Ip = ipUser.ToString();
                    addRate.Rate = comment.Rate;
                    db.Rate.Add(addRate);
                    db.Rate.Save();
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
