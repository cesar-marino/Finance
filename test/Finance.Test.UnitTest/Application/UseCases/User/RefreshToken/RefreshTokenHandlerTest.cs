using Finance.Application.Services;
using Finance.Application.UseCases.User.RefreshToken;
using Finance.Domain.Entities;
using Finance.Domain.Exceptions;
using Finance.Domain.Repositories;
using Finance.Domain.SeedWork;
using FluentAssertions;
using Moq;

namespace Finance.Test.UnitTest.Application.UseCases.User.RefreshToken
{
    public class RefreshTokenHandlerTest : IClassFixture<RefreshTokenHandlerTestFixture>
    {
        private readonly RefreshTokenHandlerTestFixture _fixture;
        private readonly RefreshTokenHandler _sut;
        private readonly Mock<ITokenService> _tokenServiceMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public RefreshTokenHandlerTest(RefreshTokenHandlerTestFixture fixture)
        {
            _fixture = fixture;
            _tokenServiceMock = new();
            _userRepositoryMock = new();
            _unitOfWorkMock = new();

            _sut = new(
                tokenService: _tokenServiceMock.Object,
                userRepository: _userRepositoryMock.Object,
                unitOfWork: _unitOfWorkMock.Object);
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatGetUsernameFromTokenAsyncThrows))]
        [Trait("Unit/UseCase", "User - RefreshToken")]
        public async void ShouldRethrowSameExceptionThatGetUsernameFromTokenAsyncThrows()
        {
            _tokenServiceMock
                .Setup(x => x.GetUsernameFromTokenAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new UnexpectedException());

            var request = _fixture.MakeRefreshTokenRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatFindByUsernameAsyncThrows))]
        [Trait("Unit/UseCase", "User - RefreshToken")]
        public async void ShouldRethrowSameExceptionThatFindByUsernameAsyncThrows()
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

            var request = _fixture.MakeRefreshTokenRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<NotFoundException>()
                .Where(x => x.Code == "not-found")
                .WithMessage("User not found");
        }

        [Fact(DisplayName = nameof(ShouldThrowUnauthorizedExceptionIfRefreshTokenIsInvalid))]
        [Trait("Unit/UseCase", "User - RefreshToken")]
        public async Task ShouldThrowUnauthorizedExceptionIfRefreshTokenIsInvalid()
        {
            var refreshToken = _fixture.MakeUserToken(
                value: _fixture.Faker.Random.Guid().ToString(),
                expiresIn: _fixture.Faker.Date.Past());

            var username = _fixture.Faker.Internet.UserName();
            _tokenServiceMock
                .Setup(x => x.GetUsernameFromTokenAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(username);

            var user = _fixture.MakeUserEntity(refreshToken: refreshToken);
            _userRepositoryMock
                .Setup(x => x.FindByUsernameAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);

            var request = _fixture.MakeRefreshTokenRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnauthorizedException>()
                .Where(x => x.Code == "unauthorized")
                .WithMessage("Unauthorized access");
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatGenerateAccessTokenAsyncThrows))]
        [Trait("Unit/UseCase", "User - RefreshToken")]
        public async void ShouldRethrowSameExceptionThatGenerateAccessTokenAsyncThrows()
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

            _tokenServiceMock
                .Setup(x => x.GenerateAccessTokenAsync(
                    It.IsAny<UserEntity>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new UnexpectedException());

            var request = _fixture.MakeRefreshTokenRequest(refreshToken: user.RefreshToken?.Value);
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatGenerateRefreshTokenAsyncThrows))]
        [Trait("Unit/UseCase", "User - RefreshToken")]
        public async void ShouldRethrowSameExceptionThatGenerateRefreshTokenAsyncThrows()
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

            var accessToken = _fixture.MakeUserToken();
            _tokenServiceMock
                .Setup(x => x.GenerateAccessTokenAsync(
                    It.IsAny<UserEntity>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(accessToken);

            _tokenServiceMock
                .Setup(x => x.GenerateRefreshTokenAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(new UnexpectedException());

            var request = _fixture.MakeRefreshTokenRequest(refreshToken: user.RefreshToken?.Value);
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatUpdateAsyncThrows))]
        [Trait("Unit/UseCase", "User - RefreshToken")]
        public async void ShouldRethrowSameExceptionThatUpdateAsyncThrows()
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

            var accessToken = _fixture.MakeUserToken();
            _tokenServiceMock
                .Setup(x => x.GenerateAccessTokenAsync(
                    It.IsAny<UserEntity>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(accessToken);

            var refreshToken = _fixture.MakeUserToken();
            _tokenServiceMock
                .Setup(x => x.GenerateRefreshTokenAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(refreshToken);

            _userRepositoryMock
                .Setup(x => x.UpdateAsync(
                    It.IsAny<UserEntity>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new UnexpectedException());

            var request = _fixture.MakeRefreshTokenRequest(refreshToken: user.RefreshToken?.Value);
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatCommitAsyncThrows))]
        [Trait("Unit/UseCase", "User - RefreshToken")]
        public async void ShouldRethrowSameExceptionThatCommitAsyncThrows()
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

            var accessToken = _fixture.MakeUserToken();
            _tokenServiceMock
                .Setup(x => x.GenerateAccessTokenAsync(
                    It.IsAny<UserEntity>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(accessToken);

            var refreshToken = _fixture.MakeUserToken();
            _tokenServiceMock
                .Setup(x => x.GenerateRefreshTokenAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(refreshToken);

            _unitOfWorkMock
                .Setup(x => x.CommitAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(new UnexpectedException());

            var request = _fixture.MakeRefreshTokenRequest(refreshToken: user.RefreshToken?.Value);
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }

        [Fact(DisplayName = nameof(ShouldReturnTheCorrectResponseIfTokensAreUpdatedSuccessfully))]
        [Trait("Unit/UseCase", "User - RefreshToken")]
        public async void ShouldReturnTheCorrectResponseIfTokensAreUpdatedSuccessfully()
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

            var accessToken = _fixture.MakeUserToken();
            _tokenServiceMock
                .Setup(x => x.GenerateAccessTokenAsync(
                    It.IsAny<UserEntity>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(accessToken);

            var refreshToken = _fixture.MakeUserToken();
            _tokenServiceMock
                .Setup(x => x.GenerateRefreshTokenAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(refreshToken);

            var request = _fixture.MakeRefreshTokenRequest(refreshToken: user.RefreshToken?.Value);
            var response = await _sut.Handle(request, _fixture.CancellationToken);

            response.AccessToken.Should().NotBeNull();
            response.AccessToken?.Value.Should().Be(accessToken.Value);
            response.AccessToken?.ExpiresIn.Should().Be(accessToken.ExpiresIn);
            response.UserId.Should().Be(user.Id);
            response.Active.Should().Be(user.Active);
            response.CreatdAt.Should().Be(user.CreatedAt);
            response.Email.Should().Be(user.Email);
            response.EmailConfirmed.Should().Be(user.EmailConfirmed);
            response.Phone.Should().Be(user.Phone);
            response.PhoneConfirmed.Should().Be(user.PhoneConfirmed);
            response.RefreshToken.Should().NotBeNull();
            response.RefreshToken?.Value.Should().Be(refreshToken.Value);
            response.RefreshToken?.ExpiresIn.Should().Be(refreshToken.ExpiresIn);
            response.Role.Should().Be(user.Role);
            response.Username.Should().Be(user.Username);
        }
    }
}