using Finance.Application.UseCases.Category.GetCategory;
using Finance.Domain.Exceptions;
using Finance.Infrastructure.Database.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace Finance.Test.IntegrationTest.Application.UseCase.Category.GetCategory
{
    public class GetCategoryHandlerTest(GetCategoryHandlerTestFixture fixture) : IClassFixture<GetCategoryHandlerTestFixture>
    {
        private readonly GetCategoryHandlerTestFixture _fixture = fixture;

        [Fact(DisplayName = nameof(ShouldThrowNotFoundException))]
        [Trait("Integration/UseCase", "Category - GetCategory")]
        public async Task ShouldThrowNotFoundException()
        {
            var context = _fixture.MakeFinanceContext();
            var repository = new CategoryRepository(context);

            var sut = new GetCategoryHandler(categoryRepository: repository);

            var request = _fixture.MakeGetCategoryRequest();
            var act = () => sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<NotFoundException>()
                .Where(x => x.Code == "not-found")
                .WithMessage("Category not found");
        }

        [Fact(DisplayName = nameof(ShouldReturnTheCorrectResponseIfUserIsFound))]
        [Trait("Integration/UseCase", "Category - GetCategory")]
        public async Task ShouldReturnTheCorrectResponseIfUserIsFound()
        {
            var context = _fixture.MakeFinanceContext();
            var repository = new CategoryRepository(context);

            var category = _fixture.MakeCategoryModel();
            var categories = _fixture.MakeCategoryList(superCategoryId: category.CategoryId);

            var trackingInfo = await context.Categories.AddAsync(category);
            await context.Categories.AddRangeAsync(categories);
            await context.SaveChangesAsync();
            trackingInfo.State = EntityState.Detached;

            var sut = new GetCategoryHandler(categoryRepository: repository);

            var request = _fixture.MakeGetCategoryRequest(userId: category.UserId, categoryId: category.CategoryId);
            var response = await sut.Handle(request, _fixture.CancellationToken);

            var categoryDb = await context.Categories
                .Include(x => x.SubCategories)
                .FirstOrDefaultAsync(x => x.CategoryId == category.CategoryId);

            var categoriesDb = await context.Categories
                .Where(x => x.SuperCategoryId == category.CategoryId)
                .ToListAsync();

            categoryDb?.UserId.Should().Be(response.UserId);
            categoryDb?.Active.Should().Be(response.Active);
            categoryDb?.CategoryId.Should().Be(response.CategoryId);
            categoryDb?.CategoryType.Should().Be(response.CategoryType);
            categoryDb?.Color.Should().Be(response.Color);
            categoryDb?.CreatedAt.Should().Be(response.CreatedAt);
            categoryDb?.Icon.Should().Be(response.Icon);
            categoryDb?.Name.Should().Be(response.Name);
            categoryDb?.SuperCategoryId.Should().Be(response.SuperCategory?.Id);
            categoriesDb?.Count.Should().Be(response.SubCategories?.Count);

            categoriesDb?.ForEach((subCategory) =>
            {
                var categoryResponse = response.SubCategories?.FirstOrDefault(x => x.Id == subCategory.CategoryId);
                categoryResponse?.Should().NotBeNull();
                categoryResponse?.Color.Should().Be(subCategory.Color);
                categoryResponse?.Icon.Should().Be(subCategory.Icon);
                categoryResponse?.Id.Should().Be(subCategory.CategoryId);
                categoryResponse?.Name.Should().Be(subCategory.Name);
            });
        }
    }
}