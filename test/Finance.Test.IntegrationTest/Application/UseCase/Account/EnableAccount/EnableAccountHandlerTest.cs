using Finance.Application.UseCases.Account.EnableAccount;
using Finance.Domain.Exceptions;
using Finance.Infrastructure.Database.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Finance.Test.IntegrationTest.Application.UseCase.Account.EnableAccount
{
    public class EnableAccountHandlerTest(EnableAccountHandlerTestFixture fixture) : IClassFixture<EnableAccountHandlerTestFixture>
    {
        private readonly EnableAccountHandlerTestFixture _fixture = fixture;

        [Fact(DisplayName = nameof(ShouldThrowNotFoundException))]
        [Trait("Integration/UseCase", "Account - EnableAccount")]
        public async Task ShouldThrowNotFoundException()
        {
            var context = _fixture.MakeFinanceContext();
            var repository = new AccountRepository(context);

            var sut = new EnableAccountHandler(accountRepository: repository, unitOfWork: context);

            var request = _fixture.MakeEnableAccountRequest();
            var act = () => sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<NotFoundException>()
                .Where(x => x.Code == "not-found")
                .WithMessage("Account not found");
        }

        [Fact(DisplayName = nameof(ShouldThrowUnexpectedException))]
        [Trait("Integration/UseCase", "Account - EnableAccount")]
        public async Task ShouldThrowUnexpectedException()
        {
            var account = _fixture.MakeAccountModel(active: false);
            var context = _fixture.MakeFinanceContext();

            var trackingInfo = await context.Accounts.AddAsync(account);
            await context.SaveChangesAsync();
            trackingInfo.State = EntityState.Detached;

            var repository = new AccountRepository(context);

            var sut = new EnableAccountHandler(accountRepository: repository, unitOfWork: context);

            await context.DisposeAsync();

            var request = _fixture.MakeEnableAccountRequest();
            var act = () => sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }
    }
}