using Finance.Application.UseCases.User.UpdateEmail;
using Finance.Test.UnitTest.Commons;

namespace Finance.Test.UnitTest.Application.UseCases.User.UpdateEmail
{
    public class UpdateEmailHandlerTestFixture : FixtureBase
    {
        public UpdateEmailRequest MakeUpdateEmailRequest() => new(
            userId: Faker.Random.Guid(),
            email: Faker.Internet.Email());
    }
}