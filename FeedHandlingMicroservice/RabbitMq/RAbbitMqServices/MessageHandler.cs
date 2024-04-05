using EasyNetQ;
using NetQ;

namespace FeedHandlingMicroservice.RabbitMq.RabbitMqServices;

public class MessageHandler : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Console.WriteLine("MessageHandler is running.");

        var messageClient = new MessageClient(RabbitHutch.CreateBus("host=localhost"));

        while (!stoppingToken.IsCancellationRequested)
        {
            Console.WriteLine("MessageHandler is listening.");
            await Task.Delay(1000, stoppingToken);
        }

        Console.WriteLine("MessageHandler is stopping.");
    }
}
