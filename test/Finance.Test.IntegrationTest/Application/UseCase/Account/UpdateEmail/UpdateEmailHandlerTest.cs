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
    }
}