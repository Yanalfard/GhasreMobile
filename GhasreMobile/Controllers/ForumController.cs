using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Models;
using DataLayer.ViewModels;
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

        public IActionResult SubmitComment(string comment)
        {
            return Redirect("/404.html");
        }
    }
}
