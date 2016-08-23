using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hex.Entites
{
    public class Blog
    {
        public Guid BlogId{ get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
        public  DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset LastUpdatedAt { get; set; }
        public BlogCategories Category { get; set; }
        public Guid UserId{ get; set; }
        public User Owner { get; set; }
        public virtual List<User> Subscribers { get; set; }
        public virtual List<Post> Posts { get; set; }
        public virtual File BlogImage  { get; set; }
        public virtual List<Activity> ActivityAsSubscribed { get; set; }
        public virtual List<Activity> ActivityAsPostGenerator { get; set; }
        public virtual Activity ActivityAsGeneratedBlog { get; set; }
        
    }
}