using Bogus;
using Finance.Application.UseCases.Goal.UpdateGoal;
using Finance.Domain.Exceptions;
using Finance.Domain.Repositories;
using FluentAssertions;
using Moq;

namespace Finance.Test.UnitTest.Application.UseCases.Goal.UpdateGoal
{
    public class UpdateGoalHandlerTest : IClassFixture<UpdateGoalHandlerTestFixture>
    {
        private readonly UpdateGoalHandlerTestFixture _fixture;
        private readonly UpdateGoalHandler _sut;
        private readonly Mock<IGoalRepository> _goalRepositoryMock;

        public UpdateGoalHandlerTest(UpdateGoalHandlerTestFixture fixture)
        {
            _fixture = fixture;
            _goalRepositoryMock = new();

            _sut = new(goalRepository: _goalRepositoryMock.Object);
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

    }
}