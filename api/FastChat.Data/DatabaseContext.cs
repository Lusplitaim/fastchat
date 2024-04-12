using FastChat.Data.Entities;
using FastChat.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FastChat.Data
{
    public class DatabaseContext : IdentityDbContext<AppUserEntity, AppRoleEntity, int>
    { 
        public DbSet<ChatMessageEntity> ChatMessages { get; set; }
        public DbSet<ChatEntity> Chats { get; set; }
        public DbSet<ChatMemberEntity> ChatMembers { get; set; }
        public DbSet<ChannelEntity> Channels { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUserEntity>(b =>
            {
                b.HasIndex(e => e.UserName).IsUnique();
                b.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
                b.Property(e => e.FirstName).HasMaxLength(200);
                b.Property(e => e.LastName).HasMaxLength(200);
            });

            builder.Entity<ChatMessageEntity>(b =>
            {
                b.HasKey(e => new { e.ChatId, e.Id });
                b.HasOne(e => e.Author).WithMany().HasForeignKey(e => e.AuthorId);
                b.HasOne(e => e.Chat).WithMany().HasForeignKey(e => e.ChatId);
                b.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            });

            builder.Entity<ChatEntity>(b =>
            {
                b.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
                b.Property(e => e.Type).HasDefaultValue(ChatType.Dialog);
                b.HasMany(c => c.Members).WithMany(u => u.Chats)
                    .UsingEntity<ChatMemberEntity>(
                        l => l.HasOne(e => e.User).WithMany().HasForeignKey(e => e.UserId),
                        r => r.HasOne(e => e.Chat).WithMany().HasForeignKey(e => e.ChatId));
            });

            builder.Entity<ChannelEntity>(b =>
            {
                b.Property(e => e.SearchName).HasMaxLength(20);
                b.Property(e => e.Name).HasMaxLength(200);
                b.Property(e => e.IsPublic).HasDefaultValue(false);
                b.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
                b.HasIndex(e => e.SearchName).IsUnique();
                b.HasOne(e => e.Chat).WithOne().HasForeignKey<ChannelEntity>(e => e.ChatId).IsRequired();
                b.HasOne(e => e.Owner).WithMany().HasForeignKey(e => e.OwnerId);
            });
        }
    }
}
