using Finance.Application.UseCases.User.RefreshToken;
using Finance.Test.UnitTest.Commons;

namespace Finance.Test.UnitTest.Application.UseCases.User.RefreshToken
{
    public class RefreshTokenHandlerTestFixture : FixtureBase
    {
        public RefreshTokenRequest MakeRefreshTokenRequest(string? refreshToken = null) => new(
            accessToken: Faker.Random.Guid().ToString(),
            refreshToken: refreshToken ?? Faker.Random.Guid().ToString());
    }
}