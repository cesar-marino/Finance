using Bogus;
using Finance.Application.UseCases.Goal.UpdateGoal;
using Finance.Domain.Entities;
using Finance.Domain.Exceptions;
using Finance.Domain.Repositories;
using Finance.Domain.SeedWork;
using FluentAssertions;
using Moq;

namespace Finance.Test.UnitTest.Application.UseCases.Goal.UpdateGoal
{
    public class UpdateGoalHandlerTest : IClassFixture<UpdateGoalHandlerTestFixture>
    {
        private readonly UpdateGoalHandlerTestFixture _fixture;
        private readonly UpdateGoalHandler _sut;
        private readonly Mock<IGoalRepository> _goalRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public UpdateGoalHandlerTest(UpdateGoalHandlerTestFixture fixture)
        {
            _fixture = fixture;
            _goalRepositoryMock = new();
            _unitOfWorkMock = new();

            _sut = new(
                goalRepository: _goalRepositoryMock.Object,
                unitOfWork: _unitOfWorkMock.Object);
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatFindAsycnThrows))]
        [Trait("Unit/UseCase", "Goal - UpdateGoal")]
        public async Task ShouldRethrowSameExceptionThatFindAsycnThrows()
        {
            _goalRepositoryMock
                .Setup(x => x.FindAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new NotFoundException("Goal"));

            var request = _fixture.MakeUpdateGoalRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<NotFoundException>()
                .Where(x => x.Code == "not-found")
                .WithMessage("Goal not found");
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatUpdateAsycnThrows))]
        [Trait("Unit/UseCase", "Goal - UpdateGoal")]
        public async Task ShouldRethrowSameExceptionThatUpdateAsycnThrows()
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

            var request = _fixture.MakeUpdateGoalRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatCommitAsycnThrows))]
        [Trait("Unit/UseCase", "Goal - UpdateGoal")]
        public async Task ShouldRethrowSameExceptionThatCommitAsycnThrows()
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

            var request = _fixture.MakeUpdateGoalRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }

        [Fact(DisplayName = nameof(ShouldReturnTheCorrectResponseIfGoalIsSuccessfullyUpdated))]
        [Trait("Unit/UseCase", "Goal - UpdateGoal")]
        public async Task ShouldReturnTheCorrectResponseIfGoalIsSuccessfullyUpdated()
        {
            var goal = _fixture.MakeGoalEntity();
            _goalRepositoryMock
                .Setup(x => x.FindAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(goal);

            var request = _fixture.MakeUpdateGoalRequest();
            var response = await _sut.Handle(request, _fixture.CancellationToken);

            response.AccountId.Should().Be(response.AccountId);
            response.CreatedAt.Should().Be(response.CreatedAt);
            response.CurrentAmount.Should().Be(response.CurrentAmount);
            response.ExpectedAmount.Should().Be(request.ExpectedAmount);
            response.GoalId.Should().Be(response.GoalId);
            response.Name.Should().Be(request.Name);
        }
    }
}