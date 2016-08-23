using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using  hex.Entites;

namespace hex.Services
{
    public class BlogService
    {
        public static Blog Add(Blog b,string owneruser)
        {
            if (b == null)
            {
                throw new ArgumentException();
            }

            using (var db = new Context())
            {
                var users = db.Users.Where(x => x.UserName.Equals(owneruser)).ToList();
                if (users.Count == 0)
                    throw new ArgumentException();
                var user = users[0];

                var blog = db.Blogs.Add(new Blog()
                {
                    Name = b.Name,
                    Category = b.Category,
                    Description = b.Description,
                    CreatedAt = DateTimeOffset.Now,
                    LastUpdatedAt = DateTimeOffset.Now,
                    Owner = user
                });
                db.Activities.Add(new Activity()
                {
                    ActivityTime = DateTimeOffset.Now,
                    UserCreateBlogBlog = blog,
                    UserCreateBlogUser = user ,  
                    Type = ActivityType.UserCreateBlog
                });
                db.SaveChanges();
                return blog;
            }
        }
        public static Blog GetBlog(Guid id)
        {
            using(var db = new Context())
            {
                return db.Blogs.Find(id);
            }
        }
        public static int GetPostCount(Guid Id)
        {
            using (var db = new Context())
            {
                var blog = db.Blogs.Find(Id);
                return blog.Posts.Count;
            }
        }
        public static int GetSubscriberCount(Guid Id)
        {
            using (var db = new Context())
            {
                var blog = db.Blogs.Find(Id);
                return blog.Subscribers.Count;
            }
        }
    }
}