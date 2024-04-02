namespace Entities;

public class User
{
    public string Id { get; set; }

    public ICollection<NotificationUserRelation> NotificationUserRelations { get; set; }

    public ICollection<Notification> Notifications { get; set; }
}