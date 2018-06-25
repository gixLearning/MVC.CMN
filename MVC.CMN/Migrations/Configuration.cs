using Microsoft.AspNet.Identity;
using MVC.CMN.Models;
using System;
using System.Data.Entity.Migrations;
using System.Linq;

namespace MVC.CMN.Migrations {

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext> {

        public Configuration() {
            AutomaticMigrationsEnabled = true;
            ContextKey = "MVC.CMN.Models.ApplicationDbContext";
        }

        protected override void Seed(ApplicationDbContext context) {
            //var store = new RoleStore<IdentityRole>(context);
            //var roleManager = new RoleManager<IdentityRole>(store);
            //List<IdentityRole> identityRoles = new List<IdentityRole> {
            //    new IdentityRole { Name = RoleTypes.Admin },
            //    new IdentityRole { Name = RoleTypes.SuperUser },
            //    new IdentityRole { Name = RoleTypes.User }
            //};
            //foreach (IdentityRole role in identityRoles) {
            //    roleManager.Create(role);
            //}

            var passwordHasher = new PasswordHasher();

            if (!context.Users.Any(u => u.UserName == "admin@admin.com")) {
                string admUser = "admin@admin.com";
                string hashedPassword = passwordHasher.HashPassword("Password@123");
                var admin = new ApplicationUser {
                    UserName = admUser,
                    PasswordHash = hashedPassword,
                    PhoneNumber = "12345678911",
                    Email = "admin@admin.com",
                    SecurityStamp = Guid.NewGuid().ToString()
                };
                context.Users.AddOrUpdate(x => x.UserName, admin);
                context.SaveChanges();
            }
        }
    }
}