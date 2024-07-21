using BB.IdentityService.WebAPI.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace BB.IdentityService.WebAPI.Config
{
    public static class ConfigureServices
    {
        public static void AddServices(this IServiceCollection services, ConfigurationManager configManager)
        {
            services.AddAuthorization();

            // Addind JWT Auth
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = configManager["JWT:Issuer"],
                        ValidateAudience = true,
                        ValidAudience = configManager["JWT:Audience"],
                        ValidateLifetime = true,
                        IssuerSigningKey = JWTHelper.GetSymmetricSecurityKey(configManager["JWT:Key"]!),
                        ValidateIssuerSigningKey = true
                    };
                });
        }

        public static void UseServices(this WebApplication app)
        {
            app.UseAuthentication();
        }
    }
}
