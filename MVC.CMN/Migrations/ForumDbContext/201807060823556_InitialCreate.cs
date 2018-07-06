namespace MVC.CMN.Migrations.ForumDbContext {

    using System.Data.Entity.Migrations;

    public partial class InitialCreate : DbMigration {

        public override void Up() {
            CreateTable(
                "Boards",
                c => new {
                    BoardId = c.Int(nullable: false, identity: true),
                    ThreadCount = c.Int(nullable: false),
                    Name = c.String(nullable: false, maxLength: 128, unicode: false),
                    Description = c.String(nullable: false, maxLength: 128, unicode: false),
                })
                .PrimaryKey(t => t.BoardId);

            CreateTable(
                "Threads",
                c => new {
                    ThreadId = c.Int(nullable: false, identity: true),
                    BoardId = c.Int(nullable: false),
                    PostCount = c.Int(nullable: false),
                    Created = c.DateTime(nullable: false, precision: 0),
                    CreatedBy = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    Subject = c.String(nullable: false, maxLength: 128, unicode: false),
                })
                .PrimaryKey(t => t.ThreadId)
                .ForeignKey("UserProfiles", t => t.CreatedBy)
                .ForeignKey("Boards", t => t.BoardId)
                .Index(t => t.BoardId)
                .Index(t => t.CreatedBy);
            //Sql("CREATE index `IX_BoardId` on `Threads` (`BoardId` DESC)");

            CreateTable(
                "Posts",
                c => new {
                    Postid = c.Int(nullable: false, identity: true),
                    ThreadId = c.Int(nullable: false),
                    Created = c.DateTime(nullable: false, precision: 0),
                    Subject = c.String(nullable: false, maxLength: 128, unicode: false),
                    Content = c.String(nullable: false, maxLength: 1000, unicode: false),
                    CreatedBy = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                })
                .PrimaryKey(t => t.Postid)
                .ForeignKey("UserProfiles", t => t.CreatedBy)
                .ForeignKey("Threads", t => t.ThreadId)
                .Index(t => t.ThreadId)
                .Index(t => t.CreatedBy);

            CreateTable(
                "UserProfiles",
                c => new {
                    UserId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    PostCount = c.Int(nullable: false),
                    Upvotes = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.UserId);
        }

        public override void Down() {
            DropForeignKey("Threads", "BoardId", "Boards");
            DropForeignKey("Posts", "ThreadId", "Threads");
            DropForeignKey("Threads", "CreatedBy", "UserProfiles");
            DropForeignKey("Posts", "CreatedBy", "UserProfiles");
            DropIndex("Posts", new[] { "CreatedBy" });
            DropIndex("Posts", new[] { "ThreadId" });
            DropIndex("Threads", new[] { "CreatedBy" });
            DropIndex("Threads", new[] { "BoardId" });
            DropTable("UserProfiles");
            DropTable("Posts");
            DropTable("Threads");
            DropTable("Boards");
        }
    }
}