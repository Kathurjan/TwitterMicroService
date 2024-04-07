namespace DTO;

public class SubscribtionDTO
{
    public string FollowerId { get; set; } 

    public required string CreatorId { get; set; }

    public required string Type { get; set; }
}