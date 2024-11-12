using Finance.Application.Services;
using Finance.Application.UseCases.Account.CreateAccount;
using Finance.Domain.Entities;
using Finance.Domain.Enums;
using Finance.Domain.Exceptions;
using Finance.Domain.Repositories;
using Finance.Domain.SeedWork;
using FluentAssertions;
using Moq;

namespace Finance.Test.UnitTest.Application.UseCases.Account.CreateAccount
{
    public class CreateAccountHandlerTest : IClassFixture<CreateAccountHandlerTestFixture>
    {
        private readonly CreateAccountHandlerTestFixture _fixture;
        private readonly CreateAccountHandler _sut;
        private readonly Mock<IAccountRepository> _accountRepositoryMock;
        private readonly Mock<IEncryptionService> _encryptionServiceMock;
        private readonly Mock<ITokenService> _tokenServiceMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public CreateAccountHandlerTest(CreateAccountHandlerTestFixture fixture)
        {
            _fixture = fixture;
            _accountRepositoryMock = new();
            _tokenServiceMock = new();
            _unitOfWorkMock = new();
            _encryptionServiceMock = new();

            _sut = new(
                accountRepository: _accountRepositoryMock.Object,
                encryptionService: _encryptionServiceMock.Object,
                tokenService: _tokenServiceMock.Object,
                unitOfWork: _unitOfWorkMock.Object);
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatCheckEmailAsyncThrows))]
        [Trait("Unit/UseCase", "Account - CreateAccount")]
        public async Task ShouldRethrowSameExceptionThatCheckEmailAsyncThrows()
        {
            _accountRepositoryMock
                .Setup(x => x.CheckEmailAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new UnexpectedException());

            var request = _fixture.MakeCreateAccountRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }

        [Fact(DisplayName = nameof(ShouldThrowEmailInUseExceptionIfCheckEmailAsyncReturnsTrue))]
        [Trait("Unit/UseCase", "Account - CreateAccount")]
        public async Task ShouldThrowEmailInUseExceptionIfCheckEmailAsyncReturnsTrue()
        {
            _accountRepositoryMock
                .Setup(x => x.CheckEmailAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            var request = _fixture.MakeCreateAccountRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<EmailInUseException>()
                .Where(x => x.Code == "email-in-use")
                .WithMessage("Email is already in use");
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatCheckUsernameAsyncThrows))]
        [Trait("Unit/UseCase", "Account - CreateAccount")]
        public async Task ShouldRethrowSameExceptionThatCheckUsernameAsyncThrows()
        {
            _accountRepositoryMock
                .Setup(x => x.CheckEmailAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            _accountRepositoryMock
                .Setup(x => x.CheckUsernameAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new UnexpectedException());

            var request = _fixture.MakeCreateAccountRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }

        [Fact(DisplayName = nameof(ShouldThrowUsernameInUseExceptionIfCheckEmailAsyncReturnsTrue))]
        [Trait("Unit/UseCase", "Account - CreateAccount")]
        public async Task ShouldThrowUsernameInUseExceptionIfCheckEmailAsyncReturnsTrue()
        {
            _accountRepositoryMock
                .Setup(x => x.CheckEmailAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            _accountRepositoryMock
                .Setup(x => x.CheckUsernameAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            _encryptionServiceMock
                .Setup(x => x.EcnryptAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new UnexpectedException());

            var request = _fixture.MakeCreateAccountRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UsernameInUseException>()
                .Where(x => x.Code == "username-in-use")
                .WithMessage("Username is already in use");
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatEncrypAsyncThrows))]
        [Trait("Unit/UseCase", "Account - CreateAccount")]
        public async Task ShouldRethrowSameExceptionThatEncrypAsyncThrows()
        {
            _accountRepositoryMock
                .Setup(x => x.CheckEmailAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            _accountRepositoryMock
                .Setup(x => x.CheckUsernameAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            _encryptionServiceMock
                .Setup(x => x.EcnryptAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new UnexpectedException());

            var request = _fixture.MakeCreateAccountRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatGenerateAccessTokenAsyncThrows))]
        [Trait("Unit/UseCase", "Account - CreateAccount")]
        public async Task ShouldRethrowSameExceptionThatGenerateAccessTokenAsyncThrows()
        {
            _accountRepositoryMock
                .Setup(x => x.CheckEmailAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            _accountRepositoryMock
                .Setup(x => x.CheckUsernameAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            var password = _fixture.Faker.Internet.Password();
            _encryptionServiceMock
                .Setup(x => x.EcnryptAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(password);

            _tokenServiceMock
                .Setup(x => x.GenerateAccessTokenAsync(
                    It.IsAny<AccountEntity>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new UnexpectedException());

            var request = _fixture.MakeCreateAccountRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatGenerateRefreshTokenAsyncThrows))]
        [Trait("Unit/UseCase", "Account - CreateAccount")]
        public async Task ShouldRethrowSameExceptionThatGenerateRefreshTokenAsyncThrows()
        {
            _accountRepositoryMock
                .Setup(x => x.CheckEmailAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            _accountRepositoryMock
                .Setup(x => x.CheckUsernameAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            var password = _fixture.Faker.Internet.Password();
            _encryptionServiceMock
                .Setup(x => x.EcnryptAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(password);

            var accessToken = _fixture.MakeAccountToken();
            _tokenServiceMock
                .Setup(x => x.GenerateAccessTokenAsync(
                    It.IsAny<AccountEntity>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(accessToken);

            _tokenServiceMock
                .Setup(x => x.GenerateRefreshTokenAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(new UnexpectedException());

            var request = _fixture.MakeCreateAccountRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatInsertAsyncThrows))]
        [Trait("Unit/UseCase", "Account - CreateAccount")]
        public async Task ShouldRethrowSameExceptionThatInsertAsyncThrows()
        {
            _accountRepositoryMock
                .Setup(x => x.CheckEmailAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            _accountRepositoryMock
                .Setup(x => x.CheckUsernameAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            var password = _fixture.Faker.Internet.Password();
            _encryptionServiceMock
                .Setup(x => x.EcnryptAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(password);

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
                .Setup(x => x.InsertAsync(
                    It.IsAny<AccountEntity>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new UnexpectedException());

            var request = _fixture.MakeCreateAccountRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatCommitAsyncThrows))]
        [Trait("Unit/UseCase", "Account - CreateAccount")]
        public async Task ShouldRethrowSameExceptionThatCommitAsyncThrows()
        {
            _accountRepositoryMock
                .Setup(x => x.CheckEmailAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            _accountRepositoryMock
                .Setup(x => x.CheckUsernameAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            var password = _fixture.Faker.Internet.Password();
            _encryptionServiceMock
                .Setup(x => x.EcnryptAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(password);

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

            var request = _fixture.MakeCreateAccountRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }

        [Fact(DisplayName = nameof(ShouldReturnTheCorrectResponseIfAccountIsCreatedSuccessfully))]
        [Trait("Unit/UseCase", "Account - CreateAccount")]
        public async Task ShouldReturnTheCorrectResponseIfAccountIsCreatedSuccessfully()
        {
            _accountRepositoryMock
                .Setup(x => x.CheckEmailAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            _accountRepositoryMock
                .Setup(x => x.CheckUsernameAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            var password = _fixture.Faker.Internet.Password();
            _encryptionServiceMock
                .Setup(x => x.EcnryptAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(password);

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

            var request = _fixture.MakeCreateAccountRequest();
            var response = await _sut.Handle(request, _fixture.CancellationToken);

            response.AccessToken.Should().NotBeNull();
            response.AccessToken?.Value.Should().Be(accessToken.Value);
            response.AccessToken?.ExpiresIn.Should().Be(accessToken.ExpiresIn);
            response.Active.Should().BeTrue();
            response.Email.Should().Be(request.Email);
            response.EmailConfirmed.Should().BeFalse();
            response.Phone.Should().Be(request.Phone);
            response.PhoneConfirmed.Should().BeFalse();
            response.RefreshToken.Should().NotBeNull();
            response.RefreshToken?.Value.Should().Be(refreshToken.Value);
            response.RefreshToken?.ExpiresIn.Should().Be(refreshToken.ExpiresIn);
            response.Role.Should().Be(Roles.User);
            response.Username.Should().Be(request.Username);
        }
    }
}