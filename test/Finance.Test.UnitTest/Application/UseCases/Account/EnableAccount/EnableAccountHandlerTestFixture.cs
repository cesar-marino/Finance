using Finance.Application.UseCases.User.EnableAccount;
using Finance.Test.UnitTest.Commons;

namespace Finance.Test.UnitTest.Application.UseCases.User.EnableAccount
{
    public class EnableAccountHandlerTestFixture : FixtureBase
    {
        public EnableAccountRequest MakeEnableAccountRequest() => new(accountId: Faker.Random.Guid());
    }
}