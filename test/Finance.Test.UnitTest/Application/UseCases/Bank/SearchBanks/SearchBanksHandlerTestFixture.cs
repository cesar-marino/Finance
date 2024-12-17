using Finance.Application.UseCases.Bank.SearchBanks;
using Finance.Domain.SeedWork;
using Finance.Test.UnitTest.Commons;

namespace Finance.Test.UnitTest.Application.UseCases.Bank.SearchBanks
{
    public class SearchBanksHandlerTestFixture : FixtureBase
    {
        public SearchBanksRequest MakeSearchBanksRequest() => new(
            active: Faker.Random.Bool(),
            code: Faker.Random.String(5),
            name: Faker.Random.String(5),
            currentPage: Faker.Random.Int(5),
            perPage: Faker.Random.Int(5),
            orderBy: Faker.Random.String(5),
            order: SearchOrder.Asc);
    }
}