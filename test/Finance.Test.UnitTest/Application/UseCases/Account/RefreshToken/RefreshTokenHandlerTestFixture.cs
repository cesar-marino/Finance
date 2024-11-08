using Finance.Application.UseCases.Account.RefreshToken;
using Finance.Test.UnitTest.Commons;

namespace Finance.Test.UnitTest.Application.UseCases.Account.RefreshToken
{
    public class RefreshTokenHandlerTestFixture : FixtureBase
    {
        public RefreshTokenRequest MakeRefreshTokenRequest() => new(accessToken: Faker.Random.Guid().ToString());
    }
}