using FastChat.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FastChat.Data
{
    public class DatabaseContext : IdentityDbContext<AppUserEntity, AppRoleEntity, int>
    { 
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ChatsUsersEntity>(b =>
            {
                b.HasKey(e => new { e.ChatId, e.UserId });
                b.HasOne(e => e.User)
                    .WithMany()
                    .HasForeignKey(e => e.UserId);
            });
        }
    }
}
