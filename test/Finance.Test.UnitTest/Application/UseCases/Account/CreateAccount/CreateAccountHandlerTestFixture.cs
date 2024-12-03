using Finance.Application.UseCases.User.CreateUser;
using Finance.Test.UnitTest.Commons;

namespace Finance.Test.UnitTest.Application.UseCases.User.CreateUser
{
    public class CreateUserHandlerTestFixture : FixtureBase
    {
        public CreateUserRequest MakeCreateUserRequest() => new(
            username: Faker.Internet.UserName(),
            email: Faker.Internet.Email(),
            password: Faker.Internet.Password(),
            phone: Faker.Phone.ToString());
    }
}