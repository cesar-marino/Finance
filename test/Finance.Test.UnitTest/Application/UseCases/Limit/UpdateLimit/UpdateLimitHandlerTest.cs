using Finance.Application.UseCases.Limit.UpdateLimit;
using Finance.Domain.Exceptions;
using Finance.Domain.Repositories;
using FluentAssertions;
using Moq;

namespace Finance.Test.UnitTest.Application.UseCases.Limit.UpdateLimit
{
    public class UpdateLimitHandlerTest : IClassFixture<UpdateLimitHandlerTestFixture>
    {
        private readonly UpdateLimitHandlerTestFixture _fixture;
        private readonly UpdateLimitHandler _sut;
        private readonly Mock<ILimitRepository> _limitRepositoryMock;

        public UpdateLimitHandlerTest(UpdateLimitHandlerTestFixture fixture)
        {
            _fixture = fixture;
            _limitRepositoryMock = new();

            _sut = new(
                limitRepository: _limitRepositoryMock.Object);
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatFindAsyncThrows))]
        [Trait("Unit/UseCase", "Limit - UpdateLimit")]
        public async Task ShouldRethrowSameExceptionThatFindAsyncThrows()
        {
            _limitRepositoryMock
                .Setup(x => x.FindAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new NotFoundException("Limit"));

            var request = _fixture.MakeUpdateLimitRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<NotFoundException>()
                .Where(x => x.Code == "not-found")
                .WithMessage("Limit not found");
        }
    }
}
