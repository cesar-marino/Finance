using Finance.Application.UseCases.Bank.DisableBank;
using Finance.Test.UnitTest.Commons;

namespace Finance.Test.UnitTest.Application.UseCases.Bank.DisableBank
{
    public class DisableBankHandlerTestFixture : FixtureBase
    {
        public DisableBankRequest MakeDisableBankRequest() => new(bankId: Faker.Random.Guid());
    }
}