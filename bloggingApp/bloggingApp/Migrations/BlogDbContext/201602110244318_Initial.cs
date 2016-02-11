namespace bloggingApp.Migrations.BlogDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false, maxLength: 20),
                        Checked = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PostCategories",
                c => new
                    {
                        PostId = c.String(nullable: false, maxLength: 128),
                        CategoryId = c.String(nullable: false, maxLength: 128),
                        Checked = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.PostId, t.CategoryId })
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Posts", t => t.PostId, cascadeDelete: true)
                .Index(t => t.PostId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Title = c.String(nullable: false, maxLength: 50),
                        ShortDescription = c.String(nullable: false, maxLength: 250),
                        Body = c.String(nullable: false),
                        Published = c.Boolean(nullable: false),
                        NetLikeCount = c.Int(nullable: false),
                        PostedOn = c.DateTime(nullable: false),
                        Modified = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        PostId = c.String(maxLength: 128),
                        DateTime = c.DateTime(nullable: false),
                        UserName = c.String(),
                        Body = c.String(nullable: false, maxLength: 1000),
                        NetLikeCount = c.Int(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Posts", t => t.PostId)
                .Index(t => t.PostId);
            
            CreateTable(
                "dbo.CommentLikes",
                c => new
                    {
                        CommentId = c.String(nullable: false, maxLength: 128),
                        Username = c.String(),
                        Like = c.Boolean(nullable: false),
                        Dislike = c.Boolean(nullable: false),
                        Comment_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.Comments", t => t.Comment_Id)
                .Index(t => t.Comment_Id);
            
            CreateTable(
                "dbo.Replies",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        PostId = c.String(maxLength: 128),
                        CommentId = c.String(maxLength: 128),
                        ParentReplyId = c.String(),
                        DateTime = c.DateTime(nullable: false),
                        UserName = c.String(),
                        Body = c.String(nullable: false, maxLength: 1000),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Comments", t => t.CommentId)
                .ForeignKey("dbo.Posts", t => t.PostId)
                .Index(t => t.PostId)
                .Index(t => t.CommentId);
            
            CreateTable(
                "dbo.ReplyLikes",
                c => new
                    {
                        ReplyId = c.String(nullable: false, maxLength: 128),
                        Username = c.String(),
                        Like = c.Boolean(nullable: false),
                        Dislike = c.Boolean(nullable: false),
                        Reply_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ReplyId)
                .ForeignKey("dbo.Replies", t => t.Reply_Id)
                .Index(t => t.Reply_Id);
            
            CreateTable(
                "dbo.PostLikes",
                c => new
                    {
                        PostId = c.String(nullable: false, maxLength: 128),
                        Username = c.String(),
                        Like = c.Boolean(nullable: false),
                        Dislike = c.Boolean(nullable: false),
                        Post_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.PostId)
                .ForeignKey("dbo.Posts", t => t.Post_Id)
                .Index(t => t.Post_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PostLikes", "Post_Id", "dbo.Posts");
            DropForeignKey("dbo.PostCategories", "PostId", "dbo.Posts");
            DropForeignKey("dbo.ReplyLikes", "Reply_Id", "dbo.Replies");
            DropForeignKey("dbo.Replies", "PostId", "dbo.Posts");
            DropForeignKey("dbo.Replies", "CommentId", "dbo.Comments");
            DropForeignKey("dbo.Comments", "PostId", "dbo.Posts");
            DropForeignKey("dbo.CommentLikes", "Comment_Id", "dbo.Comments");
            DropForeignKey("dbo.PostCategories", "CategoryId", "dbo.Categories");
            DropIndex("dbo.PostLikes", new[] { "Post_Id" });
            DropIndex("dbo.ReplyLikes", new[] { "Reply_Id" });
            DropIndex("dbo.Replies", new[] { "CommentId" });
            DropIndex("dbo.Replies", new[] { "PostId" });
            DropIndex("dbo.CommentLikes", new[] { "Comment_Id" });
            DropIndex("dbo.Comments", new[] { "PostId" });
            DropIndex("dbo.PostCategories", new[] { "CategoryId" });
            DropIndex("dbo.PostCategories", new[] { "PostId" });
            DropTable("dbo.PostLikes");
            DropTable("dbo.ReplyLikes");
            DropTable("dbo.Replies");
            DropTable("dbo.CommentLikes");
            DropTable("dbo.Comments");
            DropTable("dbo.Posts");
            DropTable("dbo.PostCategories");
            DropTable("dbo.Categories");
        }
    }
}
