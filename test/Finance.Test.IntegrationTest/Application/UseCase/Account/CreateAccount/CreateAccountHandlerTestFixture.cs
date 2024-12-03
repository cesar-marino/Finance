using Finance.Application.UseCases.User.CreateUser;
using Finance.Test.IntegrationTest.Commons;

namespace Finance.Test.IntegrationTest.Application.UseCase.User.CreateUser
{
    public class CreateUserHandlerTestFixture : FixtureBase
    {
        public CreateUserRequest MakeCreateUserRequest(
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