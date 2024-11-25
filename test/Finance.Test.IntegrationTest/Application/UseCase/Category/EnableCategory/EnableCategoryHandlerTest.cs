using Finance.Application.UseCases.Category.EnableCategory;
using Finance.Domain.Exceptions;
using Finance.Infrastructure.Database.Repositories;
using FluentAssertions;

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
    }
}