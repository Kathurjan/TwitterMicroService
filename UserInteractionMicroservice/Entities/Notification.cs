using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class Notification
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string CreatorId { get; set; }
        public required string Type { get; set; }
        public required string Message { get; set; }

        public bool HasSeen { get; set; }
        public DateTime DateOfDelivery { get; set; }
        public User User { get; set; }

    }
}
