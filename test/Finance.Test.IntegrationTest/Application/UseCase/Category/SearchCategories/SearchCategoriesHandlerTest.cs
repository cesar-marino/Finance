using Finance.Application.UseCases.Category.SearchCategories;
using Finance.Domain.Exceptions;
using Finance.Infrastructure.Database.Repositories;
using FluentAssertions;

namespace Finance.Test.IntegrationTest.Application.UseCase.Category.SearchCategories
{
    public class SearchCategoriesHandlerTest(SearchCategoriesHandlerTestFixture fixture) : IClassFixture<SearchCategoriesHandlerTestFixture>
    {
        private readonly SearchCategoriesHandlerTestFixture _fixture = fixture;

        [Fact(DisplayName = nameof(ShouldThrowUnexpectedException))]
        [Trait("Integration/UseCase", "Category - SearchCategories")]
        public async Task ShouldThrowUnexpectedException()
        {
            var context = _fixture.MakeFinanceContext();
            var repository = new CategoryRepository(context);

            var sut = new SearchCategoriesHandler(categoryRepository: repository);

            await context.DisposeAsync();

            var request = _fixture.MakeSearchCategoriesRequest();
            var act = () => sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }
    }
}