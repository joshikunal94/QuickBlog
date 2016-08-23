using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using hex.Entites;
using System.ComponentModel.DataAnnotations.Schema;

namespace hex
{
    public class Context : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Activity> Activities { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            /* integrity contraints*/
            modelBuilder.Entity<User>().HasKey(t => t.UserId);
            modelBuilder.Entity<User>().Property(t => t.UserId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Blog>().HasKey(t => t.BlogId);
            modelBuilder.Entity<Blog>().Property(t => t.BlogId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Post>().HasKey(t => t.PostId);
            modelBuilder.Entity<Post>().Property(t => t.PostId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<File>().HasKey(t => t.FileId);
            modelBuilder.Entity<File>().Property(t => t.FileId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Comment>().HasKey(t => t.CommentId);
            modelBuilder.Entity<Comment>().Property(t => t.CommentId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Notification>().HasKey(t => t.NotficationId);
            modelBuilder.Entity<Notification>().Property(t => t.NotficationId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Activity>().HasKey(t => t.ActivityId);
            modelBuilder.Entity<Activity>().Property(t => t.ActivityId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity); 

            /*Domain Constraints*/
            modelBuilder.Entity<Comment>().Property(t => t.Text).HasMaxLength(128);
            /* referential constraints*/
            modelBuilder.Entity<User>().HasMany(t => t.OwnedBlogs).WithRequired(t => t.Owner).HasForeignKey(t => t.UserId).WillCascadeOnDelete(false);
            modelBuilder.Entity<User>().HasMany(t=>t.SubscribedBlogs).WithMany(x=>x.Subscribers)
                .Map(m=>m.ToTable("BlogSubscription").MapLeftKey("Blog_BlogId").MapRightKey("Subscriber_UserId"));
            modelBuilder.Entity<User>().HasMany(t=>t.Followers).WithMany(t=>t.Following)
                .Map(m=>m.ToTable("FollowInfo").MapLeftKey("Follower_UserId").MapRightKey("Following_UserId"));
            modelBuilder.Entity<Blog>().HasMany(t=>t.Posts).WithRequired(t=>t.Owner)
                .HasForeignKey(t=>t.BlogId).WillCascadeOnDelete(false);

            modelBuilder.Entity<Post>().HasMany(t => t.Likes).WithMany(t=>t.LikedPosts).Map(m=>m.ToTable("PostLikes"));
          
            modelBuilder.Entity<File>().HasOptional(t => t.UserOwner).WithOptionalDependent(t => t.ProfilePicture)
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<Blog>().HasOptional(t => t.BlogImage)
                .WithOptionalPrincipal(t => t.BlogOwner).WillCascadeOnDelete(false);
            modelBuilder.Entity<Post>().HasOptional(t => t.Html).WithOptionalPrincipal(t => t.PostOwner)
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<User>().HasMany(t => t.CommentsMade).WithRequired(t => t.Commentator)
                .HasForeignKey(t => t.UserId).WillCascadeOnDelete(false);
            modelBuilder.Entity<Post>().HasMany(t => t.Comments).WithRequired(t => t.CommentedPost)
                .HasForeignKey(t => t.PostId).WillCascadeOnDelete(false);
            modelBuilder.Entity<User>().HasMany(t => t.Notifications).WithRequired(t => t.ConcernedUser)
                .HasForeignKey(t => t.UserId).WillCascadeOnDelete(false);

            modelBuilder.Entity<Notification>().HasOptional(t => t.BlogSubscribedBlog).WithOptionalDependent()
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<Notification>().HasOptional(t => t.BlogSubcribedUser).WithOptionalDependent()
                    .WillCascadeOnDelete(false);
            modelBuilder.Entity<Notification>().HasOptional(t => t.PostCommentedPost).WithOptionalDependent()
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<Notification>().HasOptional(t => t.PostCommentedUser).WithOptionalDependent()
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<Notification>().HasOptional(t => t.PostLikedPost).WithOptionalDependent()
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<Notification>().HasOptional(t => t.PostLikedUser).WithOptionalDependent()
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<Notification>().HasOptional(t => t.FollowedByUser).WithOptionalDependent()
                .WillCascadeOnDelete(false);
            
            
            //modelBuilder.Entity<User>().HasMany(t => t.ActivityAsFollower).WithOptional(t => t.Follower)
            //    .HasForeignKey(t => t.FollowerUserId).WillCascadeOnDelete(false);
            //modelBuilder.Entity<User>().HasMany(t => t.ActivityAsFollowed).WithOptional(t => t.Followed)
            //    .HasForeignKey(t => t.FollowedUserId).WillCascadeOnDelete(false);
            //modelBuilder.Entity<User>().HasMany(t => t.ActivityAsSubscriber).WithOptional(t => t.Subscriber)
            //    .HasForeignKey(t => t.UserId).WillCascadeOnDelete(false);
            //modelBuilder.Entity<Blog>().HasMany(t => t.ActivityAsSubscribed).WithOptional(t => t.SubscribedBlog)
            //    .HasForeignKey(t => t.BlogId).WillCascadeOnDelete(false);
            //modelBuilder.Entity<User>().HasMany(t => t.ActivityAsCommentator).WithOptional(t => t.Commentator)
            //    .HasForeignKey(t => t.CommentatorId).WillCascadeOnDelete(false);
            //modelBuilder.Entity<Post>().HasMany(t => t.ActivityAsCommented).WithOptional(t => t.CommentedPost)
            //    .HasForeignKey(t => t.CommentedPostId).WillCascadeOnDelete(false);
            //modelBuilder.Entity<Blog>().HasMany(t => t.ActivityAsPostGenerator).WithOptional(t => t.Blog)
            //    .HasForeignKey(t => t.BlogId).WillCascadeOnDelete(false);
            //modelBuilder.Entity<Post>().HasOptional(t => t.ActivityAsGeneratedPost).WithOptionalPrincipal(t => t.NewPost)
            //    .WillCascadeOnDelete(false);
            //modelBuilder.Entity<User>().HasMany(t => t.ActivityAsBlogCreator).WithOptional(t => t.CreatingUser)
            //    .HasForeignKey(t => t.CreatingUserId).WillCascadeOnDelete(false);
            //modelBuilder.Entity<Blog>().HasOptional(t => t.ActivityAsGeneratedBlog).WithOptionalPrincipal(t => t.CreatedBlog)
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<Activity>().HasOptional(t => t.UserFollowUserFollower).WithMany(t => t.ActivityAsFollower)
                .HasForeignKey(t => t.UserFollowUserFollowerId).WillCascadeOnDelete(false);
            modelBuilder.Entity<Activity>().HasOptional(t => t.UserFollowUserFollowed).WithMany(t => t.ActivityAsFollowed)
                .HasForeignKey(t => t.UserFollowUserFollowedId).WillCascadeOnDelete(false);

            modelBuilder.Entity<Activity>().HasOptional(t => t.UserSubscribedBlogUser).WithMany(t => t.ActivityAsSubscriber)
                .HasForeignKey(t => t.UserSubscribedBlogUserId).WillCascadeOnDelete(false);
            modelBuilder.Entity<Activity>().HasOptional(t => t.UserSubscribedBlogBlog).WithMany(t => t.ActivityAsSubscribed)
                .HasForeignKey(t => t.UserSubscribedBlogBlogId).WillCascadeOnDelete(false);

            modelBuilder.Entity<Activity>().HasOptional(t => t.UserCommentedPostUser).WithMany(t => t.ActivityAsCommentator)
                .HasForeignKey(t => t.UserCommentedPostUserId).WillCascadeOnDelete(false);
            modelBuilder.Entity<Activity>().HasOptional(t => t.UserCommentedPostPost).WithMany(t => t.ActivityAsCommented)
                .HasForeignKey(t => t.UserCommentedPostPostId).WillCascadeOnDelete(false);

            modelBuilder.Entity<Activity>().HasOptional(t => t.BlogGeneratedPostBlog).WithMany(t => t.ActivityAsPostGenerator)
                .HasForeignKey(t => t.BlogGeneratedPostBlogId).WillCascadeOnDelete(false);
            modelBuilder.Entity<Activity>().HasOptional(t => t.BlogGeneratedPostPost).WithOptionalPrincipal(t => t.ActivityAsGeneratedPost)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Activity>().HasOptional(t => t.UserCreateBlogUser).WithMany(t => t.ActivityAsBlogCreator)
                .HasForeignKey(t => t.UserCreateBlogUserId).WillCascadeOnDelete(false);
            modelBuilder.Entity<Activity>().HasOptional(t => t.UserCreateBlogBlog).WithOptionalPrincipal(t => t.ActivityAsGeneratedBlog)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Activity>().HasOptional(t => t.UserLikesPostPost).WithMany(t => t.ActivityAsLikedPost)
                .HasForeignKey(t => t.UserLikesPostPostId).WillCascadeOnDelete(false);
            modelBuilder.Entity<Activity>().HasOptional(t => t.UserLikesPostUser).WithMany(t => t.ActivityAsPostLiked)
                .HasForeignKey(t => t.UserLikesPostUserId).WillCascadeOnDelete(false);





            ///properties
            ///
            modelBuilder.Entity<Blog>().Property(t => t.Name).HasMaxLength(150);
            modelBuilder.Entity<Blog>().Property(t => t.Description).HasMaxLength(500);
        }
    }
}