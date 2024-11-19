using Finance.Application.UseCases.Account.Authentication;
using Finance.Domain.Exceptions;
using Finance.Infrastructure.Database.Repositories;
using Finance.Infrastructure.Services.Encryption;
using Finance.Infrastructure.Services.Token;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Finance.Test.IntegrationTest.Application.UseCase.Account.Authentication
{
    public class AuthenticationHandlerTest : IClassFixture<AuthenticationHandlerTestFixture>
    {
        private readonly AuthenticationHandlerTestFixture _fixture;
        private readonly IConfiguration _configuration;

        public AuthenticationHandlerTest(AuthenticationHandlerTestFixture fixture)
        {
            _fixture = fixture;
            _configuration = _fixture.MakeConfiguration();
        }

        [Fact(DisplayName = nameof(ShouldThrowNotFoundException))]
        [Trait("Integration/UseCase", "Account - Authentication")]
        public async Task ShouldThrowNotFoundException()
        {
            var encryptionService = new EncryptionService();
            var tokenService = new JwtBearerAdapter(configuration: _configuration);
            var context = _fixture.MakeFinanceContext();
            var repository = new AccountRepository(context);

            var sut = new AuthenticationHandler(
                accountRepository: repository,
                encryptionService: encryptionService,
                tokenService: tokenService,
                unitOfWork: context);

            var request = _fixture.MakeAuthenticationRequest();
            var act = () => sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<NotFoundException>()
                .Where(x => x.Code == "not-found")
                .WithMessage("Account not found");
        }

        [Fact(DisplayName = nameof(ShouldThrowInvalidPasswordException))]
        [Trait("Integration/UseCase", "Account - Authentication")]
        public async Task ShouldThrowInvalidPasswordException()
        {
            var account = _fixture.MakeAccountModel();
            var tokenService = new JwtBearerAdapter(configuration: _configuration);
            var encryptionService = new EncryptionService();

            var accessToken = await tokenService.GenerateAccessTokenAsync(account.ToEntity(), _fixture.CancellationToken);
            var refreshToken = await tokenService.GenerateRefreshTokenAsync(_fixture.CancellationToken);

            account.AccessTokenValue = accessToken.Value;
            account.AccessTokenExpiresIn = accessToken.ExpiresIn;
            account.RefreshTokenValue = refreshToken.Value;
            account.RefreshTokenExpiresIn = refreshToken.ExpiresIn;
            account.Passwrd = await encryptionService.EcnryptAsync(account.Passwrd);

            var context = _fixture.MakeFinanceContext();
            var trackingInfo = await context.Accounts.AddAsync(account);
            await context.SaveChangesAsync();
            trackingInfo.State = EntityState.Detached;

            var repository = new AccountRepository(context);

            var sut = new AuthenticationHandler(
                accountRepository: repository,
                encryptionService: encryptionService,
                tokenService: tokenService,
                unitOfWork: context);

            var request = _fixture.MakeAuthenticationRequest(email: account.Email);
            var act = () => sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<InvalidPasswordException>()
                .Where(x => x.Code == "invalid-password")
                .WithMessage("Incorrect password");
        }

        [Fact(DisplayName = nameof(ShouldThrowDisableAccountException))]
        [Trait("Integration/UseCase", "Account - Authentication")]
        public async Task ShouldThrowDisableAccountException()
        {
            var password = _fixture.Faker.Internet.Password();
            var account = _fixture.MakeAccountModel(active: false, password: password);
            var tokenService = new JwtBearerAdapter(configuration: _configuration);
            var encryptionService = new EncryptionService();

            var accessToken = await tokenService.GenerateAccessTokenAsync(account.ToEntity(), _fixture.CancellationToken);
            var refreshToken = await tokenService.GenerateRefreshTokenAsync(_fixture.CancellationToken);

            account.AccessTokenValue = accessToken.Value;
            account.AccessTokenExpiresIn = accessToken.ExpiresIn;
            account.RefreshTokenValue = refreshToken.Value;
            account.RefreshTokenExpiresIn = refreshToken.ExpiresIn;
            account.Passwrd = await encryptionService.EcnryptAsync(account.Passwrd);

            var context = _fixture.MakeFinanceContext();
            var trackingInfo = await context.Accounts.AddAsync(account);
            await context.SaveChangesAsync();
            trackingInfo.State = EntityState.Detached;

            var repository = new AccountRepository(context);

            var sut = new AuthenticationHandler(
                accountRepository: repository,
                encryptionService: encryptionService,
                tokenService: tokenService,
                unitOfWork: context);

            var request = _fixture.MakeAuthenticationRequest(email: account.Email, password: password);
            var act = () => sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<DisableAccountException>()
                .Where(x => x.Code == "disable-account")
                .WithMessage("Disable account");
        }
    }
}