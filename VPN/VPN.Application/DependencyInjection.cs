﻿using Microsoft.Extensions.DependencyInjection;
using VPN.Application.Services;

namespace VPN.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddScoped<IKeyService, KeyService>()
                .AddScoped<IServerService, ServerService>();
            return serviceCollection;
        }
    }
}
