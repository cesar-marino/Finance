using Finance.Application.UseCases.Goal.RemoveAmount;
using Finance.Domain.Entities;
using Finance.Domain.Exceptions;
using Finance.Domain.Repositories;
using Finance.Domain.SeedWork;
using FluentAssertions;
using Moq;

namespace Finance.Test.UnitTest.Application.UseCases.Goal.RemoveAmount
{
    public class RemoveAmountHandlerTest : IClassFixture<RemoveAmountHandlerTestFixture>
    {
        private readonly RemoveAmountHandlerTestFixture _fixture;
        private readonly RemoveAmountHandler _sut;
        private readonly Mock<IGoalRepository> _goalRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public RemoveAmountHandlerTest(RemoveAmountHandlerTestFixture fixture)
        {
            _fixture = fixture;
            _goalRepositoryMock = new();
            _unitOfWorkMock = new();

            _sut = new(
                goalRepository: _goalRepositoryMock.Object,
                unitOfWork: _unitOfWorkMock.Object);
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatFindAsyncThrows))]
        [Trait("Unit/UseCase", "Goal - RemoveAmount")]
        public async Task ShouldRethrowSameExceptionThatFindAsyncThrows()
        {
            _goalRepositoryMock
                .Setup(x => x.FindAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new NotFoundException("Goal"));

            var request = _fixture.MakeRemoveAmountRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<NotFoundException>()
                .Where(x => x.Code == "not-found")
                .WithMessage("Goal not found");
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatUpdateAsyncThrows))]
        [Trait("Unit/UseCase", "Goal - RemoveAmount")]
        public async Task ShouldRethrowSameExceptionThatUpdateAsyncThrows()
        {
            var goal = _fixture.MakeGoalEntity();
            _goalRepositoryMock
                .Setup(x => x.FindAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(goal);

            _goalRepositoryMock
                .Setup(x => x.UpdateAsync(
                    It.IsAny<GoalEntity>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new UnexpectedException());

            var request = _fixture.MakeRemoveAmountRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatCommitAsyncThrows))]
        [Trait("Unit/UseCase", "Goal - RemoveAmount")]
        public async Task ShouldRethrowSameExceptionThatCommitAsyncThrows()
        {
            var goal = _fixture.MakeGoalEntity();
            _goalRepositoryMock
                .Setup(x => x.FindAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(goal);

            _unitOfWorkMock
                .Setup(x => x.CommitAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(new UnexpectedException());

            var request = _fixture.MakeRemoveAmountRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }

        [Fact(DisplayName = nameof(ShouldReturnTheCorrectResponseIfAmountIsSuccessfullyRemoved))]
        [Trait("Unit/UseCase", "Goal - RemoveAmount")]
        public async Task ShouldReturnTheCorrectResponseIfAmountIsSuccessfullyRemoved()
        {
            var goal = _fixture.MakeGoalEntity();
            _goalRepositoryMock
                .Setup(x => x.FindAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(goal);

            var currentAmount = goal.CurrentAmount;

            var request = _fixture.MakeRemoveAmountRequest();
            var response = await _sut.Handle(request, _fixture.CancellationToken);

            response.UserId.Should().Be(goal.UserId);
            response.CreatedAt.Should().Be(goal.CreatedAt);
            response.ExpectedAmount.Should().Be(goal.ExpectedAmount);
            response.GoalId.Should().Be(goal.Id);
            response.Name.Should().Be(goal.Name);
            response.CurrentAmount.Should().Be(currentAmount - request.Amount);
        }
    }
}