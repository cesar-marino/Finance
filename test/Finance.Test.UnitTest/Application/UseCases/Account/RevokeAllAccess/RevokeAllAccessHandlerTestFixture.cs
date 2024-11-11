using Finance.Application.UseCases.Account.RevokeAllAccess;
using Finance.Test.UnitTest.Commons;

namespace Finance.Test.UnitTest.Application.UseCases.Account.RevokeAllAccess
{
    public class RevokeAllAccessHandlerTestFixture : FixtureBase
    {
        public RevokeAllAccessRequest MakeRevokeAllAccessRequest() => new();
    }
}