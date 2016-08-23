namespace hex.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Blogs",
                c => new
                    {
                        BlogId = c.Guid(nullable: false, identity: true),
                        Name = c.String(),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
                        LastUpdatedAt = c.DateTimeOffset(nullable: false, precision: 7),
                        Category = c.Int(nullable: false),
                        UserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.BlogId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Guid(nullable: false, identity: true),
                        UserName = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        DateOfBirth = c.DateTimeOffset(nullable: false, precision: 7),
                        Gender = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        PostId = c.Guid(nullable: false, identity: true),
                        Title = c.String(),
                        PostedOn = c.DateTimeOffset(nullable: false, precision: 7),
                        Posted = c.Boolean(nullable: false),
                        BlogId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.PostId)
                .ForeignKey("dbo.Blogs", t => t.BlogId)
                .Index(t => t.BlogId);
            
            CreateTable(
                "dbo.FollowInfo",
                c => new
                    {
                        Follower_UserId = c.Guid(nullable: false),
                        Following_UserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Follower_UserId, t.Following_UserId })
                .ForeignKey("dbo.Users", t => t.Follower_UserId)
                .ForeignKey("dbo.Users", t => t.Following_UserId)
                .Index(t => t.Follower_UserId)
                .Index(t => t.Following_UserId);
            
            CreateTable(
                "dbo.BlogSubscription",
                c => new
                    {
                        Blog_BlogId = c.Guid(nullable: false),
                        Subscriber_UserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Blog_BlogId, t.Subscriber_UserId })
                .ForeignKey("dbo.Users", t => t.Blog_BlogId, cascadeDelete: true)
                .ForeignKey("dbo.Blogs", t => t.Subscriber_UserId, cascadeDelete: true)
                .Index(t => t.Blog_BlogId)
                .Index(t => t.Subscriber_UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "BlogId", "dbo.Blogs");
            DropForeignKey("dbo.BlogSubscription", "Subscriber_UserId", "dbo.Blogs");
            DropForeignKey("dbo.BlogSubscription", "Blog_BlogId", "dbo.Users");
            DropForeignKey("dbo.Blogs", "UserId", "dbo.Users");
            DropForeignKey("dbo.FollowInfo", "Following_UserId", "dbo.Users");
            DropForeignKey("dbo.FollowInfo", "Follower_UserId", "dbo.Users");
            DropIndex("dbo.BlogSubscription", new[] { "Subscriber_UserId" });
            DropIndex("dbo.BlogSubscription", new[] { "Blog_BlogId" });
            DropIndex("dbo.FollowInfo", new[] { "Following_UserId" });
            DropIndex("dbo.FollowInfo", new[] { "Follower_UserId" });
            DropIndex("dbo.Posts", new[] { "BlogId" });
            DropIndex("dbo.Blogs", new[] { "UserId" });
            DropTable("dbo.BlogSubscription");
            DropTable("dbo.FollowInfo");
            DropTable("dbo.Posts");
            DropTable("dbo.Users");
            DropTable("dbo.Blogs");
        }
    }
}
