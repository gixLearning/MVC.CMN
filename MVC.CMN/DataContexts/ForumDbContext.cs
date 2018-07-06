using MVC.CMN.Models.MessageBoard;
using MySql.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using MVC.CMN.Migrations.ContextInitializers;

namespace MVC.CMN.DataContexts {
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class ForumDbContext : DbContext {

        public ForumDbContext() : base("name=ForumDBConnection") {
            this.Database.Log = (s) => System.Diagnostics.Debug.WriteLine(s);
            Database.SetInitializer(new ForumDbInitializer());
        }

        public virtual DbSet<Board> Boards { get; set; }
        public virtual DbSet<Thread> Threads { get; set; }
        public virtual DbSet<Post> Posts { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Board>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Board>()
                .HasMany(e => e.Threads)
                .WithRequired(e => e.Board)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Thread>()
                .Property(e => e.Subject)
                .IsUnicode(false);

            //
            modelBuilder.Entity<Thread>()
                .Property(e => e.Subject)
                .IsUnicode(false);

            modelBuilder.Entity<Thread>()
                .HasMany(e => e.Posts)
                .WithRequired(e => e.Thread)
                .WillCascadeOnDelete(false);
            //

            modelBuilder.Entity<Post>()
                .Property(e => e.Subject)
                .IsUnicode(false);

            modelBuilder.Entity<Post>()
                .Property(e => e.Content)
                .IsUnicode(false);
        }
    }
}