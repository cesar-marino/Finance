using Finance.Application.UseCases.Category.CreateCategory;
using Finance.Domain.Exceptions;
using Finance.Infrastructure.Database.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace Finance.Test.IntegrationTest.Application.UseCase.Category.CreateCategory
{
    public class CreateCategoryHandlerTest(CreateCategoryHandlerTestFixture fixture) : IClassFixture<CreateCategoryHandlerTestFixture>
    {
        private readonly CreateCategoryHandlerTestFixture _fixture = fixture;

        [Fact(DisplayName = nameof(ShouldThrowNotFoundException))]
        [Trait("Integration/UseCase", "Category - CreateCategory")]
        public async Task ShouldThrowNotFoundException()
        {
            var context = _fixture.MakeFinanceContext();
            var repository = new CategoryRepository(context);

            var sut = new CreateCategoryHandler(categoryRepository: repository, unitOfWork: context);

            var request = _fixture.MakeCreateCategoryRequest();
            var act = () => sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<NotFoundException>()
                .Where(x => x.Code == "not-found")
                .WithMessage("User not found");
        }

        [Fact(DisplayName = nameof(ShouldThrowUnexpectedException))]
        [Trait("Integration/UseCase", "Category - CreateCategory")]
        public async Task ShouldThrowUnexpectedException()
        {
            var context = _fixture.MakeFinanceContext();
            var repository = new CategoryRepository(context);

            var user = _fixture.MakeUserModel();
            var trackingInfo = await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
            trackingInfo.State = EntityState.Detached;

            var sut = new CreateCategoryHandler(categoryRepository: repository, unitOfWork: context);

            await context.DisposeAsync();

            var request = _fixture.MakeCreateCategoryRequest(userId: user.UserId);
            var act = () => sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }

        [Fact(DisplayName = nameof(ShouldReturnTheCorrectResponseIfCategoryIsSuccessfullyCreated))]
        [Trait("Integration/UseCase", "Category - CreateCategory")]
        public async Task ShouldReturnTheCorrectResponseIfCategoryIsSuccessfullyCreated()
        {
            var context = _fixture.MakeFinanceContext();
            var repository = new CategoryRepository(context);

            var user = _fixture.MakeUserModel();
            var trackingInfo = await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
            trackingInfo.State = EntityState.Detached;

            var sut = new CreateCategoryHandler(categoryRepository: repository, unitOfWork: context);

            var request = _fixture.MakeCreateCategoryRequest(userId: user.UserId);
            var response = await sut.Handle(request, _fixture.CancellationToken);

            var categoryDb = await context.Categories.FirstOrDefaultAsync();
            categoryDb?.UserId.Should().Be(response.UserId);
            categoryDb?.Active.Should().Be(response.Active);
            categoryDb?.CategoryType.Should().Be(response.CategoryType);
            categoryDb?.Color.Should().Be(response.Color);
            categoryDb?.CreatedAt.Should().Be(response.CreatedAt);
            categoryDb?.Icon.Should().Be(response.Icon);
            categoryDb?.Name.Should().Be(response.Name);
            categoryDb?.SuperCategoryId.Should().Be(response.SuperCategory?.Id);
        }
    }
}