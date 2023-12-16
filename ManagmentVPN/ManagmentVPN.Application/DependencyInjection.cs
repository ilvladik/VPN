using Domain.Repositories;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Application.Services;
using ManagmentVPN.Application.Services;

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