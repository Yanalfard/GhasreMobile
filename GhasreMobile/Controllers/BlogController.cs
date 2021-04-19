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
    public class BlogController : Controller
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
                ViewData["ListIdAd"] = db.Ad.Get().Select(i => i.AdId).ToList();
                return await Task.FromResult(View(db.Blog.Get()));
            }
            catch
            {
                return await Task.FromResult(Redirect("404.html"));
            }
        }
        [Route("ViewBlog/{id}/{title}")]
        public async Task<IActionResult> ViewBlog(int id, string title)
        {
            try
            {
                ViewData["ListIdAd"] = db.Ad.Get().Select(i => i.AdId).ToList();
                ViewData["ListIdBlog"] = db.Blog.Get(i => i.BlogId != id).Select(i => i.BlogId).ToList();
                return await Task.FromResult(View(db.Blog.GetById(id)));
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
                    TblBlogCommentRel addCommentRel = new TblBlogCommentRel();
                    addCommentRel.BlogId = comment.BlogId;
                    addCommentRel.CommentId = addComment.CommentId;
                    db.BlogCommentRel.Add(addCommentRel);
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

    }
}
