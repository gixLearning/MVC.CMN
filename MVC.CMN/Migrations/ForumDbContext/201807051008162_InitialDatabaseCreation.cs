namespace MVC.CMN.Migrations.ForumDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDatabaseCreation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Boards",
                c => new
                    {
                        BoardId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 128, unicode: false),
                        ThreadCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BoardId)                ;
            
        }
        
        public override void Down()
        {
            DropTable("Boards");
        }
    }
}
