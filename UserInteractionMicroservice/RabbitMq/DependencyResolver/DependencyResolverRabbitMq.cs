using RabbitMq.RabbitMqServices;
using RabbitMq.RabbitMqIServices;

namespace RabbitMq.DependencyResolver
{
    public static class DependencyResolverRabbitMq
    {
        public static void RegisterRabbitMqLayer(this IServiceCollection services, string queueName)
        {
            services.AddScoped<IRabbitMqSender>(provider => new RabbitMqSender(queueName));
            services.AddScoped<IRabbitMqReceiver>(provider => new RabbitMqReceiver(queueName));
        }
    }
}
