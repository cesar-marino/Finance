using Finance.Application.UseCases.User.RevokeAccess;
using Finance.Test.UnitTest.Commons;

namespace Finance.Test.UnitTest.Application.UseCases.User.RevokeAccess
{
    public class RevokeAccessHandlerTestFixture : FixtureBase
    {
        public RevokeAccessRequest MakeRevokeAccessRequest() => new(userId: Faker.Random.Guid());
    }
}