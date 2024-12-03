using Finance.Application.UseCases.Goal.SearchGoals;
using Finance.Domain.Exceptions;
using Finance.Domain.Repositories;
using Finance.Domain.SeedWork;
using FluentAssertions;
using Moq;

namespace Finance.Test.UnitTest.Application.UseCases.Goal.SearchGoals
{
    public class SearchGoalsHandlerTest : IClassFixture<SearchGoalsHandlerTestFixture>
    {
        private readonly SearchGoalsHandlerTestFixture _fixture;
        private readonly SearchGoalsHandler _sut;
        private readonly Mock<IGoalRepository> _goalRepositoryMock;

        public SearchGoalsHandlerTest(SearchGoalsHandlerTestFixture fixture)
        {
            _fixture = fixture;
            _goalRepositoryMock = new();

            _sut = new(goalRepository: _goalRepositoryMock.Object);
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatSearchAsyncThrows))]
        [Trait("Unit/UseCase", "Goal - SearchGoals")]
        public async Task ShouldRethrowSameExceptionThatSearchAsyncThrows()
        {
            _goalRepositoryMock
                .Setup(x => x.SearchAsync(
                    It.IsAny<string?>(),
                    It.IsAny<int?>(),
                    It.IsAny<int?>(),
                    It.IsAny<string?>(),
                    It.IsAny<SearchOrder?>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new UnexpectedException());

            var request = _fixture.MakeSearchGoalsRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }
    }
}