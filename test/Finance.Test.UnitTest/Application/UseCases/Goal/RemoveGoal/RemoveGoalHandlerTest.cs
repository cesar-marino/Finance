using Finance.Application.UseCases.Goal.RemoveGoal;
using Finance.Domain.Exceptions;
using Finance.Domain.Repositories;
using FluentAssertions;
using Moq;

namespace Finance.Test.UnitTest.Application.UseCases.Goal.RemoveGoal
{
    public class RemoveGoalHandlerTest : IClassFixture<RemoveGoalHandlerTestFixture>
    {
        private readonly RemoveGoalHandlerTestFixture _fixture;
        private readonly RemoveGoalHandler _sut;
        private readonly Mock<IGoalRepository> _goalRepositoryMock;

        public RemoveGoalHandlerTest(RemoveGoalHandlerTestFixture fixture)
        {
            _fixture = fixture;
            _goalRepositoryMock = new();

            _sut = new(goalRepository: _goalRepositoryMock.Object);
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatFindAsyncThrows))]
        public async void ShouldRethrowSameExceptionThatFindAsyncThrows()
        {
            _goalRepositoryMock
                .Setup(x => x.FindAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new NotFoundException("Goal"));

            var request = _fixture.MakeRemoveGoalRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<NotFoundException>()
                .Where(x => x.Code == "not-found")
                .WithMessage("Goal not found");
        }
    }
}