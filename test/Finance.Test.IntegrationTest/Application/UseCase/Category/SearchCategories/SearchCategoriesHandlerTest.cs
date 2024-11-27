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

        [Fact(DisplayName = nameof(ShouldReturnAnEmptyListIfSearchFindsNoResults))]
        [Trait("Integration/UseCase", "Category - SearchCategories")]
        public async Task ShouldReturnAnEmptyListIfSearchFindsNoResults()
        {
            var context = _fixture.MakeFinanceContext();
            var repository = new CategoryRepository(context);

            var sut = new SearchCategoriesHandler(categoryRepository: repository);

            var request = _fixture.MakeSearchCategoriesRequest();
            var response = await sut.Handle(request, _fixture.CancellationToken);

            response.CurrentPage.Should().Be(request.CurrentPage);
            response.Order.Should().Be(request.Order);
            response.OrderBy.Should().Be(request.OrderBy);
            response.PerPage.Should().Be(request.PerPage);
            response.Total.Should().Be(response.Items.Count);
            response.Items.Should().BeEmpty();
        }

        //should return the correct response if SearchAsync returns filtered categories
    }
}