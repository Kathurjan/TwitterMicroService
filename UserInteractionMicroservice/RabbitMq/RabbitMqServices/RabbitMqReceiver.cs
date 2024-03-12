using System.Text;
using Application.Interfaces;
using DTO;
using RabbitMq.RabbitMqIServices;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Newtonsoft.Json;
using RabbitMq.Helpers;
using Microsoft.Extensions.Options;

namespace RabbitMq.RabbitMqServices;

public class RabbitMqReceiver : IRabbitMqReceiver
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly string _queueName;

    private readonly INotificationService _notificationService;

    public RabbitMqReceiver(INotificationService notificationService,  IOptions<RabbitMqSettings> rabbitMqSettings)
    {
        
        _queueName = rabbitMqSettings.Value.Queuename;
        _notificationService = notificationService;

        var factory = new ConnectionFactory { HostName = "localhost" };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
    }
   

    public void Receive()
    {
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body;
            var message = Encoding.UTF8.GetString(body.ToArray());
            var DeserializedJson = JsonConvert.DeserializeObject<NotificationDto>(message);
            Console.WriteLine(" [x] Received {0}", DeserializedJson);
            Console.WriteLine(DeserializedJson.Message);
            
        };
        _channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);
    }
}