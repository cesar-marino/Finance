using Finance.Application.UseCases.Account.GetCurrentAccount;
using Finance.Test.UnitTest.Commons;

namespace Finance.Test.UnitTest.Application.UseCases.Account.GetCurrentAccount
{
    public class GetCurrentAccountHandlerTestFixture : FixtureBase
    {
        public GetCurrentAccountRequest MakeGetCurrentAccountRequest() => new(accessToken: Faker.Random.Guid().ToString());
    }
}