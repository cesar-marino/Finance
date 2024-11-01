using Finance.Application.UseCases.Limit.CreateLimit;
using Finance.Domain.Entities;
using Finance.Domain.Exceptions;
using Finance.Domain.Repositories;
using Finance.Domain.SeedWork;
using FluentAssertions;
using Moq;

namespace Finance.Test.UnitTest.Application.UseCases.Limit.CreateLimit
{
    public class CreateLimitHandlerTest : IClassFixture<CreateLimitHandlerTestFixture>
    {
        private readonly CreateLimitHandlerTestFixture _fixture;
        private readonly CreateLimitHandler _sut;
        private readonly Mock<ILimitRepository> _limitRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public CreateLimitHandlerTest(CreateLimitHandlerTestFixture fixture)
        {
            _fixture = fixture;
            _limitRepositoryMock = new();
            _unitOfWorkMock = new();

            _sut = new(
                limitRepository: _limitRepositoryMock.Object,
                unitOfWork: _unitOfWorkMock.Object);
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatCheckAccountByIdAsyncThrows))]
        [Trait("Unit/UseCase", "Limit - CreateLimit")]
        public async Task ShouldRethrowSameExceptionThatCheckAccountByIdAsyncThrows()
        {
            _limitRepositoryMock
                .Setup(x => x.CheckAccountByIdAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new UnexpectedException());

            var request = _fixture.MakeCreateLimitRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }

        [Fact(DisplayName = nameof(ShouldThrowNotFoundExceptionIfCheckAccountByIdAsyncReturnsFalse))]
        [Trait("Unit/UseCase", "Limit - CreateLimit")]
        public async Task ShouldThrowNotFoundExceptionIfCheckAccountByIdAsyncReturnsFalse()
        {
            _limitRepositoryMock
                .Setup(x => x.CheckAccountByIdAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            var request = _fixture.MakeCreateLimitRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<NotFoundException>()
                .Where(x => x.Code == "not-found")
                .WithMessage("Account not found");
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatCheckCategoryByIdAsyncThrows))]
        [Trait("Unit/UseCase", "Limit - CreateLimit")]
        public async Task ShouldRethrowSameExceptionThatCheckCategoryByIdAsyncThrows()
        {
            _limitRepositoryMock
                .Setup(x => x.CheckAccountByIdAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            _limitRepositoryMock
                .Setup(x => x.CheckCategoryByIdAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new UnexpectedException());

            var request = _fixture.MakeCreateLimitRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }

        [Fact(DisplayName = nameof(ShouldThrowNotFoundIfCheckCategoryByIdAsyncReturnsFalse))]
        [Trait("Unit/UseCase", "Limit - CreateLimit")]
        public async Task ShouldThrowNotFoundIfCheckCategoryByIdAsyncReturnsFalse()
        {
            _limitRepositoryMock
                .Setup(x => x.CheckAccountByIdAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            _limitRepositoryMock
                .Setup(x => x.CheckCategoryByIdAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            var request = _fixture.MakeCreateLimitRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<NotFoundException>()
                .Where(x => x.Code == "not-found")
                .WithMessage("Category not found");
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatInsertAsyncThrows))]
        [Trait("Unit/UseCase", "Limit - CreateLimit")]
        public async Task ShouldRethrowSameExceptionThatInsertAsyncThrows()
        {
            _limitRepositoryMock
                .Setup(x => x.CheckAccountByIdAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            _limitRepositoryMock
                .Setup(x => x.CheckCategoryByIdAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            _limitRepositoryMock
                .Setup(x => x.InsertAsync(
                    It.IsAny<LimitEntity>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new UnexpectedException());

            var request = _fixture.MakeCreateLimitRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatCommitAsyncThrows))]
        [Trait("Unit/UseCase", "Limit - CreateLimit")]
        public async Task ShouldRethrowSameExceptionThatCommitAsyncThrows()
        {
            _limitRepositoryMock
                .Setup(x => x.CheckAccountByIdAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            _limitRepositoryMock
                .Setup(x => x.CheckCategoryByIdAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            _unitOfWorkMock
                .Setup(x => x.CommitAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(new UnexpectedException());

            var request = _fixture.MakeCreateLimitRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }

        [Fact(DisplayName = nameof(ShouldReturnTheCorrectResponseIfLimitIsAddedSucccessfully))]
        [Trait("Unit/UseCase", "Limit - CreateLimit")]
        public async Task ShouldReturnTheCorrectResponseIfLimitIsAddedSucccessfully()
        {
            _limitRepositoryMock
                .Setup(x => x.CheckAccountByIdAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            _limitRepositoryMock
                .Setup(x => x.CheckCategoryByIdAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            var request = _fixture.MakeCreateLimitRequest();
            var response = await _sut.Handle(request, _fixture.CancellationToken);

            response.AccountId.Should().Be(request.AccountId);
            response.Category.Id.Should().Be(request.CategoryId);
            response.LimitAmount.Should().Be(request.LimitAmount);
            response.Name.Should().Be(request.Name);
        }
    }
}
