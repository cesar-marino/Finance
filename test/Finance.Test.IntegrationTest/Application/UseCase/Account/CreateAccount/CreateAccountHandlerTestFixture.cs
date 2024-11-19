using Finance.Application.UseCases.Account.CreateAccount;
using Finance.Test.IntegrationTest.Commons;

namespace Finance.Test.IntegrationTest.Application.UseCase.Account.CreateAccount
{
    public class CreateAccountHandlerTestFixture : FixtureBase
    {
        public CreateAccountRequest MakeCreateAccountRequest(
            string? username = null,
            string? email = null,
            string? password = null,
            string? phone = null) => new(
                username: username ?? Faker.Internet.UserName(),
                email: email ?? Faker.Internet.Email(),
                password: password ?? Faker.Internet.Password(),
                phone: phone ?? Faker.Phone.PhoneNumber());
    }
}