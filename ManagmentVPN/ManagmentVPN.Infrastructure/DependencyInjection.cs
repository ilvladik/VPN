using ManagmentVPN.Domain;
using ManagmentVPN.Domain.Repositories;
using ManagmentVPN.Infrastructure;
using ManagmentVPN.Infrastructure.Context;
using ManagmentVPN.Infrastructure.Repositoies;
using Microsoft.EntityFrameworkCore;


namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection serviceCollection)
        {
            string mysqlConnectionString = "server=localhost;database=managmentvpn;user=root;password=ilyin";
            serviceCollection
                .AddDbContext<ApplicationDbContext>(o => o.UseMySql(mysqlConnectionString, ServerVersion.AutoDetect(mysqlConnectionString)))
                .AddScoped<IUnitOfWork, UnitOfWork>()
                .AddScoped<IKeyRepository, KeyRepository>()
                .AddScoped<IServerRepository, ServerRepository>()
                .AddScoped<IUserRepository, UserRepository>();
            return serviceCollection;
        }
    }
}
