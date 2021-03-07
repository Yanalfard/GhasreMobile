using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Services;
using DataLayer.Models;
using ReflectionIT.Mvc.Paging;

namespace GhasreMobile.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TopicController : Controller
    {
        Core _core = new Core();

        public IActionResult Index(int page = 1, string Search = null)
        {

            if (string.IsNullOrEmpty(Search))
            {
                IEnumerable<TblTopic> topics = PagingList.Create(_core.Topic.Get(), 40, page);
                return View(topics);
            }
            else
            {
                IEnumerable<TblTopic> topics = PagingList.Create(_core.Topic.Get(t => t.Client.TellNo.Contains(Search)), 40, page);
                return View(topics);
            }

        }
        
        public IActionResult Info(int id)
        {
            return ViewComponent("TopicInfoAdmin", new { id = id });
        }

        public void ChangeStatus(int id)
        {
            TblTopic topic = _core.Topic.GetById(id);
            topic.IsValid = !topic.IsValid;
            _core.Topic.Update(topic);
            _core.Topic.Save();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _core.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
