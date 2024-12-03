using Finance.Application.UseCases.User.UpdateUsername;
using Finance.Test.IntegrationTest.Commons;

namespace Finance.Test.IntegrationTest.Application.UseCase.User.UpdateUsername
{
    public class UpdateUsernameHandlerTestFixture : FixtureBase
    {
        public UpdateUsernameRequest MakeUpdateUsernameRequest(
            Guid? userId = null,
            string? username = null) => new(
                userId: userId ?? Faker.Random.Guid(),
                username: username ?? Faker.Internet.UserName());
    }
}