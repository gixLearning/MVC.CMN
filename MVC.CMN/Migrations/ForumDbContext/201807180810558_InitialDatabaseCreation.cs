namespace MVC.CMN.Migrations.ForumDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDatabaseCreation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Boards",
                c => new
                    {
                        BoardId = c.Int(nullable: false, identity: true),
                        ThreadCount = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 128, unicode: false),
                        Description = c.String(nullable: false, maxLength: 128, unicode: false),
                    })
                .PrimaryKey(t => t.BoardId);
            
            CreateTable(
                "dbo.Threads",
                c => new
                    {
                        ThreadId = c.Int(nullable: false, identity: true),
                        BoardId = c.Int(nullable: false),
                        PostCount = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 128),
                        Subject = c.String(nullable: false, maxLength: 128, unicode: false),
                    })
                .PrimaryKey(t => t.ThreadId)
                .ForeignKey("dbo.UserProfiles", t => t.CreatedBy)
                .ForeignKey("dbo.Boards", t => t.BoardId)
                .Index(t => t.BoardId)
                .Index(t => t.CreatedBy);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        PostId = c.Int(nullable: false, identity: true),
                        ThreadId = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Subject = c.String(nullable: false, maxLength: 128, unicode: false),
                        Content = c.String(nullable: false, maxLength: 1000, unicode: false),
                        CreatedBy = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.PostId)
                .ForeignKey("dbo.UserProfiles", t => t.CreatedBy)
                .ForeignKey("dbo.Threads", t => t.ThreadId)
                .Index(t => t.ThreadId)
                .Index(t => t.CreatedBy);
            
            CreateTable(
                "dbo.UserProfiles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        PostCount = c.Int(nullable: false),
                        Upvotes = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Threads", "BoardId", "dbo.Boards");
            DropForeignKey("dbo.Posts", "ThreadId", "dbo.Threads");
            DropForeignKey("dbo.Threads", "CreatedBy", "dbo.UserProfiles");
            DropForeignKey("dbo.Posts", "CreatedBy", "dbo.UserProfiles");
            DropIndex("dbo.Posts", new[] { "CreatedBy" });
            DropIndex("dbo.Posts", new[] { "ThreadId" });
            DropIndex("dbo.Threads", new[] { "CreatedBy" });
            DropIndex("dbo.Threads", new[] { "BoardId" });
            DropTable("dbo.UserProfiles");
            DropTable("dbo.Posts");
            DropTable("dbo.Threads");
            DropTable("dbo.Boards");
        }
    }
}
