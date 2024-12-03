using Finance.Application.UseCases.User.UpdateEmail;
using Finance.Test.IntegrationTest.Commons;

namespace Finance.Test.IntegrationTest.Application.UseCase.User.UpdateEmail
{
    public class UpdateEmailHandlerTestFixture : FixtureBase
    {
        public UpdateEmailRequest MakeUpdateEmailRequest(
            Guid? userId = null,
            string? email = null) => new(
                userId: userId ?? Faker.Random.Guid(),
                email: email ?? Faker.Internet.Email());
    }
}