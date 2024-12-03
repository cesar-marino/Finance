using Finance.Application.UseCases.User.Authentication;
using Finance.Test.UnitTest.Commons;

namespace Finance.Test.UnitTest.Application.UseCases.User.Authentication
{
    public class AuthenticationHandlerTestFixture : FixtureBase
    {
        public AuthenticationRequest MakeAuthenticationRequest() => new(
            email: Faker.Internet.Email(),
            password: Faker.Internet.Password());
    }
}