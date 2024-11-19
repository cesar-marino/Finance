using Finance.Application.UseCases.Account.DisableAccount;
using Finance.Domain.Exceptions;
using Finance.Infrastructure.Database.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace Finance.Test.IntegrationTest.Application.UseCase.Account.DisableAccount
{
    public class DisableAccountHandlerTest : IClassFixture<DisableAccountHandlerTestFixture>
    {
        private readonly DisableAccountHandlerTestFixture _fixture;

        public DisableAccountHandlerTest(DisableAccountHandlerTestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact(DisplayName = nameof(ShouldThrowNotFoundException))]
        [Trait("Integration/UseCase", "Account - DisableAccount")]
        public async Task ShouldThrowNotFoundException()
        {
            var context = _fixture.MakeFinanceContext();
            var repository = new AccountRepository(context);

            var sut = new DisableAccountHandler(accountRepository: repository, unitOfWork: context);

            var request = _fixture.MakeDisableAccountRequest();
            var act = () => sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<NotFoundException>()
                .Where(x => x.Code == "not-found")
                .WithMessage("Account not found");
        }

        [Fact(DisplayName = nameof(ShouldThrowUnexpectedException))]
        [Trait("Integration/UseCase", "Account - DisableAccount")]
        public async Task ShouldThrowUnexpectedException()
        {
            var account = _fixture.MakeAccountModel();
            var context = _fixture.MakeFinanceContext();

            var trackingInfo = await context.Accounts.AddAsync(account);
            await context.SaveChangesAsync();
            trackingInfo.State = EntityState.Detached;

            var repository = new AccountRepository(context);

            var sut = new DisableAccountHandler(accountRepository: repository, unitOfWork: context);

            await context.DisposeAsync();

            var request = _fixture.MakeDisableAccountRequest(accountId: account.AccountId);
            var act = () => sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }
    }
}