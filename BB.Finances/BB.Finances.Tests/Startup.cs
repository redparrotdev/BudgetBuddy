using BB.Finances.WebAPI.Config.MappingProfiles;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BB.Finances.Tests
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(
                typeof(AccountMappingProfile).Assembly,
                typeof(CategoryMappingProfile).Assembly,
                typeof(ExpenseMappingProfile).Assembly);
        }
    }
}
