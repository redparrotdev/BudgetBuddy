using BB.IdentityService.WebAPI.Helpers;
using BB.IdentityService.WebAPI.Models.Request;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BB.IdentityService.Tests.Fixtures
{
    public static class JwtTokenFixture
    {
        public static string CreateNewJwtToken(IConfiguration config, UserRequestModel model)
        {
            var claims = new List<Claim>
            {
                new Claim("Name", Guid.NewGuid().ToString()),
                new Claim("Email", model.Email)
            };

            var credentials = new SigningCredentials(
                    JWTHelper.GetSymmetricSecurityKey(config["JWT:Key"]!),
                    SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(
                issuer: config["JWT:Issuer"],
                audience: config["JWT:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(24),
                signingCredentials: credentials);

            var handler = new JwtSecurityTokenHandler();

            return handler.WriteToken(jwt);
        }
    }
}
