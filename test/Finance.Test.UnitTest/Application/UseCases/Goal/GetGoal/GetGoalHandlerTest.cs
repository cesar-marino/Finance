using Finance.Application.UseCases.Goal.GetGoal;
using Finance.Domain.Exceptions;
using Finance.Domain.Repositories;
using FluentAssertions;
using Moq;

namespace Finance.Test.UnitTest.Application.UseCases.Goal.GetGoal
{
    public class GetGoalHandlerTest : IClassFixture<GetGoalHandlerTestFixture>
    {
        private readonly GetGoalHandlerTestFixture _fixture;
        private readonly GetGoalHandler _sut;
        private readonly Mock<IGoalRepository> _goalRepositoryMock;

        public GetGoalHandlerTest(GetGoalHandlerTestFixture fixture)
        {
            _fixture = fixture;
            _goalRepositoryMock = new();

            _sut = new(goalRepository: _goalRepositoryMock.Object);
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatFindAsyncThrows))]
        [Trait("Unit/UseCase", "Goal - GetGoal")]
        public async Task ShouldRethrowSameExceptionThatFindAsyncThrows()
        {
            _goalRepositoryMock
                .Setup(x => x.FindAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new NotFoundException("Goal"));

            var request = _fixture.MakeGetGoalRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<NotFoundException>()
                .Where(x => x.Code == "not-found")
                .WithMessage("Goal not found");
        }

        [Fact(DisplayName = nameof(ShouldReturnTheCorrectResponseIfGoalIsFound))]
        [Trait("Unit/UseCase", "Goal - GetGoal")]
        public async Task ShouldReturnTheCorrectResponseIfGoalIsFound()
        {
            var goal = _fixture.MakeGoalEntity();
            _goalRepositoryMock
                .Setup(x => x.FindAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(goal);

            var request = _fixture.MakeGetGoalRequest();
            var response = await _sut.Handle(request, _fixture.CancellationToken);

            response.AccountId.Should().Be(response.AccountId);
            response.CreatedAt.Should().Be(response.CreatedAt);
            response.CurrentAmount.Should().Be(response.CurrentAmount);
            response.ExpectedAmount.Should().Be(response.ExpectedAmount);
            response.GoalId.Should().Be(response.GoalId);
            response.Name.Should().Be(response.Name);
            response.UpdatedAt.Should().Be(response.UpdatedAt);
        }
    }
}