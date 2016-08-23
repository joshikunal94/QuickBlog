using hex.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hex.Services
{
    public class FileService
    {
        public static void AddProfilePicture(string username, string ImagePath)
        {
            using (var db = new Context())
            {
                var users = db.Users.Where(x => x.UserName.Equals(username)).ToList();
                if(users.Count == 0)
                {
                    throw new ArgumentException();
                }
                
                db.Files.Add(new File{
                    Type = FileType.UserImage,
                    CreatedOn = DateTimeOffset.Now,
                    LastAccesedOn = DateTimeOffset.Now,
                    Path = ImagePath,
                    UserOwner = users.First()
                });
                db.SaveChanges();
            }
        }
        public static String GetUserProfilePicture(string username)
        {
            using(var db = new Context())
            {
                var users = db.Users.Where(x=>x.UserName.Equals(username)).ToList();
                if(users.Count == 0)
                {
                    throw new ArgumentException();
                }
                var user = users[0];
                if(user.ProfilePicture == null)
                {
                    throw new InvalidOperationException();
                }
                else
                {
                    return user.ProfilePicture.Path;
                }

            }
        }
        public static void AddCoverPicture(Guid BlogId, string path)
        {
            using (var db = new Context())
            {
                var blog = db.Blogs.Find(BlogId);
                if (blog == null)
                {
                    throw new ArgumentException();
                }
                db.Files.Add(new File
                {
                    Type = FileType.BlogImage,
                    CreatedOn = DateTimeOffset.Now,
                    LastAccesedOn = DateTimeOffset.Now,
                    Path = path,
                    BlogOwner = blog
                });
                db.SaveChanges();
            }
        }

        public static string GetCoverPicture(Guid BlogId)
        {
            using (var db = new Context())
            {
                var blog = db.Blogs.Find(BlogId);
                if (blog == null)
                    throw new ArgumentException();
                if(blog.BlogImage == null)
                {
                    throw new InvalidOperationException();
                }
                return blog.BlogImage.Path;
            }
        }

        public static void AddHtmlContent(Guid PostId, string path)
        {
            using (var db = new Context())
            {
                var post = db.Posts.Find(PostId);
                if (post == null)
                    throw new ArgumentException();
                var blog = post.Owner;
                var user = blog.Owner;

                var file = db.Files.Add(new File()
                {
                    Type = FileType.HtmlText,
                    Path = path,
                    CreatedOn=DateTimeOffset.Now,
                    LastAccesedOn =DateTimeOffset.Now,
                    PostOwner = post,
                    BlogOwner = blog,
                    UserOwner = user
                    
                });
                db.SaveChanges();   
            }
        }
    }
}