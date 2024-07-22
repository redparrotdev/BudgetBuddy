using MediatR;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BB.Core.CQRS.JWT
{
    public class CreateJWT : IRequest<string>
    {
        public Guid UserId { get; set; }

        public string Email { get; set; }

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public DateTime Expires { get; set; }

        public SigningCredentials Credentials { get; set; }
    }

    public class CreateJWTHandler : IRequestHandler<CreateJWT, string>
    {

        public async Task<string> Handle(CreateJWT request, CancellationToken cancellationToken)
        {
            ArgumentException.ThrowIfNullOrEmpty(request.Email);
            ArgumentException.ThrowIfNullOrEmpty(request.Issuer);
            ArgumentException.ThrowIfNullOrEmpty(request.Audience);

            if (request.UserId.Equals(Guid.Empty))
                throw new ArgumentException(nameof(request.UserId));

            var claims = new List<Claim>
            {
                new Claim("UserId", request.UserId.ToString()),
                new Claim("Email", request.Email)
            };

            var jwt = new JwtSecurityToken(
                issuer: request.Issuer,
                audience: request.Audience,
                claims: claims,
                expires: request.Expires,
                signingCredentials: request.Credentials);

            var handler = new JwtSecurityTokenHandler();

            var token = handler.WriteToken(jwt);

            return await Task.FromResult(token);
        }
    }
}
