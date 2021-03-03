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
    public class CompareController : Controller
    {
        Core db = new Core();
        public ActionResult Index()
        {
            try
            {
                List<CompareItemVm> list = new List<CompareItemVm>();
                var Session = HttpContext.Session.GetComplexData<List<CompareItemVm>>("Compare");
                if (Session != null)
                {
                    list = Session as List<CompareItemVm>;
                }
                List<TblProperty> features = new List<TblProperty>();
                List<TblProductPropertyRel> productFeatures = new List<TblProductPropertyRel>();
                foreach (var item in list)
                {
                    features.AddRange(db.ProductPropertyRel.Get(p => p.ProductId == item.ProductID).Select(f => f.Property).ToList());
                    productFeatures.AddRange(db.ProductPropertyRel.Get(p => p.ProductId == item.ProductID).ToList());
                }
                ViewBag.features = features.Distinct().ToList();
                ViewBag.productFeatures = productFeatures;
                return View(list);
            }
            catch
            {
                return RedirectToAction("/ErrorPage/NotFound");
            }
        }

        //public ActionResult DeleteFromCompare(int id)
        //{
        //    try
        //    {
        //        List<CompareItemVm> list = new List<CompareItemVm>();
        //        if (Session["Compare"] != null)
        //        {
        //            list = Session["Compare"] as List<CompareItemVm>;
        //            int index = list.FindIndex(p => p.ProductID == id);
        //            list.RemoveAt(index);
        //            Session["Compare"] = list;
        //        }
        //        return PartialView("ListCompare", list);
        //    }
        //    catch
        //    {
        //        return RedirectToAction("/ErrorPage/NotFound");
        //    }
        //}


    }
}
