using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BB.IdentityService.WebAPI.Helpers
{
    public static class JWTHelper
    {
        public static SymmetricSecurityKey GetSymmetricSecurityKey(string key) => 
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
    }
}
