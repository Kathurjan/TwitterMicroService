namespace Entities;

public class User
{
    public int Id { get; set; }

    public List<Notification> Notifications { get; set; }
}