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
    public DbSet<Subscriptions> Subscriptions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Notification>()
            .HasOne(n => n.User)
            .WithMany(u => u.Notifications)
            .HasForeignKey(n => n.CreatorId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Subscriptions>()
            .HasOne(nur => nur.User)
            .WithMany(u => u.Subscriptions)
            .HasForeignKey(nur => nur.FollowerId)
            .OnDelete(DeleteBehavior.Cascade);
    }

}