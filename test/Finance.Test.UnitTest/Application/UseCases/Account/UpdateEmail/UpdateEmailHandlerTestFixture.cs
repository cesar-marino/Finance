using Finance.Application.UseCases.Account.UpdateEmail;
using Finance.Test.UnitTest.Commons;

namespace Finance.Test.UnitTest.Application.UseCases.Account.UpdateEmail
{
    public class UpdateEmailHandlerTestFixture : FixtureBase
    {
        public UpdateEmailRequest MakeUpdateEmailRequest() => new(
            accountId: Faker.Random.Guid(),
            email: Faker.Internet.Email());
    }
}