using RabbitMQ.Client;
using System.Text;
using Newtonsoft.Json;
using RabbitMq.RabbitMqIServices;
using RabbitMq.Helpers;
using Microsoft.Extensions.Options;

namespace RabbitMq.RabbitMqServices;
public class RabbitMqSender : IRabbitMqSender
{
    private readonly string _queueName;

    public RabbitMqSender(IOptions<RabbitMqSettings> rabbitMqSettings)
    {
        _queueName = rabbitMqSettings.Value.Queuename;
    }

    public void Send(TestInteraction testInteraction)
    {
        var factory = new ConnectionFactory { HostName = "localhost" };
        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var json = JsonConvert.SerializeObject(testInteraction);
            var body = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish(exchange: "", routingKey: _queueName, basicProperties: null, body: body);
            Console.WriteLine($" [x] Sent {testInteraction}");
        }
    }
}

