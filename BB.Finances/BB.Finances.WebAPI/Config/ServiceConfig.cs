using BB.Finances.Core.Abstractions;
using BB.Finances.Core.Services;
using BB.Finances.Data;
using BB.Finances.Data.CQRS;
using Microsoft.EntityFrameworkCore;

namespace BB.Finances.WebAPI.Config
{
    public static class ServiceConfig
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration config)
        {
            // Mediatr
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<IAccountService>());
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
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ICategoryService, CategoryService>();
        }
    }
}
