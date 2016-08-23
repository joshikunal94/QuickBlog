using hex.Entites;
using hex.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hex.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [ChildActionOnly]
        public PartialViewResult _LeftPane()
        {
            return PartialView();
        }
        [ChildActionOnly]
        public PartialViewResult _RightPane()
        {
            return PartialView();
        }
        public FileResult  _getProfileImage()
        {
            String path = FileService.GetUserProfilePicture(User.Identity.Name);
            return new FileStreamResult(new System.IO.FileStream(path, System.IO.FileMode.Open), "image/jpeg");
        }
        [ChildActionOnly]
        public String _getAuthName()
        {
            return UserService.GetUser(User.Identity.Name).Name;
        }
        [ChildActionOnly]
        public String _getAuthFollowersCount()
        {
            return UserService.GetFollowersCount(User.Identity.Name).ToString();
        }
        [ChildActionOnly]
        public String _getAuthBlogCount()
        {
            return UserService.GetOwnedBlogCount(User.Identity.Name).ToString();
        }


    }
}