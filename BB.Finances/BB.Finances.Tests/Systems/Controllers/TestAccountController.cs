using AutoMapper;
using BB.Finances.Core.CQRS;
using BB.Finances.Data.Entities;
using BB.Finances.Data.Exceptions;
using BB.Finances.Tests.Fixtures;
using BB.Finances.WebAPI.Controllers;
using BB.Finances.WebAPI.Models.Response;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BB.Finances.Tests.Systems.Controllers
{
    public class TestAccountController
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly IMapper _mapper;

        public TestAccountController(IMapper mapper)
        {
            _mediatorMock = new Mock<IMediator>();
            _mapper = mapper;
        }

        [Fact]
        public async Task Details_OnSuccess_ReturnsAccountResponseModel()
        {
            // Arrange
            _mediatorMock.Setup(m => m.Send(
                It.IsAny<GetAccountById>(),
                default))
                .ReturnsAsync(AccountFixtures.GetEntityFixture());

            var sut = new AccountsController(_mediatorMock.Object, _mapper);

            // Act
            var result = (ObjectResult)await sut.Details(Guid.NewGuid());

            // Assert
            result.Should()
                .NotBeNull();
            result!.StatusCode.Should()
                .Be(StatusCodes.Status200OK);
            result!.Value?.Should()
                .NotBeNull();
            result!.Value!.GetType()
                .Should()
                .Be(typeof(AccountResponseModel));
        }

        [Fact]
        public async Task Details_WithEmptyGuid_ThrowsArgumentException()
        {
            // Arrange
            _mediatorMock.Setup(m => m.Send(
                It.IsAny<GetAccountById>(),
                default))
                .ReturnsAsync(AccountFixtures.GetEntityFixture());

            var sut = new AccountsController(_mediatorMock.Object, _mapper);

            // Assert
            await sut.Invoking(s => s.Details(Guid.Empty))
                .Should().ThrowAsync<ArgumentException>();

        }

        [Fact]
        public async Task Create_OnSuccess_ReturnsAccountResponseModel()
        {
            // Arrange
            _mediatorMock.Setup(m => m.Send(
                It.IsAny<CreateAccount>(),
                default))
                .ReturnsAsync(1);

            var sut = new AccountsController(_mediatorMock.Object, _mapper);

            // Act
            var result = (CreatedResult) await sut.Create(AccountFixtures.GetRequestFixture());

            // Assert
            result.StatusCode.Should()
                .Be(StatusCodes.Status201Created);
            result.Value.Should()
                .NotBeNull();
            result.Value!.GetType()
                .Should()
                .Be(typeof(AccountResponseModel));
        }

        [Fact]
        public async Task Create_OnCreationError_ThrowsGeneralException()
        {
            // Arrange
            _mediatorMock.Setup(m => m.Send(
                It.IsAny<CreateAccount>(),
                default))
                .ReturnsAsync(0);

            var sut = new AccountsController(_mediatorMock.Object, _mapper);

            // Assert
            await sut.Invoking(s => s.Create(AccountFixtures.GetRequestFixture()))
                .Should().ThrowAsync<GeneralException>();
        }

        [Fact]
        public async Task GetAll_OnSuccess_ReturnsCollectionOfAccountResponseModels()
        {
            // Arrange
            _mediatorMock.Setup(m => m.Send(
                It.IsAny<GetAccountsByUserId>(),
                default))
                .ReturnsAsync(AccountFixtures.GetEntityFixturesList());

            var sut = new AccountsController(_mediatorMock.Object, _mapper);

            // Act
            var result = (ObjectResult)await sut.GetAll(Guid.NewGuid());

            // Assert
            result.Should()
                .NotBeNull();
            result!.StatusCode.Should()
                .Be(StatusCodes.Status200OK);
            result!.Value.Should()
                .NotBeNull();

            result!.Value!.Should()
                .BeAssignableTo<IEnumerable<AccountResponseModel>>();

            ((IEnumerable<AccountResponseModel>)result!.Value!).Should()
                .NotBeEmpty();
        }

        [Fact]
        public async Task GetAll_WithEmptyId_ShouldThrowArgumentException()
        {
            // Arrange
            _mediatorMock.Setup(m => m.Send(
                It.IsAny<GetAccountsByUserId>(),
                default))
                .ReturnsAsync(AccountFixtures.GetEntityFixturesList());

            var sut = new AccountsController(_mediatorMock.Object, _mapper);

            // Assert
            await sut.Invoking(s => s.GetAll(Guid.Empty))
                .Should()
                .ThrowAsync<ArgumentException>();
        }
    }
}