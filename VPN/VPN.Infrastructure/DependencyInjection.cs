using Microsoft.EntityFrameworkCore;
using VPN.Domain;
using VPN.Domain.Repositories;
using VPN.Infrastructure;
using VPN.Infrastructure.Context;
using VPN.Infrastructure.Repositoies;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddDbContext<ApplicationDbContext>(options =>
                    options.UseMySql("Server=VpnApiData;Database=VpnApiDatabase;User=vpn;Password=12345;", 
                    ServerVersion.AutoDetect("Server=VpnApiData;Database=VpnApiDatabase;User=vpn;Password=12345;")))
                .AddScoped<IUnitOfWork, UnitOfWork>()
                .AddScoped<IKeyRepository, KeyRepositoriy>()
                .AddScoped<IServerRepository, ServerRepositoriy>();
            return serviceCollection;
        }
    }
}
