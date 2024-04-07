using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfig;

public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{

    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder.Property(n => n.Id).IsRequired();
        builder.Property(n => n.CreatorId).IsRequired();
        builder.Property(n => n.Type).IsRequired();
        builder.Property(n => n.Message).IsRequired();
        builder.Property(n => n.DateOfDelivery).IsRequired();
    }

    
}