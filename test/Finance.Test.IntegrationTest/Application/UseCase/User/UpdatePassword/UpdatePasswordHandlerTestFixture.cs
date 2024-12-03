using Finance.Application.UseCases.User.UpdatePassword;
using Finance.Test.IntegrationTest.Commons;

namespace Finance.Test.IntegrationTest.Application.UseCase.User.UpdatePassword
{
    public class UpdatePasswordHandlerTestFixture : FixtureBase
    {
        public UpdatePasswordRequest MakeUpdatePasswordRequest(
            Guid? userId = null,
            string? currentPassword = null,
            string? newPassword = null) => new(
                userId: userId ?? Faker.Random.Guid(),
                currentPassword: currentPassword ?? Faker.Internet.Password(),
                newPassword: newPassword ?? Faker.Internet.Password());
    }
}