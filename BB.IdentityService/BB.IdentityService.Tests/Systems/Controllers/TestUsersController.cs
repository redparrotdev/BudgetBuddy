using BB.Core.CQRS.JWT;
using BB.IdentityService.Tests.Fixtures;
using BB.IdentityService.WebAPI.Controllers;
using BB.IdentityService.WebAPI.Models.Request;
using FluentAssertions;
using MediatR;
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
        private readonly Mock<IMediator> _mediatorMock;

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

            _mediatorMock = new Mock<IMediator>();
        }

        [Fact]
        public async Task Register_OnSuccess_Returns201StatusCode()
        {
            // Arrange
            _mediatorMock.Setup(m => m.Send(
                It.IsAny<CreateJWT>(),
                default))
                .ReturnsAsync(JwtTokenFixture.CreateNewJwtToken(_config, _mockUsers[0]));

            var sut = new UsersController(_config, _mediatorMock.Object);

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
            _mediatorMock.Setup(m => m.Send(
                It.IsAny<CreateJWT>(),
                default))
                .ReturnsAsync(JwtTokenFixture.CreateNewJwtToken(_config, _mockUsers[1]));

            var sut = new UsersController(_config, _mediatorMock.Object);
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

            var emailClaim = token.Claims.
                Where(c => c.Type == "Email")
                .FirstOrDefault();

            emailClaim.Should()
                .NotBeNull();

            var emailValue = emailClaim!.Value;

            emailValue!.Should()
                .Be(_mockUsers[1].Email);
        }
    }
}