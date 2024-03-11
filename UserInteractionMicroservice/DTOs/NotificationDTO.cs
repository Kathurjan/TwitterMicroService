namespace DTO;

public class NotificationDto
{
    public required string Message { get; set; }
    public required int UserId { get; set; }
    public required string Type { get; set; }
}