using Finance.Application.UseCases.Category.DisableCategory;
using Finance.Domain.Exceptions;
using Finance.Infrastructure.Database.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace Finance.Test.IntegrationTest.Application.UseCase.Category.DisableCategory
{
    public class DisableCategoryHandlerTest(DisableCategoryHandlerTestFixture fixture) : IClassFixture<DisableCategoryHandlerTestFixture>
    {
        private readonly DisableCategoryHandlerTestFixture _fixture = fixture;

        [Fact(DisplayName = nameof(ShouldThrowNotFoundException))]
        [Trait("Integration/UseCase", "Category - DisableCategory")]
        public async Task ShouldThrowNotFoundException()
        {
            var context = _fixture.MakeFinanceContext();
            var repository = new CategoryRepository(context);

            var sut = new DisableCategoryHandler(categoryRepository: repository, unitOfWork: context);

            var request = _fixture.MakeDisableCategoryRequest();
            var act = () => sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<NotFoundException>()
                .Where(x => x.Code == "not-found")
                .WithMessage("Category not found");
        }

        [Fact(DisplayName = nameof(ShouldReturnTheCorrectResponseIfCategoryIsSuccessfullyDisabled))]
        [Trait("Integration/UseCase", "Category - DisableCategory")]
        public async Task ShouldReturnTheCorrectResponseIfCategoryIsSuccessfullyDisabled()
        {
            var user = _fixture.MakeUserModel();
            var category = _fixture.MakeCategoryModel(userId: user.UserId);

            var context = _fixture.MakeFinanceContext();
            var repository = new CategoryRepository(context);

            var userTrackingInfo = await context.Users.AddAsync(user);
            var categoryTrackingInfo = await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();
            userTrackingInfo.State = EntityState.Detached;
            categoryTrackingInfo.State = EntityState.Detached;

            var sut = new DisableCategoryHandler(categoryRepository: repository, unitOfWork: context);

            var request = _fixture.MakeDisableCategoryRequest(userId: category.UserId, categoryId: category.CategoryId);
            var response = await sut.Handle(request, _fixture.CancellationToken);

            var categoryDb = await context.Categories.FirstOrDefaultAsync(x => x.CategoryId == category.CategoryId);
            categoryDb?.UserId.Should().Be(response.UserId);
            categoryDb?.Active.Should().BeFalse();
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