using Finance.Application.UseCases.Account.Authentication;
using Finance.Test.UnitTest.Commons;

namespace Finance.Test.UnitTest.Application.UseCases.Account.Authentication
{
    public class AuthenticationHandlerTestFixture : FixtureBase
    {
        public AuthenticationRequest MakeAuthenticationRequest() => new(
            email: Faker.Internet.Email(),
            password: Faker.Internet.Password());
    }
}