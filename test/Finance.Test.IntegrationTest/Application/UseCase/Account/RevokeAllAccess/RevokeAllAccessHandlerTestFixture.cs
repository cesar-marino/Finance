using Finance.Application.UseCases.Account.RevokeAllAccess;
using Finance.Test.IntegrationTest.Commons;

namespace Finance.Test.IntegrationTest.Application.UseCase.Account.RevokeAllAccess
{
    public class RevokeAllAccessHandlerTestFixture : FixtureBase
    {
        public RevokeAllAccessRequest MakeRevokeAllAccessRequest() => new();
    }
}