using Finance.Application.UseCases.Account.DisableAccount;
using Finance.Test.UnitTest.Commons;

namespace Finance.Test.UnitTest.Application.UseCases.Account.DisableAccount
{
    public class DisableAccountHandlerTestFixture : FixtureBase
    {
        public DisableAccountRequest MakeDisableAccountRequest() => new(accountId: Faker.Random.Guid());
    }
}