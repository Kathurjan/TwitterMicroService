using EasyNetQ;
using SharedLibrary;


namespace NetQ
{
    public class MessageClient
    {
        private readonly IBus _bus;

  

        public MessageClient(IBus bus)
        {
            _bus = bus;
        }

        public void Listen<T>(Action<T> handler, string queueName)
        {
            _bus.PubSub.Subscribe(queueName, handler);
        }

        public void Publish<NotificationDto>(NotificationDto message, string queueName)
        {
            Console.WriteLine(message);
            _bus.PubSub.Publish("message", queueName);
            Console.WriteLine("creationNotification");
            Console.WriteLine("Message published");
        }
      
    }
}