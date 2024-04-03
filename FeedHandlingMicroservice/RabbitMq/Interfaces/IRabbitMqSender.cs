
namespace FeedHandlingMicroservice.RabbitMq.Interfaces;
public interface IRabbitMqSender
{
    public void SendUserId(int id);
}