using Finance.Application.UseCases.Account.RevokeAccess;
using Finance.Domain.Exceptions;
using Finance.Infrastructure.Database.Repositories;
using FluentAssertions;

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
    }
}