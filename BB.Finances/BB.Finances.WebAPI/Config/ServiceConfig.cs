using BB.Finances.Data;
using Microsoft.EntityFrameworkCore;

namespace BB.Finances.WebAPI.Config
{
    public static class ServiceConfig
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration config)
        {
            // Mediatr
            /*services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<IFinanceRecordService>());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetRecordsByUserId>());*/

            // Automapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // DataBase Context
            services.AddDbContext<FinancesDbContext>(opts =>
                opts.UseSqlServer(config.GetConnectionString("Default")));

            services.AddServices();
        }

        private static void AddServices(this IServiceCollection services)
        {
            
        }
    }
}
