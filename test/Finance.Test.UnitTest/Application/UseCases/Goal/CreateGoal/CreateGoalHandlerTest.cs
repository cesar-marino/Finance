using Finance.Application.UseCases.Goal.CreateGoal;
using Finance.Domain.Exceptions;
using Finance.Domain.Repositories;
using FluentAssertions;
using Moq;

namespace Finance.Test.UnitTest.Application.UseCases.Goal.CreateGoal
{
    public class CreateGoalHandlerTest : IClassFixture<CreateGoalHandlerTestFixture>
    {
        private readonly CreateGoalHandlerTestFixture _fixture;
        private readonly CreateGoalHandler _sut;
        private readonly Mock<IGoalRepository> _goalRepositoryMock;

        public CreateGoalHandlerTest(CreateGoalHandlerTestFixture fixture)
        {
            _fixture = fixture;
            _goalRepositoryMock = new();

            _sut = new(goalRepository: _goalRepositoryMock.Object);
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
    }
}