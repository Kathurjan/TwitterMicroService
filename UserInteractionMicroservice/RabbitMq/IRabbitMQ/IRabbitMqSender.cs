namespace RabbitMq.RabbitMqIServices;
public interface IRabbitMqSender
{
    void Send(string message);
}