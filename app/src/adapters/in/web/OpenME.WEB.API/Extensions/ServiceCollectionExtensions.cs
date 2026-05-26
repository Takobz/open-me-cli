using OpenME.Core.Application.Observability;
using OpenME.WEB.API.Contexts;

namespace OpenME.WEB.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTraceContext(
            this IServiceCollection services
        )
        {
            services.AddScoped<ITraceContext, TraceContext>();
            return services;
        }
    }
}