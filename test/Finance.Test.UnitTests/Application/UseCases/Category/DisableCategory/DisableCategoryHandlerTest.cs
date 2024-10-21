using Finance.Application.UseCases.Category.DisableCategory;
using Finance.Domain.Exceptions;
using Finance.Domain.Repositories;
using FluentAssertions;
using Moq;

namespace Finance.Test.UnitTests.Application.UseCases.Category.DisableCategory
{
    public class DisableCategoryHandlerTest : IClassFixture<DisableCategoryHandlerTestFixture>
    {
        private readonly DisableCategoryHandlerTestFixture _fixture;
        private readonly DisableCategoryHandler _sut;
        private readonly Mock<ICategoryRepository> _categoryRepositoryMock;

        public DisableCategoryHandlerTest(DisableCategoryHandlerTestFixture fixture)
        {
            _fixture = fixture;
            _categoryRepositoryMock = new();

            _sut = new(
                categoryRepository: _categoryRepositoryMock.Object);
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatFindAsyncThrows))]
        [Trait("Unit/UseCase", "Category - DisableCategory")]
        public async Task ShouldRethrowSameExceptionThatFindAsyncThrows()
        {
            _categoryRepositoryMock
                .Setup(x => x.FindAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new NotFoundException("Category"));

            var request = _fixture.MakeDisableCategoryRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<NotFoundException>()
                .Where(x => x.Code == "not-found")
                .WithMessage("Category not found");
        }
    }
}
