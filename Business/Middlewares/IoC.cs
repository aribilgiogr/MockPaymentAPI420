using Business.Services;
using Core.Abstracts;
using Core.Abstracts.IRepositories;
using Core.Abstracts.IServices;
using Data;
using Data.Contexts;
using Data.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Business.Middlewares
{
    public static class IoC
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Register your business services here
            services.AddDbContext<PaymentDbContext>(options => options.UseSqlite(configuration.GetConnectionString("data")));
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IPaymentService, PaymentService>();
            return services;
        }

        public static IApplicationBuilder UseBusinessServices(this IApplicationBuilder app)
        {
            // Configure your business services here
            app.UseMiddleware<GlobalExceptionHandling>();
            app.UseMiddleware<ApiKeyHandler>();
            return app;
        }
    }
}
