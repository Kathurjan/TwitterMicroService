using System.Text;
using FeedHandlingMicroservice.Models;
using FeedHandlingMicroservice.RabbitMq.Helpers;
using FeedHandlingMicroservice.RabbitMq.Interfaces;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using Newtonsoft.Json;


namespace FeedHandlingMicroservice.RabbitMq.RabbitMqServices;

public class RabbitMqSender : IRabbitMqSender
{
    private readonly string _queueName;

    public RabbitMqSender(IOptions<RabbitMqSettings> rabbitMqSettings)
    {
        _queueName = rabbitMqSettings.Value.QueueName;
    }

    public void SendUserId(NotificationDto notificationDto)
    {
        var factory = new ConnectionFactory { HostName = "localhost" };
        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            try
            {
                if (string.IsNullOrEmpty(_queueName))
                {
                  
                    return;
                }
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

                string messageJson = JsonConvert.SerializeObject(notificationDto);
                byte[] body = Encoding.UTF8.GetBytes(messageJson);

                // Publish message
                channel.BasicPublish(exchange: "", routingKey: _queueName, basicProperties: null, body: body);
              
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");
                throw new Exception("Sender went wrong" + ex.Message);
               
            }
        }
    }
    
}
