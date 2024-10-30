using Finance.Application.UseCases.Category.CreateCategory;
using Finance.Test.UnitTest.Commons;

namespace Finance.Test.UnitTest.Application.UseCases.Category.CreateCategory
{
    public class CreateCategoryHandlerTestFixture : FixtureBase
    {
        public CreateCategoryRequest MakeCreateCategoryRequest() => new(
            accountId: Faker.Random.Guid(),
            categoryType: Domain.Enums.CategoryType.Revenue,
            name: Faker.Random.String(5),
            icon: Faker.Random.String(5),
            color: Faker.Random.String(5),
            superCategoryId: Faker.Random.Guid());
    }
}
