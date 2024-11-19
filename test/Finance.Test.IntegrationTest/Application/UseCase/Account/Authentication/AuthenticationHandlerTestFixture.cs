using Finance.Application.UseCases.Account.Authentication;
using Finance.Test.IntegrationTest.Commons;

namespace Finance.Test.IntegrationTest.Application.UseCase.Account.Authentication
{
    public class AuthenticationHandlerTestFixture : FixtureBase
    {
        public AuthenticationRequest MakeAuthenticationRequest(
            string? email = null,
            string? password = null) => new(
                email: email ?? Faker.Internet.Email(),
                password: password ?? Faker.Internet.Password());
    }
}