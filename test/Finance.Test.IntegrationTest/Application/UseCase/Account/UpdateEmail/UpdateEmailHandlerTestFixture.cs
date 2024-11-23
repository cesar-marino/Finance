using Finance.Application.UseCases.Account.UpdateEmail;
using Finance.Test.IntegrationTest.Commons;

namespace Finance.Test.IntegrationTest.Application.UseCase.Account.UpdateEmail
{
    public class UpdateEmailHandlerTestFixture : FixtureBase
    {
        public UpdateEmailRequest MakeUpdateEmailRequest(
            Guid? accountId = null,
            string? email = null) => new(
                accountId: accountId ?? Faker.Random.Guid(),
                email: email ?? Faker.Internet.Email());
    }
}