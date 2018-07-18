using MVC.CMN.Models.MessageBoard;
using System.Data.Entity;

namespace MVC.CMN.DataContexts {

    public class ForumDbContext : DbContext {

        public ForumDbContext() : base("name=ForumDBConnection") {
            Database.Log = (s) => System.Diagnostics.Debug.WriteLine(s);
            Configuration.LazyLoadingEnabled = false;
        }

        public virtual DbSet<Board> Boards { get; set; }
        public virtual DbSet<Thread> Threads { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<UserProfile> UserProfiles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Board>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Board>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Board>()
                .HasMany(e => e.Threads)
                .WithRequired(e => e.Board)
                .WillCascadeOnDelete(false);

            //
            modelBuilder.Entity<Thread>()
                .Property(e => e.Subject)
                .IsUnicode(false);

            modelBuilder.Entity<Thread>()
                .HasMany(e => e.Posts)
                .WithRequired(e => e.Thread)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Thread>()
                .Property(e => e.Created)
                .HasPrecision(6);
            //

            modelBuilder.Entity<Post>()
                .Property(e => e.Subject)
                .IsUnicode(false);

            modelBuilder.Entity<Post>()
                .Property(e => e.Content)
                .IsUnicode(false);

            modelBuilder.Entity<Post>()
                .Property(e => e.ThreadId)
                .IsRequired();

            modelBuilder.Entity<UserProfile>()
                .HasMany(e => e.Posts)
                .WithRequired(e => e.UserProfile)
                .HasForeignKey(e => e.CreatedBy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserProfile>()
                .HasMany(e => e.Threads)
                .WithRequired(e => e.UserProfile)
                .HasForeignKey(e => e.CreatedBy)
                .WillCascadeOnDelete(false);
        }
    }
}