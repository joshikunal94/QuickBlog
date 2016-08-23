using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace hex.Entites
{
    public class User
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Name
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTimeOffset  DateOfBirth { get; set; }
        public int Age
        {
            get
            {
                return (DateTimeOffset.Now - DateOfBirth).Days / 365;
            }
        }
        public Gender Gender { get; set; }
        public int VisitCount { get; set; }
        public virtual List<User> Followers { get; set; }
        public virtual List<User> Following { get; set; }
        public virtual List<Blog> SubscribedBlogs { get; set; }
        public virtual List<Blog> OwnedBlogs { get; set; }
        public virtual List<Comment> CommentsMade { get; set; }
        public virtual List<Notification> Notifications { get; set; }
        public virtual File ProfilePicture { get; set; }
        public virtual List<Activity> ActivityAsFollower { get; set; }
        public virtual List<Activity> ActivityAsFollowed { get; set; }
        public virtual List<Activity> ActivityAsSubscriber { get; set; }
        public virtual List<Activity> ActivityAsCommentator { get; set; }
        public virtual List<Activity> ActivityAsBlogCreator { get; set; }
        public virtual List<Activity> ActivityAsPostLiked { get; set; }

        public virtual List<Post> LikedPosts { get; set; }
        
    }
}