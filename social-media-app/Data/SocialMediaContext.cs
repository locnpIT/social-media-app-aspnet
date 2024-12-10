using Microsoft.EntityFrameworkCore;
using social_media_app.Models;

namespace social_media_app.Data
{
    public class SocialMediaContext : DbContext
    {
        public SocialMediaContext(DbContextOptions<SocialMediaContext> options) : base(options) { }
        public DbSet<User> users { get; set; }
        public DbSet<Comment> comments { get; set; }
        public DbSet<Chat> chats { get; set; }
        public DbSet<Message> messages { get; set; }
        public DbSet<Post> posts { get; set; }
        public DbSet<Reels> reels { get; set; }
        public DbSet<Story> stories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>()
                .ToTable("users")
                .HasMany(u => u.chats)
                .WithMany(c => c.users)
                .UsingEntity("chats_users");

            modelBuilder.Entity<User>().ToTable("users")
                .HasMany(e => e.messages)
                .WithOne(e => e.user)
                .HasForeignKey("user_id");

            modelBuilder.Entity<Chat>().ToTable("chats")
                .HasMany(e => e.messages)
                .WithOne(e => e.chat)
                .HasForeignKey("chat_id");

            modelBuilder.Entity<User>().ToTable("users")
                .HasMany(e => e.savedPost)
                .WithMany(e => e.saved)
                .UsingEntity("users_saved_post");

            modelBuilder.Entity<User>().ToTable("users")
                .HasMany(e => e.likedPost)
                .WithMany(e => e.liked)
                .UsingEntity("posts_liked");

            modelBuilder.Entity<User>()
                .ToTable("users")
                .HasMany(e => e.likedComment)
                .WithMany(e => e.liked)
                .UsingEntity("comments_liked");

            modelBuilder.Entity<Post>().ToTable("posts")
                .HasMany(e => e.comments)
                .WithMany(e => e.posts)
                .UsingEntity("post_comments");

            modelBuilder.Entity<User>().ToTable("user")
                .HasMany(e => e.stories)
                .WithOne(e => e.user)
                .HasForeignKey("user_id");

            modelBuilder.Entity<User>().ToTable("user")
                .HasMany(e => e.reels)
                .WithOne(e => e.user)
                .HasForeignKey("user_id");

            modelBuilder.Entity<User>().ToTable("user")
                .HasMany(e => e.posts)
                .WithOne(e => e.user)
                .HasForeignKey("user_id");

            modelBuilder.Entity<User>().ToTable("user")
                .HasMany(e => e.comments)
                .WithOne(e => e.user)
                .HasForeignKey("user_id");
        }
    }
}
