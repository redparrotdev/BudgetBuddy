using BB.IdentityService.WebAPI.Controllers;
using BB.IdentityService.WebAPI.Models.Request;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using System.IdentityModel.Tokens.Jwt;

namespace BB.IdentityService.Tests.Systems.Controllers
{
    public class TestUsersController
    {
        private readonly IConfiguration _config;

        private readonly List<UserRequestModel> _mockUsers = new List<UserRequestModel>
        {
            new UserRequestModel
            {
                Email = "TestUser@gmail.com",
                Password = "123pass123",
                ConfirmPassword = "123pass123"
            },
            new UserRequestModel
            {
                Email = "TestUser222@gmail.com",
                Password = "123pass123232",
                ConfirmPassword = "123pass123232"
            }
        };

        public TestUsersController()
        {
            var configSettings = new Dictionary<string, string?>
            {
                {"JWT:Key", "rV7e0PQifASFQNXI5xcM8OjLo8plqKTp"}, // This key is now a real one
                {"JWT:Issuer", "issuer"},
                {"JWT:Audience", "audience"}
            };

            _config = new ConfigurationBuilder()
                .AddInMemoryCollection(configSettings)
                .Build();
        }

        [Fact]
        public async Task Register_OnSuccess_Returns201StatusCode()
        {
            // Arrange
            var sut = new UsersController(_config);

            // Act
            var result = (ObjectResult)await sut.Register(_mockUsers[0]);

            // Assert
            result.Should()
                .NotBeNull();
            result!.StatusCode
                .Should()
                .NotBeNull();
            result!.StatusCode!
                .Should()
                .Be(StatusCodes.Status201Created);
        }

        [Fact]
        public async Task Register_OnSuccess_CreatesValidJWT()
        {
            // Arrange
            var sut = new UsersController(_config);
            var jwtHandler = new JwtSecurityTokenHandler();

            // Act
            var result = (ObjectResult)await sut.Register(_mockUsers[1]);

            // Assert

            result.Should()
                .NotBeNull();
            result!.Value
                .Should()
                .NotBeNull();

            result!.Value!
                .Should()
                .BeOfType<string>();

            // Token validation
            var token = jwtHandler.ReadJwtToken((string)result.Value!);

            token.Should()
                .NotBeNull();

            // Issuer
            token.Issuer
                .Should()
                .Be("issuer");

            // Audience
            token.Audiences
                .Should()
                .NotBeNullOrEmpty();

            token.Audiences
                .Should()
                .Contain("audience");

            // Claims

            token.Claims
                .Should()
                .NotBeNullOrEmpty();
        }
    }
}