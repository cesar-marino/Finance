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
    }
}