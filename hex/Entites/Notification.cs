using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hex.Entites
{
    public class Notification
    {
        public Guid NotficationId { get; set; }
        public NotificationType Type { get; set; }
        public Guid UserId { get; set; }
        public User ConcernedUser { get; set; }
        //user followed
        public virtual User FollowedByUser { get; set; }

        // BlogSubscribed
        public virtual Blog BlogSubscribedBlog { get; set; }
        public virtual User BlogSubcribedUser { get; set; }
        //post commented upon
        public virtual Post PostCommentedPost { get; set; }
        public virtual User PostCommentedUser { get; set; }
        //post liked
        public virtual Post PostLikedPost { get; set; }
        public virtual User PostLikedUser { get; set; }
    }

    

}