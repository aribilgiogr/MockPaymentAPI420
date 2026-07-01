using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Business.Middlewares
{
    public static class IoC
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Register your business services here
            return services;
        }

        public static IApplicationBuilder UseBusinessServices(this IApplicationBuilder app)
        {
            // Configure your business services here
            return app;
        }
    }
}
