using Finance.Application.UseCases.User.EnableUser;
using Finance.Test.UnitTest.Commons;

namespace Finance.Test.UnitTest.Application.UseCases.User.EnableUser
{
    public class EnableUserHandlerTestFixture : FixtureBase
    {
        public EnableUserRequest MakeEnableUserRequest() => new(userId: Faker.Random.Guid());
    }
}