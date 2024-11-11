using Finance.Application.UseCases.Account.RevokeAllAccess;
using Finance.Domain.Entities;
using Finance.Test.UnitTest.Commons;

namespace Finance.Test.UnitTest.Application.UseCases.Account.RevokeAllAccess
{
    public class RevokeAllAccessHandlerTestFixture : FixtureBase
    {
        public RevokeAllAccessRequest MakeRevokeAllAccessRequest() => new();

        public IReadOnlyList<AccountEntity> MakeAccountList()
        {
            return
            [
                MakeAccountEntity(),
                MakeAccountEntity(),
                MakeAccountEntity()
            ];
        }
    }
}