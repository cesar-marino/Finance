using Finance.Application.Services;
using Finance.Application.UseCases.User.GetCurrentUser;
using Finance.Domain.Exceptions;
using Finance.Domain.Repositories;
using FluentAssertions;
using Moq;

namespace Finance.Test.UnitTest.Application.UseCases.User.GetCurrentUser
{
    public class GetCurrentUserHandlerTest : IClassFixture<GetCurrentUserHandlerTestFixture>
    {
        private readonly GetCurrentUserHandlerTestFixture _fixture;
        private readonly GetCurrentUserHandler _sut;
        private readonly Mock<ITokenService> _tokenServiceMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;

        public GetCurrentUserHandlerTest(GetCurrentUserHandlerTestFixture fixture)
        {
            _fixture = fixture;
            _tokenServiceMock = new();
            _userRepositoryMock = new();

            _sut = new(
                tokenService: _tokenServiceMock.Object,
                userRepository: _userRepositoryMock.Object);
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatGetUsernameFromTokenAsyncThrows))]
        [Trait("Unit/UseCase", "User - GetCurrentUser")]
        public async Task ShouldRethrowSameExceptionThatGetUsernameFromTokenAsyncThrows()
        {
            _tokenServiceMock
                .Setup(x => x.GetUsernameFromTokenAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new UnexpectedException());

            var request = _fixture.MakeGetCurrentUserRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatFindByUsernameAsyncThrows))]
        [Trait("Unit/UseCase", "User - GetCurrentUser")]
        public async Task ShouldRethrowSameExceptionThatFindByUsernameAsyncThrows()
        {
            var username = _fixture.Faker.Internet.UserName();
            _tokenServiceMock
                .Setup(x => x.GetUsernameFromTokenAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(username);

            _userRepositoryMock
                .Setup(x => x.FindByUsernameAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new NotFoundException("User"));

            var request = _fixture.MakeGetCurrentUserRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<NotFoundException>()
                .Where(x => x.Code == "not-found")
                .WithMessage("User not found");
        }

        [Fact(DisplayName = nameof(ShouldReturnTheCorrectResponseIfUserIsFound))]
        [Trait("Unit/UseCase", "User - GetCurrentUser")]
        public async Task ShouldReturnTheCorrectResponseIfUserIsFound()
        {
            var username = _fixture.Faker.Internet.UserName();
            _tokenServiceMock
                .Setup(x => x.GetUsernameFromTokenAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(username);

            var user = _fixture.MakeUserEntity();
            _userRepositoryMock
                .Setup(x => x.FindByUsernameAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);

            var request = _fixture.MakeGetCurrentUserRequest();
            var response = await _sut.Handle(request, _fixture.CancellationToken);

            response.AccessToken.Should().NotBeNull();
            response.AccessToken?.Value.Should().Be(user.AccessToken?.Value);
            response.AccessToken?.ExpiresIn.Should().Be(user.AccessToken?.ExpiresIn);
            response.UserId.Should().Be(user.Id);
            response.Active.Should().BeTrue();
            response.CreatdAt.Should().Be(user.CreatedAt);
            response.Email.Should().Be(user.Email);
            response.EmailConfirmed.Should().Be(user.EmailConfirmed);
            response.Phone.Should().Be(user.Phone);
            response.PhoneConfirmed.Should().Be(user.PhoneConfirmed);
            response.RefreshToken.Should().NotBeNull();
            response.RefreshToken?.Value.Should().Be(user.RefreshToken?.Value);
            response.RefreshToken?.ExpiresIn.Should().Be(user.RefreshToken?.ExpiresIn);
            response.Role.Should().Be(user.Role);
            response.Username.Should().Be(user.Username);
        }
    }
}