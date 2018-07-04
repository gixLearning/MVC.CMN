using System.Data.Entity.Migrations;

namespace MVC.CMN.Migrations.ForumDbContext {
    internal sealed class Configuration : DbMigrationsConfiguration<DataContexts.ForumDbContext> {

        public Configuration() {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\ForumDbContext";
            SetSqlGenerator("MySql.Data.MySqlLient", new MySql.Data.Entity.MySqlMigrationSqlGenerator());
            CodeGenerator = new MySql.Data.Entity.MySqlMigrationCodeGenerator();
        }

        protected override void Seed(DataContexts.ForumDbContext context) {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}