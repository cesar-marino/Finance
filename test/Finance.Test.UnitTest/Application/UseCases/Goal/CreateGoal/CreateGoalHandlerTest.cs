using Finance.Application.UseCases.Goal.CreateGoal;
using Finance.Domain.Entities;
using Finance.Domain.Exceptions;
using Finance.Domain.Repositories;
using Finance.Domain.SeedWork;
using FluentAssertions;
using Moq;

namespace Finance.Test.UnitTest.Application.UseCases.Goal.CreateGoal
{
    public class CreateGoalHandlerTest : IClassFixture<CreateGoalHandlerTestFixture>
    {
        private readonly CreateGoalHandlerTestFixture _fixture;
        private readonly CreateGoalHandler _sut;
        private readonly Mock<IGoalRepository> _goalRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public CreateGoalHandlerTest(CreateGoalHandlerTestFixture fixture)
        {
            _fixture = fixture;
            _goalRepositoryMock = new();
            _unitOfWorkMock = new();

            _sut = new(
                goalRepository: _goalRepositoryMock.Object,
                unitOfWork: _unitOfWorkMock.Object);
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatCheckAccountAsyncThrows))]
        [Trait("Unit/UseCase", "Goal - CreateGoal")]
        public async Task ShouldRethrowSameExceptionThatCheckAccountAsyncThrows()
        {
            _goalRepositoryMock
                .Setup(x => x.CheckAccountAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new UnexpectedException());

            var request = _fixture.MakeCreateGoalRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }

        [Fact(DisplayName = nameof(ShouldThrowNotFoundExceptionIfCheckAccountAsyncReturnsFalse))]
        [Trait("Unit/UseCase", "Goal - CreateGoal")]
        public async Task ShouldThrowNotFoundExceptionIfCheckAccountAsyncReturnsFalse()
        {
            _goalRepositoryMock
                .Setup(x => x.CheckAccountAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            var request = _fixture.MakeCreateGoalRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<NotFoundException>()
                .Where(x => x.Code == "not-found")
                .WithMessage("Account not found");
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatInsertAsyncThrows))]
        [Trait("Unit/UseCase", "Goal - CreateGoal")]
        public async Task ShouldRethrowSameExceptionThatInsertAsyncThrows()
        {
            _goalRepositoryMock
                .Setup(x => x.CheckAccountAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            _goalRepositoryMock
                .Setup(x => x.InsertAsync(
                    It.IsAny<GoalEntity>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new UnexpectedException());

            var request = _fixture.MakeCreateGoalRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatCommitAsyncThrows))]
        [Trait("Unit/UseCase", "Goal - CreateGoal")]
        public async Task ShouldRethrowSameExceptionThatCommitAsyncThrows()
        {
            _goalRepositoryMock
                .Setup(x => x.CheckAccountAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            _unitOfWorkMock
                .Setup(x => x.CommitAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(new UnexpectedException());

            var request = _fixture.MakeCreateGoalRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }

        [Fact(DisplayName = nameof(ShouldReturnTheCorrectResponseIfGoalIsSuccessfullyCreated))]
        [Trait("Unit/UseCase", "Goal - CreateGoal")]
        public async Task ShouldReturnTheCorrectResponseIfGoalIsSuccessfullyCreated()
        {
            _goalRepositoryMock
                .Setup(x => x.CheckAccountAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            var request = _fixture.MakeCreateGoalRequest();
            var response = await _sut.Handle(request, _fixture.CancellationToken);

            response.AccountId.Should().Be(request.AccountId);
            response.CurrentAmount.Should().Be(0);
            response.ExpectedAmount.Should().Be(request.ExpectedAmount);
            response.Name.Should().Be(request.Name);
        }
    }
}