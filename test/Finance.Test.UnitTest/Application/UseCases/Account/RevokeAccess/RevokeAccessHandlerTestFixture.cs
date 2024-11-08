using Finance.Application.UseCases.Account.RevokeAccess;
using Finance.Test.UnitTest.Commons;

namespace Finance.Test.UnitTest.Application.UseCases.Account.RevokeAccess
{
    public class RevokeAccessHandlerTestFixture : FixtureBase
    {
        public RevokeAccessRequest MakeRevokeAccessRequest() => new(accountId: Faker.Random.Guid());
    }
}