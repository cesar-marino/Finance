using Finance.Application.UseCases.Account.EnableAccount;
using Finance.Test.UnitTest.Commons;

namespace Finance.Test.UnitTest.Application.UseCases.Account.EnableAccount
{
    public class EnableAccountHandlerTestFixture : FixtureBase
    {
        public EnableAccountRequest MakeEnableAccountRequest() => new(accountId: Faker.Random.Guid());
    }
}