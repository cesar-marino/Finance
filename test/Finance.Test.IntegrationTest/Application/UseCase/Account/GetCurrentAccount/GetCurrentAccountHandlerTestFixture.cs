using Finance.Application.UseCases.Account.GetCurrentAccount;
using Finance.Test.IntegrationTest.Commons;

namespace Finance.Test.IntegrationTest.Application.UseCase.Account.GetCurrentAccount
{
    public class GetCurrentAccountHandlerTestFixture : FixtureBase
    {
        public GetCurrentAccountRequest MakeGetCurrentAccountRequest(string? tokenValue = null) => new(accessToken: tokenValue ?? Faker.Random.Guid().ToString());
    }
}