using Finance.Application.UseCases.Account.UpdateUsername;
using Finance.Test.UnitTest.Commons;

namespace Finance.Test.UnitTest.Application.UseCases.Account.UpdateUsername
{
    public class UpdateUsernameHandlerTestFixture : FixtureBase
    {
        public UpdateUsernameRequest MakeUpdateUsernameRequest() => new(
            accountId: Faker.Random.Guid(),
            username: Faker.Internet.UserName());
    }
}