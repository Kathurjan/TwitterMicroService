using Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts;

public class UserInteractionDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public UserInteractionDbContext(DbContextOptions<UserInteractionDbContext> options)
        : base(options)
    {

    }

    public DbSet<Notification> Notifications { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<NotificationUserRelation> NotificationUserRelationts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Notification>()
            .HasOne(n => n.User)
            .WithMany(u => u.Notifications)
            .HasForeignKey(n => n.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<NotificationUserRelation>()
            .HasOne(nur => nur.Notification)
            .WithMany(n => n.NotificationUserRelations)
            .HasForeignKey(nur => nur.NotificationId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<NotificationUserRelation>()
            .HasOne(nur => nur.User)
            .WithMany(u => u.NotificationUserRelations)
            .HasForeignKey(nur => nur.UserId)
            .OnDelete(DeleteBehavior.Cascade);


    }

}