using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace UserInteractionMicroservice.RabbitMq;

public class Receiver
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly string _queueName;

    public Receiver(string queueName)
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
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine($" [x] Received {message}");
        };
        _channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);
    }
}