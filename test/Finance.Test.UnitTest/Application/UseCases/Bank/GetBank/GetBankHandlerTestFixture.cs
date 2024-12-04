using Finance.Application.UseCases.Bank.GetBank;
using Finance.Test.UnitTest.Commons;

namespace Finance.Test.UnitTest.Application.UseCases.Bank.GetBank
{
    public class GetBankHandlerTestFixture : FixtureBase
    {
        public GetBankRequest MakeGetBankRequest() => new(bankId: Faker.Random.Guid());
    }
}