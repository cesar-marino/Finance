using Finance.Application.UseCases.User.GetCurrentUser;
using Finance.Test.IntegrationTest.Commons;

namespace Finance.Test.IntegrationTest.Application.UseCase.User.GetCurrentUser
{
    public class GetCurrentUserHandlerTestFixture : FixtureBase
    {
        public GetCurrentUserRequest MakeGetCurrentUserRequest(string? tokenValue = null) => new(accessToken: tokenValue ?? Faker.Random.Guid().ToString());
    }
}