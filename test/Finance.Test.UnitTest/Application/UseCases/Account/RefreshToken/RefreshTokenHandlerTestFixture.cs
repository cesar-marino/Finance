using Finance.Application.UseCases.Account.RefreshToken;
using Finance.Test.UnitTest.Commons;

namespace Finance.Test.UnitTest.Application.UseCases.Account.RefreshToken
{
    public class RefreshTokenHandlerTestFixture : FixtureBase
    {
        public RefreshTokenRequest MakeRefreshTokenRequest(string? refreshToken = null) => new(
            accessToken: Faker.Random.Guid().ToString(),
            refreshToken: refreshToken ?? Faker.Random.Guid().ToString());
    }
}