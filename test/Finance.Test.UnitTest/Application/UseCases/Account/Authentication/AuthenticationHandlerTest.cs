using Finance.Application.Services;
using Finance.Application.UseCases.Account.Authentication;
using Finance.Domain.Entities;
using Finance.Domain.Exceptions;
using Finance.Domain.Repositories;
using FluentAssertions;
using Moq;

namespace Finance.Test.UnitTest.Application.UseCases.Account.Authentication
{
    public class AuthenticationHandlerTest : IClassFixture<AuthenticationHandlerTestFixture>
    {
        private readonly AuthenticationHandlerTestFixture _fixture;
        private readonly AuthenticationHandler _sut;
        private readonly Mock<IAccountRepository> _accountRepositoryMock;
        private readonly Mock<IEncryptionService> _encryptionServiceMock;
        private readonly Mock<ITokenService> _tokenServiceMock;

        public AuthenticationHandlerTest(AuthenticationHandlerTestFixture fixture)
        {
            _fixture = fixture;
            _accountRepositoryMock = new();
            _encryptionServiceMock = new();
            _tokenServiceMock = new();

            _sut = new(
                accountRepository: _accountRepositoryMock.Object,
                encryptionService: _encryptionServiceMock.Object,
                tokenService: _tokenServiceMock.Object);
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatFindByEmailAsyncThrows))]
        [Trait("Unit/UseCase", "Account - Authentication")]
        public async Task ShouldRethrowSameExceptionThatFindByEmailAsyncThrows()
        {
            _accountRepositoryMock
                .Setup(x => x.FindByEmailAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new NotFoundException("Account"));

            var request = _fixture.MakeAuthenticationRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<NotFoundException>()
                .Where(x => x.Code == "not-found")
                .WithMessage("Account not found");
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatVerifyAsyncThrows))]
        [Trait("Unit/UseCase", "Account - Authentication")]
        public async Task ShouldRethrowSameExceptionThatVerifyAsyncThrows()
        {
            var account = _fixture.MakeAccountEntity();
            _accountRepositoryMock
                .Setup(x => x.FindByEmailAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(account);

            _encryptionServiceMock
               .Setup(x => x.VerifyAsync(
                   It.IsAny<string>(),
                   It.IsAny<string>(),
                   It.IsAny<CancellationToken>()))
               .ThrowsAsync(new UnexpectedException());

            var request = _fixture.MakeAuthenticationRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }

        [Fact(DisplayName = nameof(ShouldThrowInvalidPasswordExceptionIfVerifyAsyncReturnsFalse))]
        [Trait("Unit/UseCase", "Account - Authentication")]
        public async Task ShouldThrowInvalidPasswordExceptionIfVerifyAsyncReturnsFalse()
        {
            var account = _fixture.MakeAccountEntity();
            _accountRepositoryMock
                .Setup(x => x.FindByEmailAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(account);

            _encryptionServiceMock
               .Setup(x => x.VerifyAsync(
                   It.IsAny<string>(),
                   It.IsAny<string>(),
                   It.IsAny<CancellationToken>()))
               .ReturnsAsync(false);

            var request = _fixture.MakeAuthenticationRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<InvalidPasswordException>()
                .Where(x => x.Code == "invalid-password")
                .WithMessage("Incorrect password");
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatGenerateAccessTokenAsyncThrows))]
        [Trait("Unit/UseCase", "Account - Authentication")]
        public async Task ShouldRethrowSameExceptionThatGenerateAccessTokenAsyncThrows()
        {
            var account = _fixture.MakeAccountEntity();
            _accountRepositoryMock
                .Setup(x => x.FindByEmailAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(account);

            _encryptionServiceMock
               .Setup(x => x.VerifyAsync(
                   It.IsAny<string>(),
                   It.IsAny<string>(),
                   It.IsAny<CancellationToken>()))
               .ReturnsAsync(true);

            _tokenServiceMock
                .Setup(x => x.GenerateAccessTokenAsync(
                    It.IsAny<AccountEntity>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new UnexpectedException());

            var request = _fixture.MakeAuthenticationRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatGenerateRefreshTokenAsyncThrows))]
        [Trait("Unit/UseCase", "Account - Authentication")]
        public async Task ShouldRethrowSameExceptionThatGenerateRefreshTokenAsyncThrows()
        {
            var account = _fixture.MakeAccountEntity();
            _accountRepositoryMock
                .Setup(x => x.FindByEmailAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(account);

            _encryptionServiceMock
               .Setup(x => x.VerifyAsync(
                   It.IsAny<string>(),
                   It.IsAny<string>(),
                   It.IsAny<CancellationToken>()))
               .ReturnsAsync(true);

            var accessToken = _fixture.MakeAccountToken();
            _tokenServiceMock
                .Setup(x => x.GenerateAccessTokenAsync(
                    It.IsAny<AccountEntity>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(accessToken);

            _tokenServiceMock
                .Setup(x => x.GenerateRefreshTokenAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(new UnexpectedException());

            var request = _fixture.MakeAuthenticationRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }
    }
}