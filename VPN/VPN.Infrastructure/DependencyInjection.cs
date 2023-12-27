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
            string mysqlConnectionString = "server=localhost;database=vpn;user=root;password=ilyin";
            serviceCollection
                .AddDbContext<ApplicationDbContext>(
                    o => o.UseMySql(mysqlConnectionString, 
                    ServerVersion.AutoDetect(mysqlConnectionString)))
                .AddScoped<IUnitOfWork, UnitOfWork>()
                .AddScoped<IKeyRepository, KeyRepositoriy>()
                .AddScoped<IServerRepository, ServerRepositoriy>();
            return serviceCollection;
        }
    }
}
