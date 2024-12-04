using Finance.Application.UseCases.Bank.CreateBank;
using Finance.Test.UnitTest.Commons;

namespace Finance.Test.UnitTest.Application.UseCases.Bank.CreateBank
{
    public class CreateBankHandlerTestFixture : FixtureBase
    {
        public CreateBankRequest MakeCreateBankRequest() => new(
            code: Faker.Random.String(5),
            name: Faker.Random.String(5),
            color: Faker.Random.String(5),
            logo: Faker.Internet.Url());
    }
}