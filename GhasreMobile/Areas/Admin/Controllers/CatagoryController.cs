using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Models;
using Services.Services;
using ReflectionIT.Mvc.Paging;
using GhasreMobile.Utilities;

namespace GhasreMobile.Areas.Admin.Controllers
{
    [Area("Admin")]
    [PermissionChecker("admin")]
    public class CatagoryController : Controller
    {
        Core _core = new Core();
        public IActionResult Index(int page = 1)
        {
            IEnumerable<TblCatagory> catagories = PagingList.Create(_core.Catagory.Get(c => c.ParentId == null), 10, page);
            return View(catagories);
        }

        [HttpGet]
        public IActionResult Create(int? Id)
        {
            return ViewComponent("CreateCatagoryAdmin", new { Id = Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TblCatagory catagory)
        {
            if (ModelState.IsValid)
            {
                if (catagory.ParentId == null)
                {
                    catagory.IsOnFirstPage = true;
                    _core.Catagory.Add(catagory);
                    _core.Catagory.Save();
                    return Redirect("/Admin/Catagory");
                }
                else
                {

                    TblCatagory ParentCatagory = _core.Catagory.GetById(catagory.ParentId);
                    ParentCatagory.IsOnFirstPage = false;
                    _core.Catagory.Update(ParentCatagory);
                    _core.Catagory.Add(catagory);
                    _core.Catagory.Save();
                    return Redirect("/Admin/Catagory");
                }
            }
            return View(catagory);
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            return ViewComponent("EditCatagoryAdmin", new { Id = Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TblCatagory catagory)
        {
            if (ModelState.IsValid)
            {
                _core.Catagory.Update(catagory);
                _core.Catagory.Save();
                return Redirect("/Admin/Catagory");
            }
            return View(catagory);
        }

        [HttpGet]
        public IActionResult ShowChilds(int Id)
        {
            return ViewComponent("ShowChildsCatagoryAdmin", new { Id = Id });
        }

        public string Delete(int id)
        {
            if (_core.Catagory.GetById(id).InverseParent.Count() > 0 || _core.Catagory.GetById(id).TblProduct.Count() > 0)
            {
                return "false";
            }
            else
            {
                _core.Catagory.DeleteById(id);
                _core.Catagory.Save();
                return "true";
            }
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
