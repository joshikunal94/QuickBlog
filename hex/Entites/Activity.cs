 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hex.Entites
{
    public class Activity
    {
        public Guid ActivityId { get; set; }
        public ActivityType Type { get; set; }
        public DateTimeOffset ActivityTime { get; set; }
    //user follow user activity
        
        public Guid? UserFollowUserFollowerId { get; set; }
        public Guid? UserFollowUserFollowedId { get; set; }
        public virtual User UserFollowUserFollower { get; set; }
        public virtual User UserFollowUserFollowed { get; set; }
   //user subscribed blog
        public Guid? UserSubscribedBlogBlogId { get; set; }
        public Guid? UserSubscribedBlogUserId { get; set; }
        public virtual Blog UserSubscribedBlogBlog { get; set; }
        public virtual User UserSubscribedBlogUser { get; set; }
  
   //user commented post
        public Guid? UserCommentedPostUserId { get; set; }
        public Guid? UserCommentedPostPostId { get; set; }
        public virtual User UserCommentedPostUser { get; set; }
        public virtual Post UserCommentedPostPost { get; set; }

   //blog generated post
        public Guid? BlogGeneratedPostPostId { get; set; }
        public Guid? BlogGeneratedPostBlogId { get; set; }
        public virtual Blog BlogGeneratedPostBlog { get; set; }
        public virtual Post BlogGeneratedPostPost { get; set; }
   //user create blog
        public Guid? UserCreateBlogUserId { get; set; }
        public Guid? UserCreateBlogBlogId { get; set; }
        public virtual User UserCreateBlogUser { get; set; }
        public virtual Blog UserCreateBlogBlog { get; set; }

    //user Likes Post
        public Guid? UserLikesPostUserId { get; set; }
        public Guid? UserLikesPostPostId { get; set; }
        public virtual User UserLikesPostUser { get; set; }
        public virtual Post UserLikesPostPost { get; set; }
    }
}