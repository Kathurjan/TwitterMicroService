using Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts;

public class DbContextManagement : Microsoft.EntityFrameworkCore.DbContext
{
    public DbContextManagement(DbContextOptions<DbContextManagement> options)
        : base(options) {
        
    }

    public DbSet<Notification> Notifications { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<NotificationUserRelation> NotificationUserRelationts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Notification>()
            .HasOne(n => n.User)
            .WithMany(u => u.Notifications)  // Assuming there's a navigation property in the User class representing notifications
            .HasForeignKey(n => n.UserId);

        modelBuilder.Entity<NotificationUserRelation>()
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(n => n.UserId);

        modelBuilder.Entity<NotificationUserRelation>()
            .HasOne<Notification>()
            .WithMany()
            .HasForeignKey(n => n.NotificationId);

    }

}