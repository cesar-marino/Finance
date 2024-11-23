using Finance.Application.UseCases.Account.UpdateEmail;
using Finance.Domain.Exceptions;
using Finance.Infrastructure.Database.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace Finance.Test.IntegrationTest.Application.UseCase.Account.UpdateEmail
{
    public class UpdateEmailHandlerTest(UpdateEmailHandlerTestFixture fixture) : IClassFixture<UpdateEmailHandlerTestFixture>
    {
        private readonly UpdateEmailHandlerTestFixture _fixture = fixture;

        [Fact(DisplayName = nameof(ShouldThrowEmailInUseException))]
        [Trait("INtegration/UseCase", "Account - UpdateEmail")]
        public async Task ShouldThrowEmailInUseException()
        {
            var context = _fixture.MakeFinanceContext();
            var repository = new AccountRepository(context);

            var account = _fixture.MakeAccountModel();
            var trackingInfo = await context.Accounts.AddAsync(account);
            await context.SaveChangesAsync();
            trackingInfo.State = EntityState.Detached;

            var sut = new UpdateEmailHandler(accountRepository: repository, unitOfWork: context);

            var request = _fixture.MakeUpdateEmailRequest(email: account.Email);
            var act = () => sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<EmailInUseException>()
                .Where(x => x.Code == "email-in-use")
                .WithMessage("Email is already in use");
        }

        [Fact(DisplayName = nameof(ShouldThrowNotFoundException))]
        [Trait("INtegration/UseCase", "Account - UpdateEmail")]
        public async Task ShouldThrowNotFoundException()
        {
            var context = _fixture.MakeFinanceContext();
            var repository = new AccountRepository(context);

            var account = _fixture.MakeAccountModel();
            var trackingInfo = await context.Accounts.AddAsync(account);
            await context.SaveChangesAsync();
            trackingInfo.State = EntityState.Detached;

            var sut = new UpdateEmailHandler(accountRepository: repository, unitOfWork: context);

            var request = _fixture.MakeUpdateEmailRequest();
            var act = () => sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<NotFoundException>()
                .Where(x => x.Code == "not-found")
                .WithMessage("Account not found");
        }

        [Fact(DisplayName = nameof(ShouldThrowUnexpectedException))]
        [Trait("INtegration/UseCase", "Account - UpdateEmail")]
        public async Task ShouldThrowUnexpectedException()
        {
            var context = _fixture.MakeFinanceContext();
            var repository = new AccountRepository(context);

            var sut = new UpdateEmailHandler(accountRepository: repository, unitOfWork: context);

            await context.DisposeAsync();

            var request = _fixture.MakeUpdateEmailRequest();
            var act = () => sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }

        [Fact(DisplayName = nameof(ShouldReturnTheCorrectResponseIfEmailIsSuccessfullyUpdated))]
        [Trait("INtegration/UseCase", "Account - UpdateEmail")]
        public async Task ShouldReturnTheCorrectResponseIfEmailIsSuccessfullyUpdated()
        {
            var context = _fixture.MakeFinanceContext();
            var repository = new AccountRepository(context);

            var account = _fixture.MakeAccountModel();
            var trackingInfo = await context.Accounts.AddAsync(account);
            await context.SaveChangesAsync();
            trackingInfo.State = EntityState.Detached;

            var sut = new UpdateEmailHandler(accountRepository: repository, unitOfWork: context);

            var request = _fixture.MakeUpdateEmailRequest(accountId: account.AccountId);
            var response = await sut.Handle(request, _fixture.CancellationToken);

            var accountDb = await context.Accounts.FirstOrDefaultAsync(x => x.AccountId == response.AccountId);

            accountDb.Should().NotBeNull();
            accountDb?.AccessTokenExpiresIn.Should().Be(response.AccessToken?.ExpiresIn);
            accountDb?.AccessTokenValue.Should().Be(response.AccessToken?.Value);
            accountDb?.AccountId.Should().Be(response.AccountId);
            accountDb?.Active.Should().Be(response.Active);
            accountDb?.Active.Should().Be(response.Active);
            accountDb?.CreatedAt.Should().Be(response.CreatdAt);
            accountDb?.Email.Should().Be(response.Email);
            accountDb?.Email.Should().Be(request.Email);
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