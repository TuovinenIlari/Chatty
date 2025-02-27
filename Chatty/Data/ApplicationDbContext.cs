using Chatty.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Chatty.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<ChatRoom> ChatRooms { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ChatRoom>()
                .HasMany(e => e.ApplicationUsers)
                .WithMany(e => e.ChatRooms)
                .UsingEntity("UsersToChatRoomsJoinTable");

            builder.Entity<ChatRoom>()
                .HasMany(e => e.Messages)
                .WithOne(e => e.ChatRoom);

            builder.Entity<ApplicationUser>()
                .HasMany(e => e.Messages)
                .WithOne(e => e.User);
                
        }
    }
}
