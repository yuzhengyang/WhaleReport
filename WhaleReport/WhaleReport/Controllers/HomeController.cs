using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WhaleReport.Models;

namespace WhaleReport.Controllers
{
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            //var currentUserId = User.Identity.GetUserId();
            //var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            //var currentUser = manager.FindById(User.Identity.GetUserId());
            //string appkey = currentUser.AppKey;

            //var rm = new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(new ApplicationDbContext()));
            //rm.Create(new ApplicationRole()
            //{
            //    Name = "普通用户",
            //    Description = "描述"
            //});
            //manager.AddToRoles(User.Identity.GetUserId(), "普通用户");

            //bool rs = manager.IsInRole(User.Identity.GetUserId(), "普通用户");

            return View();
        }

        [AllowAnonymous]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [AllowAnonymous]
        public ActionResult NotAuthorized()
        {
            return View();
        }
    }
}