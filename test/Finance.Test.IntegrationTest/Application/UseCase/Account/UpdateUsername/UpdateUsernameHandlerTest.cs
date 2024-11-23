using Finance.Application.UseCases.Account.UpdateUsername;
using Finance.Domain.Exceptions;
using Finance.Infrastructure.Database.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace Finance.Test.IntegrationTest.Application.UseCase.Account.UpdateUsername
{
    public class UpdateUsernameHandlerTest(UpdateUsernameHandlerTestFixture fixture) : IClassFixture<UpdateUsernameHandlerTestFixture>
    {
        private readonly UpdateUsernameHandlerTestFixture _fixture = fixture;

        [Fact(DisplayName = nameof(ShouldThrowUsernameInUseException))]
        [Trait("Integration/UseCase", "Account - UpdateUsername")]
        public async Task ShouldThrowUsernameInUseException()
        {
            var context = _fixture.MakeFinanceContext();
            var repository = new AccountRepository(context);

            var account = _fixture.MakeAccountModel();
            var trackingInfo = await context.Accounts.AddAsync(account);
            await context.SaveChangesAsync();
            trackingInfo.State = EntityState.Detached;

            var sut = new UpdateUsernameHandler(accountRepository: repository, unitOfWork: context);

            var request = _fixture.MakeUpdateUsernameRequest(username: account.Username);
            var act = () => sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UsernameInUseException>()
                .Where(x => x.Code == "username-in-use")
                .WithMessage("Username is already in use");
        }
    }
}