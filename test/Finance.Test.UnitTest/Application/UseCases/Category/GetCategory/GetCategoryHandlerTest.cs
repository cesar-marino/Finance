using Finance.Application.UseCases.Category.GetCategory;
using Finance.Domain.Exceptions;
using Finance.Domain.Repositories;
using FluentAssertions;
using Moq;

namespace Finance.Test.UnitTest.Application.UseCases.Category.GetCategory
{
    public class GetCategoryHandlerTest : IClassFixture<GetCategoryHandlerTestFixture>
    {
        private readonly GetCategoryHandlerTestFixture _fixture;
        private readonly GetCategoryHandler _sut;
        private readonly Mock<ICategoryRepository> _categoryRepositoryMock;

        public GetCategoryHandlerTest(GetCategoryHandlerTestFixture fixture)
        {
            _fixture = fixture;
            _categoryRepositoryMock = new();

            _sut = new(
                categoryRepository: _categoryRepositoryMock.Object);
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatFindAsyncThrows))]
        [Trait("Unit/UseCase", "Category - GetCategory")]
        public async Task ShouldRethrowSameExceptionThatFindAsyncThrows()
        {
            _categoryRepositoryMock
                .Setup(x => x.FindAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new NotFoundException("Category"));

            var request = _fixture.MakeGetCategoryRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<NotFoundException>()
                .Where(x => x.Code == "not-found")
                .WithMessage("Category not found");
        }

        [Fact(DisplayName = nameof(ShouldReturnTheCorrectResponseIfFindAsyncReturnsValidCategory))]
        [Trait("Unit/UseCase", "Category - GetCategory")]
        public async Task ShouldReturnTheCorrectResponseIfFindAsyncReturnsValidCategory()
        {
            var category = _fixture.MakeCategoryEntity();
            _categoryRepositoryMock
                .Setup(x => x.FindAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(category);

            var request = _fixture.MakeGetCategoryRequest();
            var response = await _sut.Handle(request, _fixture.CancellationToken);

            response.AccountId.Should().Be(category.AccountId);
            response.Active.Should().Be(category.Active);
            response.CategoryId.Should().Be(category.Id);
            response.CategoryType.Should().Be(category.CategoryType);
            response.Color.Should().Be(category.Color);
            response.CreatedAt.Should().Be(category.CreatedAt);
            response.Icon.Should().Be(category.Icon);
            response.Name.Should().Be(category.Name);
        }
    }
}
