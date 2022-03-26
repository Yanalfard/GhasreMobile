using DataLayer.Models;
using DataLayer.ViewModels;
using GhasreMobile.Utilities;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace GhasreMobile.Controllers
{
    public class HomeController : Controller
    {
        Core db = new Core();
        TblClient SelectUser()
        {
            int userId = Convert.ToInt32(User.Claims.First().Value);
            TblClient selectUser = db.Client.GetById(userId);
            return selectUser;
        }

        public async Task<IActionResult> Index()
        {
            //try
            //{
                //string strHostName = "";
                //strHostName = System.Net.Dns.GetHostName();

                //IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(strHostName);

                //IPAddress[] addr = ipEntry.AddressList;

                //var ipUser = addr[addr.Length - 1].ToString();

                var ipUser = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                string dt = DateTime.Now.ToShortDateString();
                List<TblVisit> visits = db.Visit.Get().ToList();
                if (!visits.Any(i => i.Date.ToShortDateString() == dt))
                {
                    ViewBag.showModelMessage = "true";
                    ViewBag.TextModelMessage = db.Config.Get(i => i.Key == "TextModelMessage").SingleOrDefault().Value;
                    TblVisit addVisit = new TblVisit();
                    addVisit.Ip = ipUser.ToString();
                    addVisit.Date = DateTime.Now;
                    db.Visit.Add(addVisit);
                    db.Save();
                }
                else
                {
                    if (!visits.Any(j => j.Ip == ipUser))
                    {
                        ViewBag.showModelMessage = "true";
                        ViewBag.TextModelMessage = db.Config.Get(i => i.Key == "TextModelMessage").SingleOrDefault().Value;
                        TblVisit addVisit = new TblVisit();
                        addVisit.Ip = ipUser.ToString();
                        addVisit.Date = DateTime.Now;
                        db.Visit.Add(addVisit);
                        db.Save();
                    }
                }

                ViewData["ListIdAd"] = db.Ad.Get().Select(i => i.AdId).ToList();
                ViewData["ListIdSpecial"] = db.SpecialOffer.Get(i => i.ValidTill > DateTime.Now && i.Product.IsDeleted == false && i.Product.TblColor.Sum(i => i.Count) > 0).Select(i => i.SpecialOfferId).ToList();
                return await Task.FromResult(View());
            //}
            //catch
            //{
            //    return await Task.FromResult(Redirect("404.html"));
            //}
        }
        [Route("Contact")]
        public async Task<IActionResult> Contact()
        {
            try
            {
                ViewBag.Instagram = db.Config.Get(i => i.Key == "LinkInsta").SingleOrDefault().Value;
                ViewBag.Telegram = db.Config.Get(i => i.Key == "LinkTelegram").SingleOrDefault().Value;
                ViewBag.Email = db.Config.Get(i => i.Key == "Email").SingleOrDefault().Value;
                ViewBag.Whatsapp = db.Config.Get(i => i.Key == "Whatsapp").SingleOrDefault().Value;
                return await Task.FromResult(View());
            }
            catch
            {
                return await Task.FromResult(Redirect("404.html"));
            }
        }

        [Route("Conditions")]
        public async Task<IActionResult> Conditions()
        {
            try
            {
                return await Task.FromResult(View(db.Config.Get(i => i.Key == "SharayeteAghsati").SingleOrDefault()));
            }
            catch
            {
                return await Task.FromResult(Redirect("404.html"));
            }
        }

        [Route("About")]
        public async Task<IActionResult> About()
        {
            try
            {
                ViewBag.Store = db.Store.Get();
                return await Task.FromResult(View(db.Config.Get(i => i.Key == "DarbareyeMa").SingleOrDefault()));
            }
            catch
            {
                return await Task.FromResult(Redirect("404.html"));
            }
        }

        [Route("Policies")]
        public async Task<IActionResult> Policies()
        {
            try
            {
                return await Task.FromResult(View(db.Config.Get(i => i.Key == "Gavanin").SingleOrDefault()));
            }
            catch
            {
                return await Task.FromResult(Redirect("404.html"));
            }
        }
        [Route("Questions")]
        public async Task<IActionResult> Questions()
        {
            try
            {
                return await Task.FromResult(View(db.RegularQuestion.Get()));
            }
            catch
            {
                return await Task.FromResult(Redirect("404.html"));
            }
        }
        public async Task<IActionResult> Error()
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
        [Route("Delivery")]
        public async Task<IActionResult> Delivery(DeliveryVm delivery)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TblDelivery addDelivery = new TblDelivery();
                    addDelivery.Name = delivery.Name;
                    addDelivery.TellNo = delivery.TellNo;
                    addDelivery.Address = delivery.Address;
                    addDelivery.Message = delivery.Message;
                    addDelivery.DateCreated = DateTime.Now;
                    addDelivery.IsAccepted = false;
                    db.Delivery.Add(addDelivery);
                    db.Save();
                    return await Task.FromResult(PartialView(delivery));
                }
                return await Task.FromResult(PartialView(delivery));
            }
            catch
            {
                return await Task.FromResult(Redirect("404.html"));
            }
        }
        [HttpPost]
        [Route("TamasBaMaForm")]
        public async Task<IActionResult> TamasBaMaForm(ContactUsVm contactUs)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TblContactUs addContactUs = new TblContactUs();
                    addContactUs.Name = contactUs.Name;
                    addContactUs.TellNo = contactUs.TellNo;
                    addContactUs.Message = contactUs.Message;
                    db.ContactUs.Add(addContactUs);
                    db.Save();
                    return await Task.FromResult(PartialView(contactUs));
                }
                return await Task.FromResult(PartialView(contactUs));
            }
            catch
            {
                return await Task.FromResult(Redirect("404.html"));
            }
        }
        [HttpPost]
        [PermissionChecker("user,employee,admin")]
        public async Task<IActionResult> CommentReplay(CommentReplay comment)
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
                    addComment.ParentId = comment.ParentId;
                    if (User.Identity.IsAuthenticated)
                    {
                        if (User.Claims.Last().Value != "user")
                        {
                            addComment.IsValid = true;
                        }
                    }
                    db.Comment.Add(addComment);
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


        public IActionResult IndexNew()
        {
            return View();
        }
        public IActionResult IndexNew2()
        {
            return View();
        }


    }
}
