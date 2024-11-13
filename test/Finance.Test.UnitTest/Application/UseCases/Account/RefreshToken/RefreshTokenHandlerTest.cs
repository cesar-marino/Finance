using Finance.Application.Services;
using Finance.Application.UseCases.Account.RefreshToken;
using Finance.Domain.Entities;
using Finance.Domain.Exceptions;
using Finance.Domain.Repositories;
using Finance.Domain.SeedWork;
using FluentAssertions;
using Moq;

namespace Finance.Test.UnitTest.Application.UseCases.Account.RefreshToken
{
    public class RefreshTokenHandlerTest : IClassFixture<RefreshTokenHandlerTestFixture>
    {
        private readonly RefreshTokenHandlerTestFixture _fixture;
        private readonly RefreshTokenHandler _sut;
        private readonly Mock<ITokenService> _tokenServiceMock;
        private readonly Mock<IAccountRepository> _accountRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public RefreshTokenHandlerTest(RefreshTokenHandlerTestFixture fixture)
        {
            _fixture = fixture;
            _tokenServiceMock = new();
            _accountRepositoryMock = new();
            _unitOfWorkMock = new();

            _sut = new(
                tokenService: _tokenServiceMock.Object,
                accountRepository: _accountRepositoryMock.Object,
                unitOfWork: _unitOfWorkMock.Object);
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatGetUsernameFromTokenAsyncThrows))]
        [Trait("Unit/UseCase", "Account - RefreshToken")]
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
        [Trait("Unit/UseCase", "Account - RefreshToken")]
        public async void ShouldRethrowSameExceptionThatFindByUsernameAsyncThrows()
        {
            var username = _fixture.Faker.Internet.UserName();
            _tokenServiceMock
                .Setup(x => x.GetUsernameFromTokenAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(username);

            _accountRepositoryMock
                .Setup(x => x.FindByUsernameAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new NotFoundException("Account"));

            var request = _fixture.MakeRefreshTokenRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<NotFoundException>()
                .Where(x => x.Code == "not-found")
                .WithMessage("Account not found");
        }

        [Fact(DisplayName = nameof(ShouldThrowUnauthorizedExceptionIfRefreshTokenIsInvalid))]
        [Trait("Unit/UseCase", "Account - RefreshToken")]
        public async Task ShouldThrowUnauthorizedExceptionIfRefreshTokenIsInvalid()
        {
            var refreshToken = _fixture.MakeAccountToken(
                value: _fixture.Faker.Random.Guid().ToString(),
                expiresIn: _fixture.Faker.Date.Past());

            var username = _fixture.Faker.Internet.UserName();
            _tokenServiceMock
                .Setup(x => x.GetUsernameFromTokenAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(username);

            var account = _fixture.MakeAccountEntity(refreshToken: refreshToken);
            _accountRepositoryMock
                .Setup(x => x.FindByUsernameAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(account);

            var request = _fixture.MakeRefreshTokenRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnauthorizedException>()
                .Where(x => x.Code == "unauthorized")
                .WithMessage("Unauthorized access");
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatGenerateAccessTokenAsyncThrows))]
        [Trait("Unit/UseCase", "Account - RefreshToken")]
        public async void ShouldRethrowSameExceptionThatGenerateAccessTokenAsyncThrows()
        {
            var username = _fixture.Faker.Internet.UserName();
            _tokenServiceMock
                .Setup(x => x.GetUsernameFromTokenAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(username);

            var account = _fixture.MakeAccountEntity();
            _accountRepositoryMock
                .Setup(x => x.FindByUsernameAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(account);

            _tokenServiceMock
                .Setup(x => x.GenerateAccessTokenAsync(
                    It.IsAny<AccountEntity>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new UnexpectedException());

            var request = _fixture.MakeRefreshTokenRequest(refreshToken: account.RefreshToken?.Value);
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatGenerateRefreshTokenAsyncThrows))]
        [Trait("Unit/UseCase", "Account - RefreshToken")]
        public async void ShouldRethrowSameExceptionThatGenerateRefreshTokenAsyncThrows()
        {
            var username = _fixture.Faker.Internet.UserName();
            _tokenServiceMock
                .Setup(x => x.GetUsernameFromTokenAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(username);

            var account = _fixture.MakeAccountEntity();
            _accountRepositoryMock
                .Setup(x => x.FindByUsernameAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(account);

            var accessToken = _fixture.MakeAccountToken();
            _tokenServiceMock
                .Setup(x => x.GenerateAccessTokenAsync(
                    It.IsAny<AccountEntity>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(accessToken);

            _tokenServiceMock
                .Setup(x => x.GenerateRefreshTokenAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(new UnexpectedException());

            var request = _fixture.MakeRefreshTokenRequest(refreshToken: account.RefreshToken?.Value);
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatUpdateAsyncThrows))]
        [Trait("Unit/UseCase", "Account - RefreshToken")]
        public async void ShouldRethrowSameExceptionThatUpdateAsyncThrows()
        {
            var username = _fixture.Faker.Internet.UserName();
            _tokenServiceMock
                .Setup(x => x.GetUsernameFromTokenAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(username);

            var account = _fixture.MakeAccountEntity();
            _accountRepositoryMock
                .Setup(x => x.FindByUsernameAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(account);

            var accessToken = _fixture.MakeAccountToken();
            _tokenServiceMock
                .Setup(x => x.GenerateAccessTokenAsync(
                    It.IsAny<AccountEntity>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(accessToken);

            var refreshToken = _fixture.MakeAccountToken();
            _tokenServiceMock
                .Setup(x => x.GenerateRefreshTokenAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(refreshToken);

            _accountRepositoryMock
                .Setup(x => x.UpdateAsync(
                    It.IsAny<AccountEntity>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new UnexpectedException());

            var request = _fixture.MakeRefreshTokenRequest(refreshToken: account.RefreshToken?.Value);
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatCommitAsyncThrows))]
        [Trait("Unit/UseCase", "Account - RefreshToken")]
        public async void ShouldRethrowSameExceptionThatCommitAsyncThrows()
        {
            var username = _fixture.Faker.Internet.UserName();
            _tokenServiceMock
                .Setup(x => x.GetUsernameFromTokenAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(username);

            var account = _fixture.MakeAccountEntity();
            _accountRepositoryMock
                .Setup(x => x.FindByUsernameAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(account);

            var accessToken = _fixture.MakeAccountToken();
            _tokenServiceMock
                .Setup(x => x.GenerateAccessTokenAsync(
                    It.IsAny<AccountEntity>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(accessToken);

            var refreshToken = _fixture.MakeAccountToken();
            _tokenServiceMock
                .Setup(x => x.GenerateRefreshTokenAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(refreshToken);

            _unitOfWorkMock
                .Setup(x => x.CommitAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(new UnexpectedException());

            var request = _fixture.MakeRefreshTokenRequest(refreshToken: account.RefreshToken?.Value);
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }

        [Fact(DisplayName = nameof(ShouldReturnTheCorrectResponseIfTokensAreUpdatedSuccessfully))]
        [Trait("Unit/UseCase", "Account - RefreshToken")]
        public async void ShouldReturnTheCorrectResponseIfTokensAreUpdatedSuccessfully()
        {
            var username = _fixture.Faker.Internet.UserName();
            _tokenServiceMock
                .Setup(x => x.GetUsernameFromTokenAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(username);

            var account = _fixture.MakeAccountEntity();
            _accountRepositoryMock
                .Setup(x => x.FindByUsernameAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(account);

            var accessToken = _fixture.MakeAccountToken();
            _tokenServiceMock
                .Setup(x => x.GenerateAccessTokenAsync(
                    It.IsAny<AccountEntity>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(accessToken);

            var refreshToken = _fixture.MakeAccountToken();
            _tokenServiceMock
                .Setup(x => x.GenerateRefreshTokenAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(refreshToken);

            var request = _fixture.MakeRefreshTokenRequest(refreshToken: account.RefreshToken?.Value);
            var response = await _sut.Handle(request, _fixture.CancellationToken);

            response.AccessToken.Should().NotBeNull();
            response.AccessToken?.Value.Should().Be(accessToken.Value);
            response.AccessToken?.ExpiresIn.Should().Be(accessToken.ExpiresIn);
            response.AccountId.Should().Be(account.Id);
            response.Active.Should().Be(account.Active);
            response.CreatdAt.Should().Be(account.CreatedAt);
            response.Email.Should().Be(account.Email);
            response.EmailConfirmed.Should().Be(account.EmailConfirmed);
            response.Phone.Should().Be(account.Phone);
            response.PhoneConfirmed.Should().Be(account.PhoneConfirmed);
            response.RefreshToken.Should().NotBeNull();
            response.RefreshToken?.Value.Should().Be(refreshToken.Value);
            response.RefreshToken?.ExpiresIn.Should().Be(refreshToken.ExpiresIn);
            response.Role.Should().Be(account.Role);
            response.Username.Should().Be(account.Username);
        }
    }
}