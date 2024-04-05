using DTO;
using EasyNetQ;
namespace NetQ
{
    public class MessageHandler : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var connectionStr = "amqp://guest:guest@localhost:5672/";

            // Assuming MessageClient is correctly set up to create an EasyNetQ bus.
            var messageClient = new MessageClient(RabbitHutch.CreateBus(connectionStr));

            // Adjusted to listen for NotificationDto instead of string.
            messageClient.Listen<NotificationDto>(OnMessageReceived, "notificationSubscription");

            void OnMessageReceived(NotificationDto notification)
            {
                try
                {
                    // Now directly processing NotificationDto object.
                    Console.WriteLine($"Notification received for UserId: {notification.UserId}, Message: {notification.Message}");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            while (!stoppingToken.IsCancellationRequested)
            {
                Console.WriteLine("MessageHandler is listening for notifications.");
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}