using Finance.Application.UseCases.Account.RevokeAllAccess;
using Finance.Domain.Exceptions;
using Finance.Infrastructure.Database.Repositories;
using FluentAssertions;

namespace Finance.Test.IntegrationTest.Application.UseCase.Account.RevokeAllAccess
{
    public class RevokeAllAccessHandlerTest(RevokeAllAccessHandlerTestFixture fixture) : IClassFixture<RevokeAllAccessHandlerTestFixture>
    {
        private readonly RevokeAllAccessHandlerTestFixture _fixture = fixture;

        [Fact(DisplayName = nameof(ShouldThrowUnexpectedException))]
        [Trait("Integration/UseCase", "Account - RevokeAllAccess")]
        public async Task ShouldThrowUnexpectedException()
        {
            var context = _fixture.MakeFinanceContext();
            var repository = new AccountRepository(context);

            var sut = new RevokeAllAccessHandler(accountRepository: repository, unitOfWork: context);

            await context.DisposeAsync();

            var request = _fixture.MakeRevokeAllAccessRequest();
            var act = () => sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }
    }
}