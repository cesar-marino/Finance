using Finance.Application.UseCases.Category.EnableCategory;
using Finance.Test.IntegrationTest.Commons;

namespace Finance.Test.IntegrationTest.Application.UseCase.Category.EnableCategory
{
    public class EnableCategoryHandlerTestFixture : FixtureBase
    {
        public EnableCategoryRequest MakeEnableCategoryRequest(
            Guid? accountId = null,
            Guid? categoryId = null) => new(
                accountId: accountId ?? Faker.Random.Guid(),
                categoryId: categoryId ?? Faker.Random.Guid());
    }
}