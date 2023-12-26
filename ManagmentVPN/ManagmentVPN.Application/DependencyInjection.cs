
using Application.Services;
using ManagmentVPN.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddScoped<IServerService, ServerService>()
                .AddScoped<IUserService, UserService>()
                .AddScoped<IKeyService, KeyService>();
            return serviceCollection;
        }
    }
}