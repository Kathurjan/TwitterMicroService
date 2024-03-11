using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities;
public class NotificationUserRelation
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int UserId  { get; set; }
    public int NotificationId { get; set; }
    public bool HasSeen { get; set; }
}