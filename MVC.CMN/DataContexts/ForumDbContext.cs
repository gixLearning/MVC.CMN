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

        }
    }
}