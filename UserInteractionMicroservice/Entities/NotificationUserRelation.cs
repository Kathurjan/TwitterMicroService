using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities;
public class Subscriptions
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string FollowerId  { get; set; }
    public string CreatorId { get; set; }
    public string Type { get; set; }
    public User User { get; set; }
}