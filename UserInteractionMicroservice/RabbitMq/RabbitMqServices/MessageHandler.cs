using Application.Interfaces;
using DTO;
using EasyNetQ;
using Monitor;

namespace NetQ
{
    public class MessageHandler : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public MessageHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider =
                serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                var notificationService =
                    serviceProvider.GetRequiredService<INotificationService>();

                var messageClient = new MessageClient(RabbitHutch.CreateBus("host=localhost"));



                void OnMessageReceived(NotificationDto notificationDto)
                {
                    try
                    {
                        MonitorService.Log.Information("Received message: {0}", notificationDto);
                        notificationService.CreateNotification(notificationDto);
                    }
                    catch (Exception e)
                    {
                        MonitorService.Log.Error(e.Message);
                    }
                }

                messageClient.Listen<NotificationDto>(OnMessageReceived, "notificationCreation");

                while (!stoppingToken.IsCancellationRequested)
                {
                    MonitorService.Log.Information("Listening for messages");
                    await Task.Delay(1000, stoppingToken);
                }

                MonitorService.Log.Information("Stopped listening for messages");
            }
        }
    }
}
