using Finance.Application.UseCases.User.DisableUser;
using Finance.Test.IntegrationTest.Commons;

namespace Finance.Test.IntegrationTest.Application.UseCase.User.DisableUser
{
    public class DisableUserHandlerTestFixture : FixtureBase
    {
        public DisableUserRequest MakeDisableUserRequest(Guid? userId = null) => new(userId: userId ?? Faker.Random.Guid());
    }
}