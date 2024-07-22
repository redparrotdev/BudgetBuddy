using BB.Core.CQRS.JWT;
using BB.IdentityService.WebAPI.Helpers;
using BB.IdentityService.WebAPI.Models.Request;
using MediatR;
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
        private readonly IMediator _mediator;

        public UsersController(IConfiguration configuration, IMediator mediator)
        {
            _configuration = configuration;
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(model);
            }
            // Register new user using user service

            // Create new jwt token for user
            var credentials = new SigningCredentials(
                    JWTHelper.GetSymmetricSecurityKey(_configuration["JWT:Key"]!),
                    SecurityAlgorithms.HmacSha256);

            var result = await _mediator.Send(new CreateJWT
            {
                UserId = Guid.NewGuid(),
                Email = model.Email,
                Issuer = _configuration["JWT:Issuer"] ?? "bb.identity",
                Audience = _configuration["JWT:Adience"] ?? "bb.finances",
                Expires = DateTime.UtcNow.AddHours(24),
                Credentials = credentials
            });

            return Created("", result);
        }
    }
}
