using BB.Finances.Data;
using BB.Finances.Core.CQRS;
using Microsoft.EntityFrameworkCore;
using Serilog;
using BB.Finances.WebAPI.Middleware;

namespace BB.Finances.WebAPI.Config
{
    public static class ServiceConfig
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddSerilog();
            // Mediatr
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetAccountById>());

            // Automapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // DataBase Context
            services.AddDbContext<FinancesDbContext>(opts =>
                opts.UseSqlServer(config.GetConnectionString("Default")));

            services.AddServices();
        }

        private static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<GlobalExceptionHandlerMiddleware>();
        }
    }
}
