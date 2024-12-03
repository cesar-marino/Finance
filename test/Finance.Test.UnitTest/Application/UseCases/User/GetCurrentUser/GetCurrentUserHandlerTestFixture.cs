using Finance.Application.UseCases.User.GetCurrentUser;
using Finance.Test.UnitTest.Commons;

namespace Finance.Test.UnitTest.Application.UseCases.User.GetCurrentUser
{
    public class GetCurrentUserHandlerTestFixture : FixtureBase
    {
        public GetCurrentUserRequest MakeGetCurrentUserRequest() => new(accessToken: Faker.Random.Guid().ToString());
    }
}