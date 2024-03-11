using Entities;
using Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;


namespace Infrastructure.Contexts;

public class DbContextManagement : DbContext
{

    public DbContextManagement(DbContextOptions<DbContextManagement> options, IOptions<InfrastructureSettings> infastructureSettings) : base(options)
    {

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