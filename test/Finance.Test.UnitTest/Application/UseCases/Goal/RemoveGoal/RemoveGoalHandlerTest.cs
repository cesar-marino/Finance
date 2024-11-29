using Finance.Application.UseCases.Goal.RemoveGoal;
using Finance.Domain.Exceptions;
using Finance.Domain.Repositories;
using Finance.Domain.SeedWork;
using FluentAssertions;
using Moq;

namespace Finance.Test.UnitTest.Application.UseCases.Goal.RemoveGoal
{
    public class RemoveGoalHandlerTest : IClassFixture<RemoveGoalHandlerTestFixture>
    {
        private readonly RemoveGoalHandlerTestFixture _fixture;
        private readonly RemoveGoalHandler _sut;
        private readonly Mock<IGoalRepository> _goalRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public RemoveGoalHandlerTest(RemoveGoalHandlerTestFixture fixture)
        {
            _fixture = fixture;
            _goalRepositoryMock = new();
            _unitOfWorkMock = new();

            _sut = new(
                goalRepository: _goalRepositoryMock.Object,
                unitOfWork: _unitOfWorkMock.Object);
        }

        // [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatFindAsyncThrows))]
        // [Trait("Unit/UseCase", "Goal - RemoveGoal")]
        // public async void ShouldRethrowSameExceptionThatFindAsyncThrows()
        // {
        //     _goalRepositoryMock
        //         .Setup(x => x.FindAsync(
        //             It.IsAny<Guid>(),
        //             It.IsAny<Guid>(),
        //             It.IsAny<CancellationToken>()))
        //         .ThrowsAsync(new NotFoundException("Goal"));

        //     var request = _fixture.MakeRemoveGoalRequest();
        //     var act = () => _sut.Handle(request, _fixture.CancellationToken);

        //     await act.Should().ThrowExactlyAsync<NotFoundException>()
        //         .Where(x => x.Code == "not-found")
        //         .WithMessage("Goal not found");
        // }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatRemoveAsyncThrows))]
        [Trait("Unit/UseCase", "Goal - RemoveGoal")]
        public async void ShouldRethrowSameExceptionThatRemoveAsyncThrows()
        {
            var goal = _fixture.MakeGoalEntity();
            _goalRepositoryMock
                .Setup(x => x.FindAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(goal);

            _goalRepositoryMock
                .Setup(x => x.RemoveAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new UnexpectedException());

            var request = _fixture.MakeRemoveGoalRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatCommitAsyncThrows))]
        [Trait("Unit/UseCase", "Goal - RemoveGoal")]
        public async void ShouldRethrowSameExceptionThatCommitAsyncThrows()
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

            var request = _fixture.MakeRemoveGoalRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }

        [Fact(DisplayName = nameof(ShouldRemoveGoalSuccessfully))]
        [Trait("Unit/UseCase", "Goal - RemoveGoal")]
        public void ShouldRemoveGoalSuccessfully()
        {
            var goal = _fixture.MakeGoalEntity();
            _goalRepositoryMock
                .Setup(x => x.FindAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(goal);

            var request = _fixture.MakeRemoveGoalRequest();
            var response = _sut.Handle(request, _fixture.CancellationToken);

            response.IsCompletedSuccessfully.Should().Be(true);
        }
    }
}