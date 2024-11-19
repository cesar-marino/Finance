using Finance.Application.UseCases.Account.DisableAccount;
using Finance.Test.IntegrationTest.Commons;

namespace Finance.Test.IntegrationTest.Application.UseCase.Account.DisableAccount
{
    public class DisableAccountHandlerTestFixture : FixtureBase
    {
        public DisableAccountRequest MakeDisableAccountRequest(Guid? accountId = null) => new(accountId: accountId ?? Faker.Random.Guid());
    }
}