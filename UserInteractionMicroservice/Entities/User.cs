namespace Entities;

public class User
{
    public string Id { get; set; }

    public ICollection<Subscriptions> Subscriptions { get; set; }

    public ICollection<Notification> Notifications { get; set; }
}