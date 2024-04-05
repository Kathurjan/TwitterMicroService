using EasyNetQ;

namespace NetQ;
public class MessageHandler : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            var connectionStr = "amqp://guest:guest@localhost:5672/";

            var messageClient = new MessageClient(RabbitHutch.CreateBus(connectionStr));


            while (!stoppingToken.IsCancellationRequested)
            {
                Console.WriteLine("MessageHandler is listening.");
                await Task.Delay(1000, stoppingToken);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }

        Console.WriteLine("MessageHandler is stopping.");


    }
}
