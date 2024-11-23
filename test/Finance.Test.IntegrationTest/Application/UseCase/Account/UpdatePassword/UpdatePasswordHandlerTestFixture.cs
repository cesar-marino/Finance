using Finance.Application.UseCases.Account.UpdatePassword;
using Finance.Test.IntegrationTest.Commons;

namespace Finance.Test.IntegrationTest.Application.UseCase.Account.UpdatePassword
{
    public class UpdatePasswordHandlerTestFixture : FixtureBase
    {
        public UpdatePasswordRequest MakeUpdatePasswordRequest(
            Guid? accountId = null,
            string? currentPassword = null,
            string? newPassword = null) => new(
                accountId: accountId ?? Faker.Random.Guid(),
                currentPassword: currentPassword ?? Faker.Internet.Password(),
                newPassword: newPassword ?? Faker.Internet.Password());
    }
}