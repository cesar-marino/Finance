using Finance.Application.UseCases.Bank.UpdateBank;
using Finance.Test.UnitTest.Commons;

namespace Finance.Test.UnitTest.Application.UseCases.Bank.UpdateBank
{
    public class UpdateBankHandlerTestFixture : FixtureBase
    {
        public UpdateBankRequest MakeUpdateBankRequest() => new(
            bankId: Faker.Random.Guid(),
            name: Faker.Random.String(5),
            code: Faker.Random.String(5),
            color: Faker.Random.String(5));
    }
}