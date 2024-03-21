using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities;
public class NotificationUserRelation
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string UserId  { get; set; }
    public int NotificationId { get; set; }
    public bool HasSeen { get; set; }

    public Notification Notification { get; set; }
    public User User { get; set; }
}