using Finance.Application.UseCases.Account.EnableAccount;
using Finance.Test.IntegrationTest.Commons;

namespace Finance.Test.IntegrationTest.Application.UseCase.Account.EnableAccount
{
    public class EnableAccountHandlerTestFixture : FixtureBase
    {
        public EnableAccountRequest MakeEnableAccountRequest(Guid? accountId = null) => new(accountId: accountId ?? Faker.Random.Guid());
    }
}