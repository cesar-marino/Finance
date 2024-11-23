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
    }
}