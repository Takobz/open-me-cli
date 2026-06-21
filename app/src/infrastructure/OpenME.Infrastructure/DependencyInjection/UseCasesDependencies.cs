using Microsoft.Extensions.DependencyInjection;
using OpenME.Core.Application.Ports.In;
using OpenME.Core.Application.Ports.Out;
using OpenME.Core.Application.Services;
using OpenME.Data.DatabaseProvider;
using OpenME.Data.Repository;

namespace OpenME.Infrastructure.DependencyInjection
{
    public static class UseCasesDependencies
    {
        public static IServiceCollection AddUserUseCases(
            this IServiceCollection services
        )
        {
            services.AddTransient<ICreateUserUseCase, UserService>();
            services.AddTransient<IGetUserUseCase, UserService>();
            services.AddTransient<ICreateOAuthAuthenticationLinkUseCase, OAuthConfigurationService>();
            services.AddTransient<ICreateUserPort, UserRepository>();
            services.AddTransient<IGetUserPort, UserRepository>();
            services.AddTransient<IDatabaseProvider, InMemoryDatabaseProvider>();
            services.AddMemoryCache();
            return services;
        }
    }
}