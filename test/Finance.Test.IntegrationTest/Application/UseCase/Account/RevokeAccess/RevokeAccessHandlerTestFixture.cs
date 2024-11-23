using Finance.Application.UseCases.Account.RevokeAccess;
using Finance.Test.IntegrationTest.Commons;

namespace Finance.Test.IntegrationTest.Application.UseCase.Account.RevokeAccess
{
    public class RevokeAccessHandlerTestFixture : FixtureBase
    {
        public RevokeAccessRequest MakeRevokeAccessRequest(Guid? accountId = null) => new(accountId: accountId ?? Faker.Random.Guid());
    }
}