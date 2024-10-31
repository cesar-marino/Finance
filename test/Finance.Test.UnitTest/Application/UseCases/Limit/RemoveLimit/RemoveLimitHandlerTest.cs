using Finance.Application.UseCases.Limit.RemoveLimit;
using Finance.Domain.Exceptions;
using Finance.Domain.Repositories;
using FluentAssertions;
using Moq;

namespace Finance.Test.UnitTest.Application.UseCases.Limit.RemoveLimit
{
    public class RemoveLimitHandlerTest : IClassFixture<RemoveLimitHandlerTestFixture>
    {
        private readonly RemoveLimitHandlerTestFixture _fixture;
        private readonly RemoveLimitHandler _sut;
        private readonly Mock<ILimitRepository> _limitRepositoryMock;

        public RemoveLimitHandlerTest(RemoveLimitHandlerTestFixture fixture)
        {
            _fixture = fixture;
            _limitRepositoryMock = new();

            _sut = new(
                limitRepository: _limitRepositoryMock.Object);
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatRemoveAsyncThrows))]
        [Trait("Unit/UseCase", "Limit - RemoveLimit")]
        public async Task ShouldRethrowSameExceptionThatRemoveAsyncThrows()
        {
            _limitRepositoryMock
                .Setup(x => x.RemoveAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new UnexpectedException());

            var request = _fixture.MakeRemoveLimitRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }
    }
}
