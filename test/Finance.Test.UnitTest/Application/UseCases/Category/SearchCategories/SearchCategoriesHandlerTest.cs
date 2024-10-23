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

        //should return the correct response if SearchAsync returns category list
    }
}
