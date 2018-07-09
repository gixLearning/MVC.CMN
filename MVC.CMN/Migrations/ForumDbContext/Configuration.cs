using System.Collections.Generic;
using System.Data.Entity.Migrations;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MVC.CMN.Models;
using MVC.CMN.Models.MessageBoard;
using System.Linq;

namespace MVC.CMN.Migrations.ForumDbContext {
    internal sealed class Configuration : DbMigrationsConfiguration<DataContexts.ForumDbContext> {

        public Configuration() {
            AutomaticMigrationsEnabled = true;
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
                new Board() { BoardId = 1, Name = "Announcements", Description = "Collection of important messages regarding the site" },
                new Board() { BoardId = 2, Name = "Game Discussion", Description = "Discuss games!" },
                new Board() { BoardId = 3, Name = "General Discussion", Description = "All non-gamerelated discussion" }
            };
            boards.ForEach(s => context.Boards.AddOrUpdate(s));
            context.SaveChanges();
            


            var ap_context = new DataContexts.ApplicationDbContext();
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(ap_context));
            //var userId = userManager.FindByEmail("admin@admin.com").Id;
            var userId = userManager.FindByName("kevin").Id;
            var user = context.UserProfiles.Where(s => s.UserId == userId).FirstOrDefault();


            //throw new System.Exception(System.DateTimeOffset.UtcNow.ToString());

            var threads = new List<Thread>() {
                new Thread() {
                    ThreadId = 1,
                    BoardId = 1,
                    CreatedBy = user.UserId,
                    Created = System.DateTime.UtcNow,
                    Subject = "Welcome, new user! Enjoy your stay, and obey the rules!"
                },
                new Thread() {
                    ThreadId = 2,
                    BoardId = 1,
                    CreatedBy = user.UserId,
                    Created = System.DateTime.UtcNow,
                    Subject = "User rules"
                },
                new Thread() {
                    ThreadId = 3,
                    BoardId = 2,
                    CreatedBy = user.UserId,
                    Created = System.DateTime.UtcNow,
                    Subject = "Sonic 2020 Speedrun"
                },
                new Thread() {
                    ThreadId = 4,
                    BoardId = 3,
                    CreatedBy = user.UserId,
                    Created = System.DateTime.UtcNow,
                    Subject = "Introduce yourself"
                }
            };
            threads.ForEach(t => context.Threads.AddOrUpdate(t));
            context.SaveChanges();

            var j_id = userManager.FindByName("Jonatan Streith").Id;
            var k_id = userManager.FindByName("kevin").Id;
            var p_id = userManager.FindByName("Paw McFist").Id;

            var posts = new List<Post>() {
                new Post() {
                    PostId = 1,
                    ThreadId = 1,
                    CreatedBy = j_id,
                    Created = System.DateTime.UtcNow,
                    Content = "Welcome, new user! Enjoy your stay, and obey the rules!",
                    Subject = context.Threads.Where(t => t.ThreadId == 1).FirstOrDefault().Subject
                },
                new Post() {
                    PostId = 2,
                    ThreadId = 1,
                    CreatedBy = k_id,
                    Created = System.DateTime.UtcNow,
                    Content = "This place sucks. Where's the content?",
                    Subject = context.Threads.Where(t => t.ThreadId == 1).FirstOrDefault().Subject
                },
                new Post() {
                    PostId = 3,
                    ThreadId = 1,
                    CreatedBy = j_id,
                    Created = System.DateTime.UtcNow,
                    Content = "New content will be added in time. This is just a placeholder. Also I'm talking to myself?",
                    Subject = context.Threads.Where(t => t.ThreadId == 1).FirstOrDefault().Subject
                },
                new Post() {
                    PostId = 4,
                    ThreadId = 2,
                    CreatedBy = j_id,
                    Created = System.DateTime.UtcNow,
                    Content = "User rules! That means you! You rule!",
                    Subject = context.Threads.Where(t => t.ThreadId == 2).FirstOrDefault().Subject
                },
                new Post() {
                    PostId = 5,
                    ThreadId = 2,
                    CreatedBy = p_id,
                    Created = System.DateTime.UtcNow,
                    Content = "Actually, that's not what 'user rules' is supposed to mean.",
                    Subject = context.Threads.Where(t => t.ThreadId == 2).FirstOrDefault().Subject
                },
                new Post() {
                    PostId = 6,
                    ThreadId = 2,
                    CreatedBy = j_id,
                    Created = System.DateTime.UtcNow,
                    Content = "Terms mean what I say they mean. Don't make me ban you for your offensive username.",
                    Subject = context.Threads.Where(t => t.ThreadId == 2).FirstOrDefault().Subject
                },
                new Post() {
                    PostId = 7,
                    ThreadId = 3,
                    CreatedBy = j_id,
                    Created = System.DateTime.UtcNow,
                    Content = "This week, I'll be attempting another speedrun!",
                    Subject = context.Threads.Where(t => t.ThreadId == 3).FirstOrDefault().Subject
                },
                new Post() {
                    PostId = 8,
                    ThreadId = 3,
                    CreatedBy = p_id,
                    Created = System.DateTime.UtcNow,
                    Content = "That game doesn't even exist.",
                    Subject = context.Threads.Where(t => t.ThreadId == 3).FirstOrDefault().Subject
                },
                new Post() {
                    PostId = 9,
                    ThreadId = 3,
                    CreatedBy = j_id,
                    Created = System.DateTime.UtcNow,
                    Content = "That's how you know it's fast!",
                    Subject = context.Threads.Where(t => t.ThreadId == 3).FirstOrDefault().Subject
                },
                new Post() {
                    PostId = 10,
                    ThreadId = 4,
                    CreatedBy = j_id,
                    Created = System.DateTime.UtcNow,
                    Content = "Tell us who you are!",
                    Subject = context.Threads.Where(t => t.ThreadId == 4).FirstOrDefault().Subject
                }
            };
            posts.ForEach(p => context.Posts.AddOrUpdate(p));
            context.SaveChanges();
        }
    }
}