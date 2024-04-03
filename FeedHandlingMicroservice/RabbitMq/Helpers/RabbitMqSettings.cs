using System.ComponentModel.DataAnnotations;

namespace FeedHandlingMicroservice.RabbitMq.Helpers;

public class RabbitMqSettings
{
    [Required]
    public string QueueName { get; set; }
}