using Finance.Application.UseCases.Category.CreateCategory;
using Finance.Domain.Entities;
using Finance.Domain.Exceptions;
using Finance.Domain.Repositories;
using Finance.Domain.SeedWork;
using FluentAssertions;
using Moq;

namespace Finance.Test.UnitTest.Application.UseCases.Category.CreateCategory
{
    public class CreateCategoryHandlerTest : IClassFixture<CreateCategoryHandlerTestFixture>
    {
        private readonly CreateCategoryHandlerTestFixture _fixture;
        private readonly CreateCategoryHandler _sut;
        private readonly Mock<ICategoryRepository> _categoryRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public CreateCategoryHandlerTest(CreateCategoryHandlerTestFixture fixture)
        {
            _fixture = fixture;
            _categoryRepositoryMock = new();
            _unitOfWorkMock = new();

            _sut = new(
                categoryRepository: _categoryRepositoryMock.Object,
                unitOfWork: _unitOfWorkMock.Object);
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatInsertAsyncThrows))]
        [Trait("Unit/UseCase", "Category - CreateCategory")]
        public async Task ShouldRethrowSameExceptionThatInsertAsyncThrows()
        {
            _categoryRepositoryMock
                .Setup(x => x.InsertAsync(It.IsAny<CategoryEntity>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new UnexpectedException());

            var request = _fixture.MakeCreateCategoryRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatCommitAsyncThrows))]
        [Trait("Unit/UseCase", "Category - CreateCategory")]
        public async Task ShouldRethrowSameExceptionThatCommitAsyncThrows()
        {
            _unitOfWorkMock
                .Setup(x => x.CommitAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(new UnexpectedException());

            var request = _fixture.MakeCreateCategoryRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }

        [Fact(DisplayName = nameof(ShouldReturnTheCorrectResponseIfCategoryIsAddedSuccessfully))]
        [Trait("Unit/UseCase", "Category - CreateCategory")]
        public async Task ShouldReturnTheCorrectResponseIfCategoryIsAddedSuccessfully()
        {
            var request = _fixture.MakeCreateCategoryRequest();
            var response = await _sut.Handle(request, _fixture.CancellationToken);

            response.AccountId.Should().Be(request.AccountId);
            response.Active.Should().BeTrue();
            response.CategoryType.Should().Be(request.CategoryType);
            response.Color.Should().Be(request.Color);
            response.Icon.Should().Be(request.Icon);
            response.Name.Should().Be(request.Name);
            response.SuperCategory?.Id.Should().Be(response.SuperCategory.Id);
        }
    }
}
