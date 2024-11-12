using Finance.Application.UseCases.Account.UpdatePassword;
using Finance.Test.UnitTest.Commons;

namespace Finance.Test.UnitTest.Application.UseCases.Account.UpdatePassword
{
    public class UpdatePasswordHandlerTestFixture : FixtureBase
    {
        public UpdatePasswordRequest MakeUpdatePasswordRequest() => new(
            accountId: Faker.Random.Guid(),
            currentPassword: Faker.Internet.Password(),
            newPassword: Faker.Internet.Password());
    }
}