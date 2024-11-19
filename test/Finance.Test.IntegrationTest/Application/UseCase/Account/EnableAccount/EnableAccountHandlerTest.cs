using Finance.Application.UseCases.Account.EnableAccount;
using Finance.Domain.Exceptions;
using Finance.Infrastructure.Database.Repositories;
using FluentAssertions;

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
    }
}