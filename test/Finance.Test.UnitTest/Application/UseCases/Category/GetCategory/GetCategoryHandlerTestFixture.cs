using Finance.Application.UseCases.Category.GetCategory;
using Finance.Domain.Entities;
using Finance.Test.UnitTest.Commons;

namespace Finance.Test.UnitTest.Application.UseCases.Category.GetCategory
{
    public class GetCategoryHandlerTestFixture : FixtureBase
    {
        public GetCategoryRequest MakeGetCategoryRequest() => new(
            accountId: Faker.Random.Guid(),
            categoryId: Faker.Random.Guid());

        public IReadOnlyList<CategoryEntity> MakeSubCategories(Guid id)
        {
            return
            [
                MakeCategoryEntity(superCategoryId: id),
                MakeCategoryEntity(superCategoryId: id),
                MakeCategoryEntity(superCategoryId: id)
            ];
        }
    }
}
