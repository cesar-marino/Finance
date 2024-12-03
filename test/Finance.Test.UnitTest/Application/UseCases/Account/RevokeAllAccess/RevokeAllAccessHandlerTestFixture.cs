using Finance.Application.UseCases.User.RevokeAllAccess;
using Finance.Domain.Entities;
using Finance.Test.UnitTest.Commons;

namespace Finance.Test.UnitTest.Application.UseCases.User.RevokeAllAccess
{
    public class RevokeAllAccessHandlerTestFixture : FixtureBase
    {
        public RevokeAllAccessRequest MakeRevokeAllAccessRequest() => new();

        public IReadOnlyList<UserEntity> MakeAccountList()
        {
            return
            [
                MakeUserEntity(),
                MakeUserEntity(),
                MakeUserEntity()
            ];
        }
    }
}