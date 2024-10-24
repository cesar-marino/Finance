using Finance.Application.UseCases.Category.SearchCategories;
using Finance.Domain.Enums;
using Finance.Domain.Exceptions;
using Finance.Domain.Repositories;
using Finance.Domain.SeedWork;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Test.UnitTest.Application.UseCases.Category.SearchCategories
{
    public class SearchCategoriesHandlerTest : IClassFixture<SearchCategoriesHandlerTestFixture>
    {
        private readonly SearchCategoriesHandlerTestFixture _fixture;
        private readonly SearchCategoriesHandler _sut;
        private readonly Mock<ICategoryRepository> _categoryRepositoryMock;

        public SearchCategoriesHandlerTest(SearchCategoriesHandlerTestFixture fixture)
        {
            _fixture = fixture;
            _categoryRepositoryMock = new();

            _sut = new(
                categoryRepository: _categoryRepositoryMock.Object);
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatSearchAsyncThrows))]
        [Trait("Unit/UseCase", "Category - SearchCategories")]
        public async Task ShouldRethrowSameExceptionThatSearchAsyncThrows()
        {
            _categoryRepositoryMock
                .Setup(x => x.SearchAsync(
                    It.IsAny<bool?>(),
                    It.IsAny<CategoryType>(),
                    It.IsAny<string?>(),
                    It.IsAny<int?>(),
                    It.IsAny<int?>(),
                    It.IsAny<string?>(),
                    It.IsAny<SearchOrder?>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new UnexpectedException());

            var request = _fixture.MakeSearchCategoriesRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }

        [Fact(DisplayName = nameof(ShouldReturnTheCorrectResponseIfSearchAsyncReturnsValidCategoryList))]
        [Trait("Unit/UseCase", "Category - SearchCategories")]
        public async Task ShouldReturnTheCorrectResponseIfSearchAsyncReturnsValidCategoryList()
        {
            var result = _fixture.MakeSearchResult();
            _categoryRepositoryMock
                .Setup(x => x.SearchAsync(
                    It.IsAny<bool?>(),
                    It.IsAny<CategoryType>(),
                    It.IsAny<string?>(),
                    It.IsAny<int?>(),
                    It.IsAny<int?>(),
                    It.IsAny<string?>(),
                    It.IsAny<SearchOrder?>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(result);

            var request = _fixture.MakeSearchCategoriesRequest();
            var response = await _sut.Handle(request, _fixture.CancellationToken);

            response.CurrentPage.Should().Be(result.CurrentPage);
            response.Order.Should().Be(result.Order);
            response.OrderBy.Should().Be(result.OrderBy);
            response.PerPage.Should().Be(result.PerPage);
            response.Total.Should().Be(result.Total);
            response.Items.ToList().ForEach((item) =>
            {
                var category = result.Items.FirstOrDefault(x => x.Id == item.CategoryId);
                category?.AccountId.Should().Be(item.AccountId);
                category?.Active.Should().Be(item.Active);
                category?.CategoryType.Should().Be(item.CategoryType);
                category?.Color.Should().Be(item.Color);
                category?.CreatedAt.Should().Be(item.CreatedAt);
                category?.Icon.Should().Be(item.Icon);
                category?.Id.Should().Be(item.CategoryId);
                category?.Name.Should().Be(item.Name);
                category?.UpdatedAt.Should().Be(item.UpdatedAt);
            });
        }
    }
}
