using Finance.Application.UseCases.Category.CreateCategory;
using Finance.Test.UnitTests.Commons;

namespace Finance.Test.UnitTests.Application.UseCases.Category
{
    public class CreateCategoryHandlerTestFixture : FixtureBase
    {
        public CreateCategoryRequest MakeCreateCategoryRequest() => new(
                name: Faker.Random.String(5),
                icon: Faker.Random.String(5),
                color: Faker.Random.String(5));
    }
}
