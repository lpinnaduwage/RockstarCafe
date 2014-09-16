using RockstarProj.DBModel;
using RockstarProj.Models;
using RockstarProj.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CaptchaMvc.Attributes;
using CaptchaMvc.HtmlHelpers;
using CaptchaMvc.Interface;
using System.Configuration;

namespace RockstarProj.Controllers
{
    public class HomeController : Controller
    {
        protected string ToAddress = ConfigurationManager.AppSettings["ToAddress"];

        public ActionResult Index()
        {
            ViewBag.Message = "Home";
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "About Us";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        public ActionResult Partners()
        {
            ViewBag.Message = "Partners";
            return View();
        }

        public ActionResult BecomeARockstar()
        {
            ViewBag.Message = "Get Involved";
            return View();
        }

        public ActionResult Sponsors()
        {
            ViewBag.Message = "Sponsors";
            return View();
        }

        /// <summary>
        /// Become a rockstar POST view
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult BecomeARockstar(InvolvedModel model)
        {
            UserService svc = new UserService();
            EmailService eSvc = new EmailService();
            List<HttpPostedFileBase> fileList = new List<HttpPostedFileBase>();
            string toAddress = ToAddress;
            string subject = "A new user has registered!";

            if (this.IsCaptchaValid("Captcha is valid"))
            {
                if (ModelState.IsValid)
                {
                    fileList.Add(model.FirstFile);
                    fileList.Add(model.SecondFile);
                    fileList.Add(model.ThirdFile);
                    try
                    {
                        svc.CreateUser(model);
                        ModelState.Clear();
                        TempData["RegisterSuccess"] = "true";
                        string body = eSvc.BecomeARockstarTextBuilder(model);
                        eSvc.SendMail(toAddress, subject, body, fileList);
                    }
                    catch (Exception)
                    {
                        ModelState.Clear();
                        TempData["RegisterSuccess"] = "false";
                    }
                }
                else
                {
                    TempData["MissingFields"] = "true";
                }
            }
            else
            {
                TempData["InvalidCaptcha"] = "true";
            }

            return View();
        }

        /// <summary>
        /// Blog view
        /// </summary>
        /// <returns></returns>
        public ActionResult Blog()
        {
            ViewBag.Message = "Blog";
            BlogService svc = new BlogService();
            List<BlogFileTbl> fileTbl = svc.ReadAllBlogFiles();
            List<BlogTbl> blogTbl = svc.ReadAllBlogItems();
            List<BlogDataModel> blogList = new List<BlogDataModel>();           

            foreach (BlogTbl blogItem in blogTbl)
            {
                BlogDataModel model = new BlogDataModel();
                foreach (BlogFileTbl fileItem in fileTbl)
                {
                    if (fileItem.BlogId == blogItem.Id)
                    {
                        model.FileData = fileItem.FileData.ToArray();
                    }
                }
                //Assigning values to blog model 
                model.BlogTitle = blogItem.BlogTitle;
                model.BlogContent = blogItem.BlogContent;
                model.DateCreated = (DateTime)blogItem.DateCreated;
                blogList.Add(model);
            }
            return View(blogList);
        }

        /// <summary>
        /// Rockstar View
        /// </summary>
        /// <returns></returns>
        public ActionResult Rockstar()
        {
            ProfileService svc = new ProfileService();
            List<RockstarProfile> model = svc.ReadAllProfiles();
            return View(model);
        }

        /// <summary>
        /// Rockstar POST view
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Rockstar(FormCollection collection)
        {
            string searchText = collection["SearchQuery"].ToString().ToLower();
            ProfileService svc = new ProfileService();
            List<RockstarProfile> model = svc.ProfileSearch(searchText);
            return View(model);
        }


    }
}
