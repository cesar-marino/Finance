using Finance.Application.UseCases.Category.GetCategory;
using Finance.Domain.Exceptions;
using Finance.Infrastructure.Database.Repositories;
using FluentAssertions;

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
    }
}