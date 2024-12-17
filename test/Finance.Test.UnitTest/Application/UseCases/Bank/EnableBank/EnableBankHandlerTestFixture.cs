using Finance.Application.UseCases.Bank.EnableBank;
using Finance.Test.UnitTest.Commons;

namespace Finance.Test.UnitTest.Application.UseCases.Bank.EnableBank
{
    public class EnableBankHandlerTestFixture : FixtureBase
    {
        public EnableBankRequest MakeEnableBankRequest() => new(bankId: Faker.Random.Guid());
    }
}