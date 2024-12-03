using Finance.Application.UseCases.User.UpdatePassword;
using Finance.Test.UnitTest.Commons;

namespace Finance.Test.UnitTest.Application.UseCases.User.UpdatePassword
{
    public class UpdatePasswordHandlerTestFixture : FixtureBase
    {
        public UpdatePasswordRequest MakeUpdatePasswordRequest() => new(
            userId: Faker.Random.Guid(),
            currentPassword: Faker.Internet.Password(),
            newPassword: Faker.Internet.Password());
    }
}