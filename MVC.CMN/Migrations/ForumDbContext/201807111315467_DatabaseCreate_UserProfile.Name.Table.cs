namespace MVC.CMN.Migrations.ForumDbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DatabaseCreate_UserProfileNameTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("UserProfiles", "UserName", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("UserProfiles", "UserName");
        }
    }
}
