using Finance.Application.UseCases.Limit.CreateLimit;
using Finance.Domain.Entities;
using Finance.Domain.Exceptions;
using Finance.Domain.Repositories;
using FluentAssertions;
using Moq;

namespace Finance.Test.UnitTest.Application.UseCases.Limit.CreateLimit
{
    public class CreateLimitHandlerTest : IClassFixture<CreateLimitHandlerTestFixture>
    {
        private readonly CreateLimitHandlerTestFixture _fixture;
        private readonly CreateLimitHandler _sut;
        private readonly Mock<ILimitRepository> _limitRepositoryMock;

        public CreateLimitHandlerTest(CreateLimitHandlerTestFixture fixture)
        {
            _fixture = fixture;
            _limitRepositoryMock = new();

            _sut = new(
                limitRepository: _limitRepositoryMock.Object);
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatInsertAsynccThrows))]
        [Trait("Unit/UseCase", "Limit - CreateLimit")]
        public async Task ShouldRethrowSameExceptionThatInsertAsynccThrows()
        {
            _limitRepositoryMock
                .Setup(x => x.InsertAsync(
                    It.IsAny<LimitEntity>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new UnexpectedException());

            var request = _fixture.MakeCreateLimitRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }
    }
}
