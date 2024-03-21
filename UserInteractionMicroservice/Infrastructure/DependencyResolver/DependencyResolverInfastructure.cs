using Infrastructure.IRepositories;
using Infrastructure.Repositories;


namespace Infastructure.DependencyResolver
{
    public static class DependencyResolverInfrastruce
    {
        public static void RegistInfrastructure(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<INotificationRepo, NotificationRepo>();
        }
    }
}
