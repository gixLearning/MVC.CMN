namespace MVC.CMN.Migrations.ForumDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Boards",
                c => new
                    {
                        BoardId = c.Int(nullable: false, identity: true),
                        ThreadCount = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 128, unicode: false),
                    })
                .PrimaryKey(t => t.BoardId)                ;
            
            CreateTable(
                "Threads",
                c => new
                    {
                        ThreadId = c.Int(nullable: false, identity: true),
                        BoardId = c.Int(nullable: false),
                        PostCount = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false, precision: 0),
                        CreatedBy = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Subject = c.String(nullable: false, maxLength: 128, unicode: false),
                    })
                .PrimaryKey(t => t.ThreadId)                
                .ForeignKey("Boards", t => t.BoardId)
                .Index(t => t.BoardId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("Threads", "BoardId", "Boards");
            DropIndex("Threads", new[] { "BoardId" });
            DropTable("Threads");
            DropTable("Boards");
        }
    }
}
