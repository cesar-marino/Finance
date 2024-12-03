using Finance.Application.UseCases.User.DisableUser;
using Finance.Test.UnitTest.Commons;

namespace Finance.Test.UnitTest.Application.UseCases.User.DisableUser
{
    public class DisableUserHandlerTestFixture : FixtureBase
    {
        public DisableUserRequest MakeDisableUserRequest() => new(userId: Faker.Random.Guid());
    }
}