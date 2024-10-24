using Finance.Application.UseCases.Category.UpdateCategory;
using Finance.Test.UnitTest.Commons;

namespace Finance.Test.UnitTest.Application.UseCases.Category.UpdateCategory
{
    public class UpdateCategoryHandlerTestFixture : FixtureBase
    {
        public UpdateCategoryRequest MakeUpdateCategoryRequest() => new(
            accountId: Faker.Random.Guid(),
            categoryId: Faker.Random.Guid(),
            active: Faker.Random.Bool(),
            categoryType: Domain.Enums.CategoryType.Revenue,
            name: Faker.Random.String(5),
            icon: Faker.Random.String(5),
            color: Faker.Random.String(5));
    }
}
