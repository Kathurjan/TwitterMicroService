using SharedLibrary;
using EasyNetQ;

namespace NetQ
{
    public class MessageHandler : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var connectionStr = "amqp://guest:guest@localhost:5672/";

            var messageClient = new MessageClient(RabbitHutch.CreateBus(connectionStr));

            messageClient.Listen<NotificationDto>(OnMessageReceived, "notificationCreation");

            void OnMessageReceived(NotificationDto notificationDto)
            {
                try
                {
                    Console.WriteLine("Notification received");

                    Console.WriteLine(notificationDto);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            while (!stoppingToken.IsCancellationRequested)
            {
                Console.WriteLine("MessageHandler is listening.");
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
