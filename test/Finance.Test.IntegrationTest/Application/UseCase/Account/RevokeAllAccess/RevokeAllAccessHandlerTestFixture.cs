using Finance.Application.UseCases.User.RevokeAllAccess;
using Finance.Infrastructure.Database.Models;
using Finance.Test.IntegrationTest.Commons;

namespace Finance.Test.IntegrationTest.Application.UseCase.User.RevokeAllAccess
{
    public class RevokeAllAccessHandlerTestFixture : FixtureBase
    {
        public RevokeAllAccessRequest MakeRevokeAllAccessRequest() => new();

        public List<UserModel> MakeAccounModelList()
        {
            return
            [
                MakeUserModel(),
                MakeUserModel(),
                MakeUserModel(),
            ];
        }
    }
}