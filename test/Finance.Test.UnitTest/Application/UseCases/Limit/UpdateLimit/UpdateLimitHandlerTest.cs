using Finance.Application.UseCases.Limit.UpdateLimit;
using Finance.Domain.Entities;
using Finance.Domain.Exceptions;
using Finance.Domain.Repositories;
using Finance.Domain.SeedWork;
using FluentAssertions;
using Moq;

namespace Finance.Test.UnitTest.Application.UseCases.Limit.UpdateLimit
{
    public class UpdateLimitHandlerTest : IClassFixture<UpdateLimitHandlerTestFixture>
    {
        private readonly UpdateLimitHandlerTestFixture _fixture;
        private readonly UpdateLimitHandler _sut;
        private readonly Mock<ILimitRepository> _limitRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public UpdateLimitHandlerTest(UpdateLimitHandlerTestFixture fixture)
        {
            _fixture = fixture;
            _limitRepositoryMock = new();
            _unitOfWorkMock = new();

            _sut = new(
                limitRepository: _limitRepositoryMock.Object,
                unitOfWork: _unitOfWorkMock.Object);
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatFindAsyncThrows))]
        [Trait("Unit/UseCase", "Limit - UpdateLimit")]
        public async Task ShouldRethrowSameExceptionThatFindAsyncThrows()
        {
            _limitRepositoryMock
                .Setup(x => x.FindAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new NotFoundException("Limit"));

            var request = _fixture.MakeUpdateLimitRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<NotFoundException>()
                .Where(x => x.Code == "not-found")
                .WithMessage("Limit not found");
        }

        [Fact(DisplayName = nameof(ShouldRetrowSameExceptionThatCheckAccountByIdAsyncThrows))]
        [Trait("Unit/UseCase", "Limit - UpdateLimit")]
        public async Task ShouldRetrowSameExceptionThatCheckAccountByIdAsyncThrows()
        {
            var limit = _fixture.MakeLimitEntity();
            _limitRepositoryMock
                .Setup(x => x.FindAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(limit);

            _limitRepositoryMock
                .Setup(x => x.CheckAccountByIdAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new UnexpectedException());

            var request = _fixture.MakeUpdateLimitRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }

        [Fact(DisplayName = nameof(ShouldThrowNotFoundExceptionThatCheckAccountByIdAsyncReturnsFalse))]
        [Trait("Unit/UseCase", "Limit - UpdateLimit")]
        public async Task ShouldThrowNotFoundExceptionThatCheckAccountByIdAsyncReturnsFalse()
        {
            var limit = _fixture.MakeLimitEntity();
            _limitRepositoryMock
                .Setup(x => x.FindAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(limit);

            _limitRepositoryMock
                .Setup(x => x.CheckAccountByIdAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            var request = _fixture.MakeUpdateLimitRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<NotFoundException>()
                .Where(x => x.Code == "not-found")
                .WithMessage("Account not found");
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatCheckCategoryByIdAsyncThrows))]
        [Trait("Unit/UseCase", "Limit - UpdateLimit")]
        public async Task ShouldRethrowSameExceptionThatCheckCategoryByIdAsyncThrows()
        {
            var limit = _fixture.MakeLimitEntity();
            _limitRepositoryMock
                .Setup(x => x.FindAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(limit);

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

            var request = _fixture.MakeUpdateLimitRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }

        [Fact(DisplayName = nameof(ShouldThrowNotFoundExceptionIfCheckCategoryByIdAsyncReturnsFalse))]
        [Trait("Unit/UseCase", "Limit - UpdateLimit")]
        public async Task ShouldThrowNotFoundExceptionIfCheckCategoryByIdAsyncReturnsFalse()
        {
            var limit = _fixture.MakeLimitEntity();
            _limitRepositoryMock
                .Setup(x => x.FindAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(limit);

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

            var request = _fixture.MakeUpdateLimitRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<NotFoundException>()
                .Where(x => x.Code == "not-found")
                .WithMessage("Category not found");
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatUpdateAsyncThrows))]
        [Trait("Unit/UseCase", "Limit - UpdateLimit")]
        public async Task ShouldRethrowSameExceptionThatUpdateAsyncThrows()
        {
            var limit = _fixture.MakeLimitEntity();
            _limitRepositoryMock
                .Setup(x => x.FindAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(limit);

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
                .Setup(x => x.UpdateAsync(
                    It.IsAny<LimitEntity>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new UnexpectedException());

            var request = _fixture.MakeUpdateLimitRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatCommitAsyncThrows))]
        [Trait("Unit/UseCase", "Limit - UpdateLimit")]
        public async Task ShouldRethrowSameExceptionThatCommitAsyncThrows()
        {
            var limit = _fixture.MakeLimitEntity();
            _limitRepositoryMock
                .Setup(x => x.FindAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(limit);

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

            var request = _fixture.MakeUpdateLimitRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }
    }
}
