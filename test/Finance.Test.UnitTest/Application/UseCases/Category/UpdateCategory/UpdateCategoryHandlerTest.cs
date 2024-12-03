using Finance.Application.UseCases.Category.UpdateCategory;
using Finance.Domain.Entities;
using Finance.Domain.Exceptions;
using Finance.Domain.Repositories;
using Finance.Domain.SeedWork;
using FluentAssertions;
using Moq;

namespace Finance.Test.UnitTest.Application.UseCases.Category.UpdateCategory
{
    public class UpdateCategoryHandlerTest : IClassFixture<UpdateCategoryHandlerTestFixture>
    {
        private readonly UpdateCategoryHandlerTestFixture _fixture;
        private readonly UpdateCategoryHandler _sut;
        private readonly Mock<ICategoryRepository> _categoryRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWrokMock;

        public UpdateCategoryHandlerTest(UpdateCategoryHandlerTestFixture fixture)
        {
            _fixture = fixture;
            _categoryRepositoryMock = new();
            _unitOfWrokMock = new();

            _sut = new(
                categoryRepository: _categoryRepositoryMock.Object,
                unitOfWork: _unitOfWrokMock.Object);
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatFindAsyncThrows))]
        [Trait("Unit/UseCase", "Category - UpdateCategory")]
        public async Task ShouldRethrowSameExceptionThatFindAsyncThrows()
        {
            _categoryRepositoryMock
                .Setup(x => x.FindAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new NotFoundException("Category"));

            var request = _fixture.MakeUpdateCategoryRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<NotFoundException>()
                .Where(x => x.Code == "not-found")
                .WithMessage("Category not found");
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatUpdateAsyncThrows))]
        [Trait("Unit/UseCase", "Category - UpdateCategory")]
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

            var request = _fixture.MakeUpdateCategoryRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatCommitAsyncThrows))]
        [Trait("Unit/UseCase", "Category - UpdateCategory")]
        public async Task ShouldRethrowSameExceptionThatCommitAsyncThrows()
        {
            var category = _fixture.MakeCategoryEntity();
            _categoryRepositoryMock
                .Setup(x => x.FindAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(category);

            _unitOfWrokMock
                .Setup(x => x.CommitAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(new UnexpectedException());

            var request = _fixture.MakeUpdateCategoryRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }

        [Fact(DisplayName = nameof(ShouldReturnTheCorrectResponseIfCategoryIsUpdatedSuccessfully))]
        [Trait("Unit/UseCase", "Category - UpdateCategory")]
        public async Task ShouldReturnTheCorrectResponseIfCategoryIsUpdatedSuccessfully()
        {
            var category = _fixture.MakeCategoryEntity();
            _categoryRepositoryMock
                .Setup(x => x.FindAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(category);

            var request = _fixture.MakeUpdateCategoryRequest();
            var response = await _sut.Handle(request, _fixture.CancellationToken);

            response.UserId.Should().Be(category.UserId);
            response.Active.Should().Be(request.Active);
            response.CategoryId.Should().Be(category.Id);
            response.CategoryType.Should().Be(request.CategoryType);
            response.Color.Should().Be(request.Color);
            response.CreatedAt.Should().Be(category.CreatedAt);
            response.Icon.Should().Be(request.Icon);
            response.Name.Should().Be(request.Name);
        }
    }
}
