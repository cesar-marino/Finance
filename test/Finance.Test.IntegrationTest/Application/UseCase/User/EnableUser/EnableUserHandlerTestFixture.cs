using Finance.Application.UseCases.User.EnableUser;
using Finance.Test.IntegrationTest.Commons;

namespace Finance.Test.IntegrationTest.Application.UseCase.User.EnableUser
{
    public class EnableUserHandlerTestFixture : FixtureBase
    {
        public EnableUserRequest MakeEnableUserRequest(Guid? userId = null) => new(userId: userId ?? Faker.Random.Guid());
    }
}