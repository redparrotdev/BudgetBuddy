using BB.IdentityService.WebAPI.Helpers;
using BB.IdentityService.WebAPI.Models.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BB.IdentityService.WebAPI.Controllers
{
    [Route("users/")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public UsersController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRequestModel model)
        {
            var claims = new List<Claim>
            {
                new Claim("Email", model.Email),
                new Claim("UserId", Guid.NewGuid().ToString())
            };

            var jwt = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(15),
                signingCredentials: new SigningCredentials(
                    JWTHelper.GetSymmetricSecurityKey(_configuration["JWT:Key"]!),
                    SecurityAlgorithms.HmacSha256));

            return Created("", new JwtSecurityTokenHandler().WriteToken(jwt));
        }
    }
}
