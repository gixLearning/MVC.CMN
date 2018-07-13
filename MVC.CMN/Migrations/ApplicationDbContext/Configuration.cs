using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MVC.CMN.Models;
using MVC.CMN.Models.MessageBoard;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

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

            var store = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(store);
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            List<IdentityRole> identityRoles = new List<IdentityRole> {
                new IdentityRole { Name = RoleTypes.Admin },
                new IdentityRole { Name = RoleTypes.Moderator },
                new IdentityRole { Name = RoleTypes.User }
            };
            foreach (IdentityRole role in identityRoles) {
                roleManager.Create(role);
            }

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

            userManager.AddToRole(admin.Id, RoleTypes.Admin);

            var users = new List<ApplicationUser> {
                new ApplicationUser() {
                    UserName = "Sorban",
                    PasswordHash = passwordHasher.HashPassword("Password@1"),
                    PhoneNumber = "12345678911",
                    Email = "sorban@mail.com",
                    SecurityStamp = Guid.NewGuid().ToString()
                },
                new ApplicationUser() {
                    UserName = "kevin",
                    PasswordHash = passwordHasher.HashPassword("Password@2"),
                    PhoneNumber = "12345678911",
                    Email = "kevin@mail.com",
                    SecurityStamp = Guid.NewGuid().ToString()
                },
                new ApplicationUser() {
                    UserName = "Paw McFist",
                    PasswordHash = passwordHasher.HashPassword("Password@3"),
                    PhoneNumber = "12345678911",
                    Email = "paw@mail.com",
                    SecurityStamp = Guid.NewGuid().ToString()
                },
                new ApplicationUser() {
                    UserName = "Jonatan Streith",
                    PasswordHash = passwordHasher.HashPassword("Password@123"),
                    Email = "jonatan@streith.se",
                    SecurityStamp = Guid.NewGuid().ToString()
                }
            };
            users.ForEach(s => context.Users.AddOrUpdate(x => x.UserName, s));

            using (var forumdb = new DataContexts.ForumDbContext()) {
                if (!forumdb.UserProfiles.Any(u => u.UserId == admin.Id)) {
                    UserProfile profile = new UserProfile {
                        UserId = admin.Id,
                        UserName = "Adminzor"
                    };
                    forumdb.UserProfiles.AddOrUpdate(profile);
                }

                foreach (ApplicationUser user in users) {
                    if (forumdb.UserProfiles.Any(u => u.UserId == user.Id)) {
                        continue;
                    }
                    UserProfile profile = new UserProfile {
                        UserId = user.Id,
                        UserName = user.UserName
                    };
                    forumdb.UserProfiles.AddOrUpdate(profile);
                }

                forumdb.SaveChanges();
            }
        }
    }
}