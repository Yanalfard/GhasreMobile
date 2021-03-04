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
        TblClient SelectUser()
        {
            int userId = Convert.ToInt32(User.Claims.First().Value);
            TblClient selectUser = db.Client.GetById(userId);
            return selectUser;
        }
        public ForumController()
        {
            db = new Core();
        }
        public IActionResult Index()
        {
            return View(db.Topic.Get(i => i.IsValid));
        }
        [Route("ForumView/{id}/{name}")]
        public IActionResult ForumView(int id, string name)
        {
            try
            {
                return View(db.Topic.GetById(id));
            }
            catch
            {
                return Redirect("ErrorPage");
            }
        }

        public IActionResult ThreadBlock(VmTopic topic)
        {
            return PartialView("_ThreadBlock", topic);
        }

        [HttpPost]
        public IActionResult VoteUp(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                TblTopic topic = db.Topic.GetById(id);
                topic.VoteCount++;
                bool res = db.Topic.Update(topic);
                db.Topic.Save();
                return Ok(true);
            }

            return Ok(false);
        }
        [HttpPost]

        public IActionResult VoteDown(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                TblTopic topic = db.Topic.GetById(id);
                topic.VoteCount--;
                db.Topic.Update(topic);
                db.Topic.Save();
                return Ok(true);
            }

            return Ok(false);
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
                    TblTopicCommentRel addCommentRel = new TblTopicCommentRel();
                    addCommentRel.TopicId = comment.TopicId;
                    addCommentRel.CommentId = addComment.CommentId;
                    db.TopicCommentRel.Add(addCommentRel);
                    db.TopicCommentRel.Save();
                    return await Task.FromResult(PartialView());
                }
                return await Task.FromResult(PartialView(comment));
            }
            catch
            {
                return await Task.FromResult(Redirect("ErrorPage"));
            }
        }

        public IActionResult NewForum()
        {
            return View();
        }
        [HttpPost]
        [PermissionChecker("user,employee,admin")]
        public async Task<IActionResult> NewForum(AddTopicVm topic)
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
                db.Topic.Save();
                return await Task.FromResult(Redirect("/Forum?addForum=true"));
            }
            return View(topic);
        }
    }
}
