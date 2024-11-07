using Finance.Application.UseCases.Account.CreateAccount;
using Finance.Test.UnitTest.Commons;

namespace Finance.Test.UnitTest.Application.UseCases.Account.CreateAccount
{
    public class CreateAccountHandlerTestFixture : FixtureBase
    {
        public CreateAccountRequest MakeCreateAccountRequest() => new(
            username: Faker.Internet.UserName(),
            email: Faker.Internet.Email(),
            password: Faker.Internet.Password(),
            phone: Faker.Phone.ToString());
    }
}