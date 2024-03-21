using Entities;

namespace RabbitMq.RabbitMqIServices;
public interface IRabbitMqSender
{
    public void SendNotification(Notification notification);
}