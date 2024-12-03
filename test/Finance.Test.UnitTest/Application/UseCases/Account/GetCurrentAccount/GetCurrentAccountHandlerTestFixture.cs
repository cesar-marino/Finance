using Finance.Application.UseCases.User.GetCurrentUser;
using Finance.Test.UnitTest.Commons;

namespace Finance.Test.UnitTest.Application.UseCases.User.GetCurrentAccount
{
    public class GetCurrentAccountHandlerTestFixture : FixtureBase
    {
        public GetCurrentUserRequest MakeGetCurrentAccountRequest() => new(accessToken: Faker.Random.Guid().ToString());
    }
}