using Finance.Application.UseCases.Goal.AddAmount;
using Finance.Domain.Entities;
using Finance.Domain.Exceptions;
using Finance.Domain.Repositories;
using Finance.Domain.SeedWork;
using FluentAssertions;
using Moq;

namespace Finance.Test.UnitTest.Application.UseCases.Goal.AddAmount
{
    public class AddAmountHandlerTest : IClassFixture<AddAmountHandlerTestFixture>
    {
        private readonly AddAmountHandlerTestFixture _fixture;
        private readonly AddAmountHandler _sut;
        private readonly Mock<IGoalRepository> _goalRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public AddAmountHandlerTest(AddAmountHandlerTestFixture fixture)
        {
            _fixture = fixture;
            _goalRepositoryMock = new();
            _unitOfWorkMock = new();

            _sut = new(
                goalRepository: _goalRepositoryMock.Object,
                unitOfWork: _unitOfWorkMock.Object);
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatFindAsyncThrows))]
        [Trait("Unit/UseCase", "Goal - AddAmount")]
        public async Task ShouldRethrowSameExceptionThatFindAsyncThrows()
        {
            _goalRepositoryMock
                .Setup(x => x.FindAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new NotFoundException("Goal"));

            var request = _fixture.MakeAddAmountRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<NotFoundException>()
                .Where(x => x.Code == "not-found")
                .WithMessage("Goal not found");
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatUpdateAsyncThrows))]
        [Trait("Unit/UseCase", "Goal - AddAmount")]
        public async Task ShouldRethrowSameExceptionThatUpdateAsyncThrows()
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

            var request = _fixture.MakeAddAmountRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }
    }
}