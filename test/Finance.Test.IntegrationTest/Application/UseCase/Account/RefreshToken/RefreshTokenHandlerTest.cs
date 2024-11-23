using Finance.Application.UseCases.Account.RefreshToken;
using Finance.Domain.Exceptions;
using Finance.Infrastructure.Database.Repositories;
using Finance.Infrastructure.Services.Token;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace Finance.Test.IntegrationTest.Application.UseCase.Account.RefreshToken
{
    public class RefreshTokenHandlerTest(RefreshTokenHandlerTestFixture fixture) : IClassFixture<RefreshTokenHandlerTestFixture>
    {
        private readonly RefreshTokenHandlerTestFixture _fixture = fixture;

        [Fact(DisplayName = nameof(ShouldThrowInvalidTokenException))]
        [Trait("Integration/UseCase", "Account - RefreshToken")]
        public async Task ShouldThrowInvalidTokenException()
        {
            var tokenService = new JwtBearerAdapter(_fixture.MakeConfiguration());
            var context = _fixture.MakeFinanceContext();
            var repository = new AccountRepository(context);

            var sut = new RefreshTokenHandler(
                tokenService: tokenService,
                accountRepository: repository,
                unitOfWork: context);

            var request = _fixture.MakeRefreshTokenRequest();
            var act = () => sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<InvalidTokenException>()
                .Where(x => x.Code == "invalid-token")
                .WithMessage("Token is invalid");
        }

        [Fact(DisplayName = nameof(ShouldThrowNotFoundException))]
        [Trait("Integration/UseCase", "Account - RefreshToken")]
        public async Task ShouldThrowNotFoundException()
        {
            var tokenService = new JwtBearerAdapter(_fixture.MakeConfiguration());
            var account = _fixture.MakeAccountModel();
            var token = await tokenService.GenerateAccessTokenAsync(account.ToEntity());

            var context = _fixture.MakeFinanceContext();
            var repository = new AccountRepository(context);

            var sut = new RefreshTokenHandler(
                tokenService: tokenService,
                accountRepository: repository,
                unitOfWork: context);

            var request = _fixture.MakeRefreshTokenRequest(accessToken: token.Value);
            var act = () => sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<NotFoundException>()
                .Where(x => x.Code == "not-found")
                .WithMessage("Account not found");
        }

        [Fact(DisplayName = nameof(ShouldThrowUnauthorizedException))]
        [Trait("Integration/UseCase", "Account - RefreshToken")]
        public async Task ShouldThrowUnauthorizedException()
        {
            var tokenService = new JwtBearerAdapter(_fixture.MakeConfiguration());
            var account = _fixture.MakeAccountModel();
            var token = await tokenService.GenerateAccessTokenAsync(account.ToEntity());
            account.AccessTokenValue = token.Value;
            account.AccessTokenExpiresIn = token.ExpiresIn;

            var context = _fixture.MakeFinanceContext();
            var repository = new AccountRepository(context);

            var trackingInfo = await context.Accounts.AddAsync(account);
            await context.SaveChangesAsync();
            trackingInfo.State = EntityState.Detached;

            var sut = new RefreshTokenHandler(
                tokenService: tokenService,
                accountRepository: repository,
                unitOfWork: context);

            var request = _fixture.MakeRefreshTokenRequest(accessToken: token.Value);
            var act = () => sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnauthorizedException>()
                .Where(x => x.Code == "unauthorized")
                .WithMessage("Unauthorized access");
        }

        [Fact(DisplayName = nameof(ShouldThrowUnexpectedException))]
        [Trait("Integration/UseCase", "Account - RefreshToken")]
        public async Task ShouldThrowUnexpectedException()
        {
            var tokenService = new JwtBearerAdapter(_fixture.MakeConfiguration());
            var account = _fixture.MakeAccountModel();
            var token = await tokenService.GenerateAccessTokenAsync(account.ToEntity());
            account.AccessTokenValue = token.Value;
            account.AccessTokenExpiresIn = token.ExpiresIn;

            var context = _fixture.MakeFinanceContext();
            var repository = new AccountRepository(context);

            var trackingInfo = await context.Accounts.AddAsync(account);
            await context.SaveChangesAsync();
            trackingInfo.State = EntityState.Detached;

            var sut = new RefreshTokenHandler(
                tokenService: tokenService,
                accountRepository: repository,
                unitOfWork: context);

            await context.DisposeAsync();

            var request = _fixture.MakeRefreshTokenRequest(accessToken: token.Value);
            var act = () => sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }

        [Fact(DisplayName = nameof(ShouldReturnTheCorrectResponseIfTokensAreSuccessfullyRefreshed))]
        [Trait("Integration/UseCase", "Account - RefreshToken")]
        public async Task ShouldReturnTheCorrectResponseIfTokensAreSuccessfullyRefreshed()
        {
            var tokenService = new JwtBearerAdapter(_fixture.MakeConfiguration());
            var account = _fixture.MakeAccountModel();
            var token = await tokenService.GenerateAccessTokenAsync(account.ToEntity());
            account.AccessTokenValue = token.Value;
            account.AccessTokenExpiresIn = token.ExpiresIn;

            var context = _fixture.MakeFinanceContext();
            var repository = new AccountRepository(context);

            var trackingInfo = await context.Accounts.AddAsync(account);
            await context.SaveChangesAsync();
            trackingInfo.State = EntityState.Detached;

            var sut = new RefreshTokenHandler(
                tokenService: tokenService,
                accountRepository: repository,
                unitOfWork: context);

            var request = _fixture.MakeRefreshTokenRequest(accessToken: token.Value, refreshToken: account.RefreshTokenValue);
            var response = await sut.Handle(request, _fixture.CancellationToken);

            var accountDb = await context.Accounts.FirstOrDefaultAsync(x => x.AccountId == response.AccountId);
            accountDb.Should().NotBeNull();
            accountDb?.AccessTokenExpiresIn.Should().Be(response.AccessToken?.ExpiresIn);
            accountDb?.AccessTokenValue.Should().Be(response.AccessToken?.Value);
            accountDb?.AccountId.Should().Be(response.AccountId);
            accountDb?.Active.Should().Be(response.Active);
            accountDb?.CreatedAt.Should().Be(response.CreatdAt);
            accountDb?.Email.Should().Be(response.Email);
            accountDb?.EmailConfirmed.Should().Be(response.EmailConfirmed);
            accountDb?.Phone.Should().Be(response.Phone);
            accountDb?.PhoneConfirmed.Should().Be(response.PhoneConfirmed);
            accountDb?.RefreshTokenExpiresIn.Should().Be(response.RefreshToken?.ExpiresIn);
            accountDb?.RefreshTokenValue.Should().Be(response.RefreshToken?.Value);
            accountDb?.Role.Should().Be(response.Role.ToString());
            accountDb?.UpdatedAt.Should().Be(response.UpdatedAt);
            accountDb?.Username.Should().Be(response.Username);
        }
    }
}