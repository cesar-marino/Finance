using Finance.Application.UseCases.Account.GetCurrentAccount;
using Finance.Domain.Exceptions;
using Finance.Infrastructure.Database.Repositories;
using Finance.Infrastructure.Services.Token;
using FluentAssertions;

namespace Finance.Test.IntegrationTest.Application.UseCase.Account.GetCurrentAccount
{
    public class GetCurrentAccountHnadlerTest(GetCurrentAccountHandlerTestFixture fixture) : IClassFixture<GetCurrentAccountHandlerTestFixture>
    {
        private readonly GetCurrentAccountHandlerTestFixture _fixture = fixture;

        [Fact(DisplayName = nameof(ShouldThrowNotFoundException))]
        [Trait("Integration/UseCase", "Account - GetCurrentAccount")]
        public async Task ShouldThrowNotFoundException()
        {
            var account = _fixture.MakeAccountModel();
            var tokenService = new JwtBearerAdapter(_fixture.MakeConfiguration());
            var token = await tokenService.GenerateAccessTokenAsync(account.ToEntity());

            var context = _fixture.MakeFinanceContext();
            var repository = new AccountRepository(context);

            var sut = new GetCurrentAccountHandler(tokenService, repository);

            var request = _fixture.MakeGetCurrentAccountRequest(tokenValue: token.Value);
            var act = () => sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<NotFoundException>()
                .Where(x => x.Code == "not-found")
                .WithMessage("Account not found");
        }
    }
}