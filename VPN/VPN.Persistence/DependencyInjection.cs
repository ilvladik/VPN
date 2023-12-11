using Microsoft.EntityFrameworkCore;
using VPN.Domain;
using VPN.Domain.Repositories;
using VPN.Persistence;
using VPN.Persistence.Context;
using VPN.Persistence.Repositoies;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistance(this IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddDbContext<ApplicationDbContext>(options =>
                    options.UseNpgsql("Host=db;Database=vpndb;Username=postgres;Password=postgres"))
                .AddScoped<IUnitOfWork, UnitOfWork>()
                .AddScoped<IKeyRepository, KeyRepositoriy>()
                .AddScoped<IServerRepository, ServerRepositoriy>();
            return serviceCollection;
        }
    }
}
