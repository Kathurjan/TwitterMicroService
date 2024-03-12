namespace RabbitMq.RabbitMqIServices;
public interface IRabbitMqSender
{
    void Send(TestInteraction testInteraction);
}