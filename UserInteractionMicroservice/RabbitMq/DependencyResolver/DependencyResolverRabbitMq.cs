using RabbitMq.RabbitMqServices;
using RabbitMq.RabbitMqIServices;

namespace RabbitMq.DependencyResolver
{
    public static class DependencyResolverRabbitMq
    {
        public static void RegisterRabbitMqLayer(this IServiceCollection services)
        {
            services.AddScoped<IRabbitMqSender, RabbitMqSender>();
            services.AddScoped<IRabbitMqReceiver, RabbitMqReceiver>();
        }
    }
}
