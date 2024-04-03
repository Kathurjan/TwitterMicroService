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
            try
            {
                if (string.IsNullOrEmpty(_queueName))
                {
                    Console.WriteLine("Queue name is null or empty.");
                    return;
                }

                Console.WriteLine($"Queue Name: {_queueName}");

                // Ensure proper UTF-8 encoding for queue name
                byte[] queueNameBytes = Encoding.UTF8.GetBytes(_queueName);

                // QueueDeclare parameters
                bool durable = false;
                bool exclusive = false;
                bool autoDelete = false;
                IDictionary<string, object> arguments = null;

                // QueueDeclare method call
                channel.QueueDeclare(queue: _queueName, durable: durable, exclusive: exclusive, autoDelete: autoDelete,
                    arguments: arguments);

                byte[] body = BitConverter.GetBytes(id); // Convert int to byte array

                // Publish message
                channel.BasicPublish(exchange: "", routingKey: _queueName, basicProperties: null, body: body);
                Console.WriteLine($" [x] Sent {id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");
                // Handle exception appropriately, whether by logging, rethrowing, or other means.
            }
        }
    }
}