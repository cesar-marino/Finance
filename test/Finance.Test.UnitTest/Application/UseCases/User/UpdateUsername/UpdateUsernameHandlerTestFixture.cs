using Finance.Application.UseCases.User.UpdateUsername;
using Finance.Test.UnitTest.Commons;

namespace Finance.Test.UnitTest.Application.UseCases.User.UpdateUsername
{
    public class UpdateUsernameHandlerTestFixture : FixtureBase
    {
        public UpdateUsernameRequest MakeUpdateUsernameRequest() => new(
            userId: Faker.Random.Guid(),
            username: Faker.Internet.UserName());
    }
}