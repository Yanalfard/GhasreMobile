using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GhasreMobile.Utilities;
using Services.Services;
using DataLayer.Models;
using ReflectionIT.Mvc.Paging;

namespace GhasreMobile.Areas.Admin.Controllers
{
    [Area("Admin")]
    [PermissionChecker("admin")]
    public class RegularQuestionController : Controller
    {
        Core _core = new Core();

        public IActionResult Index(int page = 1)
        {
            IEnumerable<TblRegularQuestion> regularQuestions = PagingList.Create(_core.RegularQuestion.Get().OrderByDescending(rq=>rq.RegularQuestionId), 40, page);
            return View(regularQuestions);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return ViewComponent("CreateRegularQuestionAdmin");
        }

        [HttpPost]
        public IActionResult Create(TblRegularQuestion regularQuestion)
        {
            if (ModelState.IsValid)
            {
                _core.RegularQuestion.Add(regularQuestion);
                _core.Save();
                return Redirect("/Admin/RegularQuestion");
            }
            return View(regularQuestion);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            return ViewComponent("EditRegularQuestionAdmin", new { id = id });
        }

        [HttpPost]
        public IActionResult Edit(TblRegularQuestion regularQuestion)
        {
            if (ModelState.IsValid)
            {
                _core.RegularQuestion.Update(regularQuestion);
                _core.Save();
                return Redirect("/Admin/RegularQuestion");
            }

            return View(regularQuestion);
        }


        public IActionResult RegularQuestionInfo(int id)
        {
            return ViewComponent("RegularQuestionInfoAdmin", new { id = id });
        }

        [HttpPost]
        public void Delete(int id)
        {
            _core.RegularQuestion.DeleteById(id);
            _core.Save();
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
