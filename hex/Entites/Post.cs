using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hex.Entites
{
    public class Post
    {
        public Guid PostId { get; set; }
        public string Title { get; set; }
        public DateTimeOffset PostedOn { get; set; }
        public bool Posted { get; set; }
        public Guid BlogId { get; set; }
        public Blog Owner { get; set; }
        public virtual File Html { get; set; }
        public virtual List<User> Likes { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public virtual List<Activity> ActivityAsCommented { get; set; }
        public virtual Activity ActivityAsGeneratedPost { get; set; }
        public virtual List<Activity> ActivityAsLikedPost { get; set; }


    }
}