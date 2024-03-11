namespace UserInteractionMicroservice.Entities
{
    public class Notification
    {
        public int Id { get; set; }
        public required string UserUId { get; set; }
        public required string Type { get; set; }
        public required string Message{ get; set; }
        //0 for no 1 for yes
        public bool HasSeen { get; set; }
        public DateTime DateOfDelivery{ get; set; }
    }
}
