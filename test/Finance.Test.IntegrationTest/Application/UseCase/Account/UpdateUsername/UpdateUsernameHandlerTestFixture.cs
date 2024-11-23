using Finance.Application.UseCases.Account.UpdateUsername;
using Finance.Test.IntegrationTest.Commons;

namespace Finance.Test.IntegrationTest.Application.UseCase.Account.UpdateUsername
{
    public class UpdateUsernameHandlerTestFixture : FixtureBase
    {
        public UpdateUsernameRequest MakeUpdateUsernameRequest(
            Guid? accountId = null,
            string? username = null) => new(
                accountId: accountId ?? Faker.Random.Guid(),
                username: username ?? Faker.Internet.UserName());
    }
}