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
        private Core _core;
        public ForumController()
        {
            _core = new Core();
        }
        public IActionResult Index()
        {
            List<VmTopic> topics = new List<VmTopic>();
            _core.Topic.Get().ToList().ForEach(i => topics.Add(new VmTopic(i)));
            return View(topics);
        }
        [Route("Forum/ForumView/{id}")]
        public IActionResult ForumView(int id)
        {
            try
            {
                VmTopic topic = new VmTopic(_core.Topic.GetById(id));
                return View(topic);
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
                TblTopic topic = _core.Topic.GetById(id);
                topic.VoteCount++;
                bool res = _core.Topic.Update(topic);
                _core.Topic.Save();
                return Ok(true);
            }

            return Ok(false);
        }
        [HttpPost]

        public IActionResult VoteDown(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                TblTopic topic = _core.Topic.GetById(id);
                topic.VoteCount--;
                _core.Topic.Update(topic);
                _core.Topic.Save();
                return Ok(true);
            }

            return Ok(false);
        }
        [PermissionChecker("user,employee,admin")]
        [HttpPost]
        public IActionResult SubmitComment(string Comment, int TopicId)
        {
            try
            {
                TblClient client = _core.Client.Get(i => i.TellNo == User.Identity.Name).ToList()[0];
                TblComment commentToAdd = new TblComment
                {
                    DateCreated = DateTime.Now,
                    Body = Comment,
                    ClientId = client.ClientId,
                    IsValid = false
                };
                commentToAdd = (TblComment)_core.Comment.Add(commentToAdd).Entity;
                _core.Comment.Save();
                TblTopicCommentRel rel = new TblTopicCommentRel
                {
                    TopicId = TopicId,
                    CommentId = commentToAdd.CommentId
                };
                _core.TopicCommentRel.Add(rel);
                _core.TopicCommentRel.Save();
                return View();
            }
            catch
            {
                return Redirect("/404.html");
            }
        }

        public IActionResult NewForum()
        {
            return View();
        }
    }
}
