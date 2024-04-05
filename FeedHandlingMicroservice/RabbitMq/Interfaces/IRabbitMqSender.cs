
using FeedHandlingMicroservice.Models;

namespace FeedHandlingMicroservice.RabbitMq.Interfaces;
public interface IRabbitMqSender
{
    public void SendUserId(NotificationDto notificationDto);
}