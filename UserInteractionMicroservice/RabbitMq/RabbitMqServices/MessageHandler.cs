
using Application.Interfaces;
using EasyNetQ;
using Sharedmodel;

namespace NetQ
{
    public class MessageHandler : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public MessageHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using (var scope = _serviceProvider.CreateScope())
            {


            var notificationService = scope.ServiceProvider.GetRequiredService<INotificationService>();

                

            messageClient.Listen<NotificationDto>(OnMessageReceived, "notificationCreation");

            void OnMessageReceived(NotificationDto notificationDto)
            {
                try
                {
                    Console.WriteLine("Notification received");
                var connectionStr = "amqp://guest:guest@rabbitmq:5672/";

                // Assuming MessageClient is correctly set up to create an EasyNetQ bus.
                var messageClient = new MessageClient(RabbitHutch.CreateBus(connectionStr));

                // Adjusted to listen for NotificationDto instead of string.
                messageClient.Listen<NotificationDto>(
                    OnMessageReceived,
                    "notificationSubscription"
                );

                void OnMessageReceived(NotificationDto notification)
                {
                    try
                    {
                        notificationService.CreateNotification(notification);
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
}
