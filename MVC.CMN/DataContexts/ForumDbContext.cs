using MVC.CMN.Models.MessageBoard;
using MySql.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MVC.CMN.DataContexts {
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class ForumDbContext : DbContext {

        public ForumDbContext() : base("name=ForumDBConnection") {
            this.Database.Log = (s) => System.Diagnostics.Debug.WriteLine(s);
        }

        public virtual DbSet<Board> Boards { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Board>()
                .Property(e => e.Name)
                .IsUnicode(false);
        }
    }
}