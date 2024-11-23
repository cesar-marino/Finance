using Finance.Application.UseCases.Account.RevokeAccess;
using Finance.Domain.Exceptions;
using Finance.Infrastructure.Database.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace Finance.Test.IntegrationTest.Application.UseCase.Account.RevokeAccess
{
    public class RevokeAccessHandlerTest(RevokeAccessHandlerTestFixture fixture) : IClassFixture<RevokeAccessHandlerTestFixture>
    {
        private readonly RevokeAccessHandlerTestFixture _fixture = fixture;

        [Fact(DisplayName = nameof(ShouldThrowNotFoundException))]
        [Trait("Integration/UseCase", "Account - RevokeAccess")]
        public async Task ShouldThrowNotFoundException()
        {
            var context = _fixture.MakeFinanceContext();
            var repository = new AccountRepository(context);

            var sut = new RevokeAccessHandler(accountRepository: repository, unitOfWork: context);

            var request = _fixture.MakeRevokeAccessRequest();
            var act = () => sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<NotFoundException>()
                .Where(x => x.Code == "not-found")
                .WithMessage("Account not found");
        }

        [Fact(DisplayName = nameof(ShouldThrowUnexpectedException))]
        [Trait("Integration/UseCase", "Account - RevokeAccess")]
        public async Task ShouldThrowUnexpectedException()
        {
            var account = _fixture.MakeAccountModel();
            var context = _fixture.MakeFinanceContext();
            var repository = new AccountRepository(context);

            var trackingInfo = await context.Accounts.AddAsync(account);
            await context.SaveChangesAsync();
            trackingInfo.State = EntityState.Detached;

            var sut = new RevokeAccessHandler(accountRepository: repository, unitOfWork: context);

            await context.DisposeAsync();

            var request = _fixture.MakeRevokeAccessRequest();
            var act = () => sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }

        [Fact(DisplayName = nameof(ShouldReturnTheCorrectResponseIfAccessIsSuccessfullyRevoked))]
        [Trait("Integration/UseCase", "Account - RevokeAccess")]
        public async Task ShouldReturnTheCorrectResponseIfAccessIsSuccessfullyRevoked()
        {
            var account = _fixture.MakeAccountModel();
            var context = _fixture.MakeFinanceContext();
            var repository = new AccountRepository(context);

            var trackingInfo = await context.Accounts.AddAsync(account);
            await context.SaveChangesAsync();
            trackingInfo.State = EntityState.Detached;

            var sut = new RevokeAccessHandler(accountRepository: repository, unitOfWork: context);
            var request = _fixture.MakeRevokeAccessRequest(accountId: account.AccountId);
            var response = await sut.Handle(request, _fixture.CancellationToken);

            var accountDb = await context.Accounts.FirstOrDefaultAsync(x => x.AccountId == response.AccountId);
            accountDb.Should().NotBeNull();
            accountDb?.AccessTokenExpiresIn.Should().Be(response.AccessToken?.ExpiresIn);
            accountDb?.AccessTokenExpiresIn.Should().BeNull();
            accountDb?.AccessTokenValue.Should().Be(response.AccessToken?.Value);
            accountDb?.AccessTokenValue.Should().BeNull();
            accountDb?.AccountId.Should().Be(response.AccountId);
            accountDb?.Active.Should().Be(response.Active);
            accountDb?.CreatedAt.Should().Be(response.CreatdAt);
            accountDb?.Email.Should().Be(response.Email);
            accountDb?.EmailConfirmed.Should().Be(response.EmailConfirmed);
            accountDb?.Phone.Should().Be(response.Phone);
            accountDb?.PhoneConfirmed.Should().Be(response.PhoneConfirmed);
            accountDb?.RefreshTokenExpiresIn.Should().Be(response.RefreshToken?.ExpiresIn);
            accountDb?.RefreshTokenExpiresIn.Should().BeNull();
            accountDb?.RefreshTokenValue.Should().Be(response.RefreshToken?.Value);
            accountDb?.RefreshTokenValue.Should().BeNull();
            accountDb?.Role.Should().Be(response.Role.ToString());
            accountDb?.UpdatedAt.Should().Be(response.UpdatedAt);
            accountDb?.Username.Should().Be(response.Username);
        }
    }
}