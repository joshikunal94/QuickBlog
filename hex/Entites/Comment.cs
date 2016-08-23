using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hex.Entites
{
    public class Comment
    {
        public Guid CommentId { get; set; }
        public string Text { get; set; } 
        public Guid PostId { get; set; }
        public virtual Post CommentedPost { get; set; }
        public Guid UserId { get; set; }
        public virtual User Commentator { get; set; }

    }
}