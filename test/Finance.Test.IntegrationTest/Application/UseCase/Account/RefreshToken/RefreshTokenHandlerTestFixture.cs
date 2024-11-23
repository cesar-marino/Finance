using Finance.Application.UseCases.Account.RefreshToken;
using Finance.Test.IntegrationTest.Commons;

namespace Finance.Test.IntegrationTest.Application.UseCase.Account.RefreshToken
{
    public class RefreshTokenHandlerTestFixture : FixtureBase
    {
        public RefreshTokenRequest MakeRefreshTokenRequest(
            string? accessToken = null,
            string? refreshToken = null) => new(
                accessToken: accessToken ?? Faker.Random.Guid().ToString(),
                refreshToken: refreshToken ?? Faker.Random.Guid().ToString());
    }
}