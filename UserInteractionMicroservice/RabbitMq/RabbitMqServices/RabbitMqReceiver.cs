using System.Text;
using RabbitMq.RabbitMqIServices;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMq.RabbitMqServices;

public class RabbitMqReceiver : IRabbitMqReceiver
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly string _queueName;

    public RabbitMqReceiver(string queueName)
    {
        _queueName = queueName;

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
            Console.WriteLine($" [x] Received {message}");
        };
        _channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);
    }
}