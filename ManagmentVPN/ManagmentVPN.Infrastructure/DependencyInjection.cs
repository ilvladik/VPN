using ManagmentVPN.Domain;
using ManagmentVPN.Domain.Repositories;
using ManagmentVPN.Infrastructure;
using ManagmentVPN.Infrastructure.Repositoies;


namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistance(this IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddScoped<IUnitOfWork, UnitOfWork>()
                .AddScoped<IKeyRepository, KeyRepository>()
                .AddScoped<IServerRepository, ServerRepository>()
                .AddScoped<IUserRepository, UserRepository>();
            return serviceCollection;
        }
    }
}
