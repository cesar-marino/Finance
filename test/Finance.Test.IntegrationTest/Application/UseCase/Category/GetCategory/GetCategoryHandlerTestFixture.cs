using Finance.Application.UseCases.Category.GetCategory;
using Finance.Test.IntegrationTest.Commons;

namespace Finance.Test.IntegrationTest.Application.UseCase.Category.GetCategory
{
    public class GetCategoryHandlerTestFixture : FixtureBase
    {
        public GetCategoryRequest MakeGetCategoryRequest(
            Guid? accountId = null,
            Guid? categoryId = null) => new(
                accountId: accountId ?? Faker.Random.Guid(),
                categoryId: categoryId ?? Faker.Random.Guid());
    }
}