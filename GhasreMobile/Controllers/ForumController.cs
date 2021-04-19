using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Models;
using DataLayer.ViewModels;
using GhasreMobile.Utilities;
using Services.Services;

namespace GhasreMobile.Controllers
{
    public class ForumController : Controller
    {
        private Core db;
        public ForumController()
        {
            db = new Core();
        }
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
                return View(db.Topic.Get(i => i.IsValid).OrderByDescending(i => i.TopicId));
            }
            catch (Exception)
            {
                return await Task.FromResult(Redirect("404.html"));
            }
        }
        [Route("ForumView/{id}/{name}")]
        public async Task<IActionResult> ForumView(int id, string name)
        {
            try
            {
                ViewData["ListIdAd"] = db.Ad.Get().Select(i => i.AdId).ToList();
                return await Task.FromResult(View(db.Topic.GetById(id)));
            }
            catch
            {
                return await Task.FromResult(Redirect("404.html"));
            }
        }

        public async Task<IActionResult> ThreadBlock(VmTopic topic)
        {
            try
            {
                return await Task.FromResult(PartialView("_ThreadBlock", topic));
            }
            catch (Exception)
            {
                return await Task.FromResult(Redirect("404.html"));
            }

        }

        [HttpGet]
        [Route("/Forum/VoteUp/{id}")]
        public async Task<IActionResult> VoteUp(int id)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    TblTopic topic = db.Topic.GetById(id);
                    topic.VoteCount++;
                    bool res = db.Topic.Update(topic);
                    db.Save();
                    return Ok(true);
                }
                return await Task.FromResult(Ok(false));
            }
            catch
            {
                return await Task.FromResult(Redirect("404.html"));
            }

        }

        [HttpGet]
        [Route("/Forum/VoteDown/{id}")]
        public async Task<IActionResult> VoteDown(int id)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    TblTopic topic = db.Topic.GetById(id);
                    topic.VoteCount--;
                    db.Topic.Update(topic);
                    db.Save();
                    return await Task.FromResult(Ok(true));
                }
                return await Task.FromResult(Ok(false));
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
                    TblTopicCommentRel addCommentRel = new TblTopicCommentRel();
                    addCommentRel.TopicId = comment.TopicId;
                    addCommentRel.CommentId = addComment.CommentId;
                    db.TopicCommentRel.Add(addCommentRel);
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

        public async Task<IActionResult> NewForum()
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
        [PermissionChecker("user,employee,admin")]
        public async Task<IActionResult> NewForum(AddTopicVm topic)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TblTopic tblTopic = new TblTopic();
                    tblTopic.Title = topic.Title;
                    tblTopic.Body = topic.Body;
                    tblTopic.DateCreated = DateTime.Now;
                    tblTopic.ClientId = SelectUser().ClientId;
                    tblTopic.VoteCount = 0;
                    if (User.Identity.IsAuthenticated)
                    {
                        if (User.Claims.Last().Value != "user")
                        {
                            tblTopic.IsValid = true;
                        }
                    }
                    db.Topic.Add(tblTopic);
                    db.Save();
                    return await Task.FromResult(Redirect("/Forum?addForum=true"));
                }
                return await Task.FromResult(View(topic));
            }
            catch
            {
                return await Task.FromResult(Redirect("404.html"));
            }

        }
    }
}
