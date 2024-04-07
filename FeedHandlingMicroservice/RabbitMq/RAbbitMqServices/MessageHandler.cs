using EasyNetQ;
using FeedHandlingMicroservice.Models;
using NetQ;
using Sharedmodel;

namespace FeedHandlingMicroservice.RabbitMq.RabbitMqServices;

public class MessageHandler : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            var connectionStr = "amqp://guest:guest@rabbitmq:5672/";
            var bus = RabbitHutch.CreateBus(connectionStr);

            var messageClient = new MessageClient(bus);

            // Example subscription, replace 'YourMessageType' with your actual message type.
            // The "subscriptionId" should be unique for each type of subscription.
            messageClient.Listen<NotificationDto>(message =>
            {
                // Process message here
                Console.WriteLine($"Received message: {message}");
            }, "subscriptionId");

            // Keep the service running
            while (!stoppingToken.IsCancellationRequested)
            {
                Console.WriteLine("MessageHandler is listening.");
                await Task.Delay(1000, stoppingToken);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred: {e.Message}");
            throw;
        }
        finally
        {
            Console.WriteLine("MessageHandler is stopping.");
        }
    }
}