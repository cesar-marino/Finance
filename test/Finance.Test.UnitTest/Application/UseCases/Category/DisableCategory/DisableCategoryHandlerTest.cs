using Finance.Application.UseCases.Category.DisableCategory;
using Finance.Domain.Entities;
using Finance.Domain.Exceptions;
using Finance.Domain.Repositories;
using FluentAssertions;
using Moq;

namespace Finance.Test.UnitTest.Application.UseCases.Category.DisableCategory
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
                .Setup(x => x.FindAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new NotFoundException("Category"));

            var request = _fixture.MakeDisableCategoryRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<NotFoundException>()
                .Where(x => x.Code == "not-found")
                .WithMessage("Category not found");
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatUpdateAsyncThrows))]
        [Trait("Unit/UseCase", "Category - DisableCategory")]
        public async Task ShouldRethrowSameExceptionThatUpdateAsyncThrows()
        {
            var category = _fixture.MakeCategoryEntity();
            _categoryRepositoryMock
                .Setup(x => x.FindAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(category);

            _categoryRepositoryMock
                .Setup(x => x.UpdateAsync(
                    It.IsAny<CategoryEntity>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new UnexpectedException());

            var request = _fixture.MakeDisableCategoryRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }
    }
}
