using Finance.Application.UseCases.Category.EnableCategory;
using Finance.Domain.Exceptions;
using Finance.Infrastructure.Database.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace Finance.Test.IntegrationTest.Application.UseCase.Category.EnableCategory
{
    public class EnableCategoryHandlerTest(EnableCategoryHandlerTestFixture fixture) : IClassFixture<EnableCategoryHandlerTestFixture>
    {
        private readonly EnableCategoryHandlerTestFixture _fixture = fixture;

        [Fact(DisplayName = nameof(ShouldThrowNotFoundException))]
        [Trait("Integration/UseCase", "Category - EnableCategory")]
        public async Task ShouldThrowNotFoundException()
        {
            var context = _fixture.MakeFinanceContext();
            var repository = new CategoryRepository(context);

            var sut = new EnableCategoryHandler(categoryRepository: repository, unitOfWork: context);

            var request = _fixture.MakeEnableCategoryRequest();
            var act = () => sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<NotFoundException>()
                .Where(x => x.Code == "not-found")
                .WithMessage("Category not found");
        }

        [Fact(DisplayName = nameof(ShouldThrowUnexpectedException))]
        [Trait("Integration/UseCase", "Category - EnableCategory")]
        public async Task ShouldThrowUnexpectedException()
        {
            var context = _fixture.MakeFinanceContext();
            var repository = new CategoryRepository(context);

            var sut = new EnableCategoryHandler(categoryRepository: repository, unitOfWork: context);

            await context.DisposeAsync();

            var request = _fixture.MakeEnableCategoryRequest();
            var act = () => sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }

        [Fact(DisplayName = nameof(ShouldReturnTheCorrectResponseIfCategoryIsSuccessfullyEnabled))]
        [Trait("Integration/UseCase", "Category - EnableCategory")]
        public async Task ShouldReturnTheCorrectResponseIfCategoryIsSuccessfullyEnabled()
        {
            var context = _fixture.MakeFinanceContext();
            var repository = new CategoryRepository(context);

            var category = _fixture.MakeCategoryModel(active: false);
            var trackingInfo = await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();
            trackingInfo.State = EntityState.Detached;

            var sut = new EnableCategoryHandler(categoryRepository: repository, unitOfWork: context);

            var request = _fixture.MakeEnableCategoryRequest(accountId: category.AccountId, categoryId: category.CategoryId);
            var response = await sut.Handle(request, _fixture.CancellationToken);

            var categoryDb = await context.Categories.FirstOrDefaultAsync(x => x.CategoryId == category.CategoryId);
            categoryDb?.AccountId.Should().Be(response.AccountId);
            categoryDb?.Active.Should().BeTrue();
            categoryDb?.CategoryId.Should().Be(response.CategoryId);
            categoryDb?.CategoryType.Should().Be(response.CategoryType);
            categoryDb?.Color.Should().Be(response.Color);
            categoryDb?.CreatedAt.Should().Be(response.CreatedAt);
            categoryDb?.Icon.Should().Be(response.Icon);
            categoryDb?.Name.Should().Be(response.Name);
            categoryDb?.SuperCategoryId.Should().Be(response.SuperCategory?.Id);
        }
    }
}