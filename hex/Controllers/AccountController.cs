using hex.Entites;
using hex.Models.Account;
using hex.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace hex.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserService.Add(new User()
                {
                    UserName = model.UserName,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Password = model.Password,
                    DateOfBirth = model.DateOfBirth,
                    Gender = model.Gender
                });
                FormsAuthentication.SetAuthCookie(model.UserName, false);
                return RedirectToAction("UploadImage", "Account");
            }
            else
            {
                return View();
            }

        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (UserService.IsUserAuthentic(model.UserName, model.Password))
                {
                    if (model.KeepSignedIn)
                    {
                        FormsAuthentication.SetAuthCookie(model.UserName, true);
                        UserService.IncreaseVisitCount(model.UserName);
                    }
                    else
                    {
                        FormsAuthentication.SetAuthCookie(model.UserName, false);
                        UserService.IncreaseVisitCount(model.UserName);
                    }
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Message = "User Name or password incorrect ";
                    return View();
                }

            }
            else
            {
                ViewBag.Message = "User Name or password Incorrect";
                return View();
            }
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }
        [Authorize]
        [HttpGet]
        public ActionResult UploadImage()
        {
            return View();
        }    
        [Authorize]
        [HttpPost]
        public ActionResult UploadImage(HttpPostedFileBase req)
        {
            if(ModelState.IsValid)
            {
                var file = Request.Files["ImageUpload"];
                if (file != null && file.ContentLength > 0)
                {
                    var UploadDir = "~\\UserProfilePic";
                    var name = Guid.NewGuid().ToString() + User.Identity.Name;
                    var ImagePath = Path.Combine(Server.MapPath(UploadDir),name); 
                    file.SaveAs(ImagePath);
                    FileService.AddProfilePicture(User.Identity.Name, ImagePath);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Message = "Something went wrong";
                    return View();
                }
            }
            else
            {
                return View();
            }
        }
   }
    
}