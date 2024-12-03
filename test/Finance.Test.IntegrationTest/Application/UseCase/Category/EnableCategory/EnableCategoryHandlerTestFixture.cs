using Finance.Application.UseCases.Category.EnableCategory;
using Finance.Test.IntegrationTest.Commons;

namespace Finance.Test.IntegrationTest.Application.UseCase.Category.EnableCategory
{
    public class EnableCategoryHandlerTestFixture : FixtureBase
    {
        public EnableCategoryRequest MakeEnableCategoryRequest(
            Guid? userId = null,
            Guid? categoryId = null) => new(
                userId: userId ?? Faker.Random.Guid(),
                categoryId: categoryId ?? Faker.Random.Guid());
    }
}