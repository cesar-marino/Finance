using Finance.Application.UseCases.User.RevokeAccess;
using Finance.Test.IntegrationTest.Commons;

namespace Finance.Test.IntegrationTest.Application.UseCase.User.RevokeAccess
{
    public class RevokeAccessHandlerTestFixture : FixtureBase
    {
        public RevokeAccessRequest MakeRevokeAccessRequest(Guid? userId = null) => new(userId: userId ?? Faker.Random.Guid());
    }
}