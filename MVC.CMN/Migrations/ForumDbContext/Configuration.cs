using System.Collections.Generic;
using System.Data.Entity.Migrations;
using MVC.CMN.Models.MessageBoard;

namespace MVC.CMN.Migrations.ForumDbContext {
    internal sealed class Configuration : DbMigrationsConfiguration<DataContexts.ForumDbContext> {

        public Configuration() {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\ForumDbContext";
            SetSqlGenerator("MySql.Data.MySqlLient", new MySql.Data.Entity.MySqlMigrationSqlGenerator());
            CodeGenerator = new MySql.Data.Entity.MySqlMigrationCodeGenerator();
            SetHistoryContextFactory("MySql.Data.MySqlClient", (conn, schema) => new MySqlHistoryContext(conn, schema));
        }

        protected override void Seed(DataContexts.ForumDbContext context) {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
            var boards = new List<Board>() {
                new Board() { Name = "Announcements"},
                new Board() { Name = "General Discussion"}
            };
            boards.ForEach(s => context.Boards.AddOrUpdate(s));
            context.SaveChanges();
        }
    }
}