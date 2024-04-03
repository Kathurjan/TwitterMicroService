using System.Text;
using FeedHandlingMicroservice.RabbitMq.Helpers;
using FeedHandlingMicroservice.RabbitMq.Interfaces;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;


namespace FeedHandlingMicroservice.RabbitMq.RabbitMqServices;

public class RabbitMqSender : IRabbitMqSender
{
    private readonly string _queueName;

    public RabbitMqSender(IOptions<RabbitMqSettings> rabbitMqSettings)
    {
        _queueName = rabbitMqSettings.Value.QueueName;
    }

    public void SendUserId(int id)
    {
        var factory = new ConnectionFactory { HostName = "localhost" };
        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var json = JsonConvert.SerializeObject(id);
            var body = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish(exchange: "", routingKey: _queueName, basicProperties: null, body: body);
            Console.WriteLine($" [x] Sent {id}");
        }
    }
}
