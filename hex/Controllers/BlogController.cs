using hex.Models.Blog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using hex.Services;
using hex.Entites;
using System.IO;
using hex.Models.Shared;

namespace hex.Controllers
{
    [Authorize]
    public class BlogController : Controller
    {
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BlogCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var file = Request.Files["CoverUpload"];
                var blog = BlogService.Add(new hex.Entites.Blog()
                {
                    Name = model.Title,
                    Description = model.Description,
                    Category = model.Category
                }, User.Identity.Name);
                if (file != null && file.ContentLength > 0)
                {
                    var UploadDir = "~\\BlogCoverPic";
                    var name = Guid.NewGuid().ToString() + User.Identity.Name;
                    var ImagePath = Path.Combine(Server.MapPath(UploadDir), name);
                    file.SaveAs(ImagePath);
                    FileService.AddCoverPicture(blog.BlogId, ImagePath);
                }
                return RedirectToAction("Home", "Blog", new { id = blog.BlogId });

            }
            else
            {
                ViewBag.Message = "Something Went Wrong";
                return View();
            }
        }

        public PartialViewResult _getAuthOwnedBlogList(int? page)
        {
            int pagesize = 2;
            int pg  = 1;
            if (page != null)
            {
                pg = (int)page;
                List<Blog> blogs = UserService.GetOwnedBlogs(User.Identity.Name)
                                         .Skip((pg - 1) * pagesize)
                                         .Take(pagesize).ToList();
                return PartialView("_blogList", blogs);
            }
            else
            {
                List<Blog> blogs = UserService.GetOwnedBlogs(User.Identity.Name)
                                             .Take(pagesize).ToList();
                return PartialView(blogs);
            }
        }
        public PartialViewResult _getBlogSummary(Guid BlogId)
        {
            
            return PartialView(BlogService.GetBlog(BlogId));
        }
        [ChildActionOnly]
        public int _getPostCount(Guid id)
        {
            return BlogService.GetPostCount(id);
        }
        [ChildActionOnly]
        public int _getSubscribersCount(Guid id)
        {
            return @BlogService.GetSubscriberCount(id);
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Home(Guid? id)
        {
            return View(id);
        }
        public FileResult _getBlogCover(Guid? id )
        {
            string path = FileService.GetCoverPicture((Guid)id);
            return new FileStreamResult(new System.IO.FileStream(path , System.IO.FileMode.Open), "image/jpeg");
        }
        public PartialViewResult _leftPane(Guid? id) 
        {
            return PartialView(); 
        }

        public ActionResult CreatePost(Guid? id) 
        {
            return View();
        }

    }
}