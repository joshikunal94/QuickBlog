using hex.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hex.Services
{
    public class PostService
    {
        public static List<Post> Posts()
        {
              using (var db = new Context())
                {
                    var posts = db.Posts.ToList();
                    return posts;
                }
        }

        public static Post GetPost(Guid id)
        {
            using (var db = new Context())
            {
                var posts = db.Posts.Find(id);
                if (posts == null)
                    throw new ArgumentException();
                return posts;
            }
        }

        public static Post AddPost(Post model,Guid PostOwner)
        {
            using (var db = new Context())
            {
                var blog = db.Blogs.Find(PostOwner);
                if (blog == null)
                    throw new ArgumentException();
                var post = db.Posts.Add(new Post()
                {
                    Title = model.Title,
                    Posted = false,
                    Owner = blog,
                    PostedOn = DateTimeOffset.Now,
                });
                db.SaveChanges();
                return post;                
            }
        }


      
    }
}