using OpenME.Core.Application.Ports.In;
using OpenME.Core.Application.Services;

namespace OpenME.WEB.API.ServiceCollectionExtensions
{
    public static class ApplicationServices
    {
        public static IServiceCollection AddUseCases(
            this IServiceCollection services
        )
        {
            services.AddTransient<ICreateUserUseCase, UserService>();
            services.AddTransient<ICreateOAuthAuthenticationLinkUseCase, OAuthConfigurationService>();
            return services;
        }
    }
}