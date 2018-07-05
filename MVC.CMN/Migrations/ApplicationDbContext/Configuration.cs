using Microsoft.AspNet.Identity;
using MVC.CMN.Models;
using System;
using System.Data.Entity.Migrations;

namespace MVC.CMN.Migrations.ApplicationDbContext {

    internal sealed class Configuration : DbMigrationsConfiguration<DataContexts.ApplicationDbContext> {

        public Configuration() {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\ApplicationDbContext";
            SetSqlGenerator("MySql.Data.MySqlLient", new MySql.Data.Entity.MySqlMigrationSqlGenerator());
            CodeGenerator = new MySql.Data.Entity.MySqlMigrationCodeGenerator();
        }

        protected override void Seed(DataContexts.ApplicationDbContext context) {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
            var passwordHasher = new PasswordHasher();
            var admin = new ApplicationUser {
                UserName = "Admin",
                PasswordHash = passwordHasher.HashPassword("Password@123"),
                PhoneNumber = "12345678911",
                Email = "admin@admin.com",
                SecurityStamp = Guid.NewGuid().ToString()
            };
            context.Users.AddOrUpdate(x => x.UserName, admin);
            context.SaveChanges();
        }
    }
}