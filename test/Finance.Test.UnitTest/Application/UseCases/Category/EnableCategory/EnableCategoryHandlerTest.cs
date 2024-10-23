using Finance.Application.UseCases.Category.EnableCategory;
using Finance.Domain.Exceptions;
using Finance.Domain.Repositories;
using FluentAssertions;
using Moq;

namespace Finance.Test.UnitTest.Application.UseCases.Category.EnableCategory
{
    public class EnableCategoryHandlerTest : IClassFixture<EnableCategoryHandlerTestFixture>
    {
        private readonly EnableCategoryHandlerTestFixture _fixture;
        private readonly EnableCategoryHandler _sut;
        private readonly Mock<ICategoryRepository> _categoryRepositoryMock;

        public EnableCategoryHandlerTest(EnableCategoryHandlerTestFixture fixture)
        {
            _fixture = fixture;
            _categoryRepositoryMock = new();

            _sut = new(
                categoryRepository: _categoryRepositoryMock.Object);
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatFindAsyncThrows))]
        [Trait("Unit/UseCase", "Category - EnableCategory")]
        public async Task ShouldRethrowSameExceptionThatFindAsyncThrows()
        {
            _categoryRepositoryMock
                .Setup(x => x.FindAsync(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new NotFoundException("Category"));

            var request = _fixture.MakeEnableCategoryRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<NotFoundException>()
                .Where(x => x.Code == "not-found")
                .WithMessage("Category not found");
        }
    }
}
