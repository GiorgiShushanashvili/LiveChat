using System.Reflection;
using ChatWithSignalR.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatWithSignalR.Database;

public class ChatDbContext : DbContext
{
    public ChatDbContext(DbContextOptions<ChatDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
        builder.Entity<UserMessage>()
            .HasKey(m => m.Id);
        builder.Entity<User>()
            .Property(u => u.Id)
            .ValueGeneratedOnAdd();
        builder.Entity<UserProfile>()
            .HasKey(m => m.Id);
    }

    public DbSet<User> User { get; set; }
    public DbSet<UserMessage> Messages { get; set; }
    public DbSet<UserProfile> UserProfile { get; set; }
    public DbSet<Room> Room { get; set; }
}