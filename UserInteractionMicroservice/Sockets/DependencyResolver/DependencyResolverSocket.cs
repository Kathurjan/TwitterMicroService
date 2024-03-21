using Infrastructure.IRepositories;
using Infrastructure.Repositories;


namespace Infastructure.DependencyResolver
{
    public static class DependencyResolverInfastructure
    {
        public static void RegisterInfastructureLayer(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<INotificationRepo, NotificationRepo>();
        }
    }
}
