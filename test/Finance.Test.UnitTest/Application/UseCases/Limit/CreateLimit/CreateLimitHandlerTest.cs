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

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatCheckAccountByIdAsyncThrows))]
        [Trait("Unit/UseCase", "Limit - CreateLimit")]
        public async Task ShouldRethrowSameExceptionThatCheckAccountByIdAsyncThrows()
        {
            _limitRepositoryMock
                .Setup(x => x.CheckAccountByIdAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new UnexpectedException());

            var request = _fixture.MakeCreateLimitRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatCheckAccountByIdAsyncThrows))]
        [Trait("Unit/UseCase", "Limit - CreateLimit")]
        public async Task ShouldThrowNotFoundExceptionIfCheckAccountByIdAsyncReturnsFalse()
        {
            _limitRepositoryMock
                .Setup(x => x.CheckAccountByIdAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            var request = _fixture.MakeCreateLimitRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<NotFoundException>()
                .Where(x => x.Code == "not-found")
                .WithMessage("Account not found");
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatInsertAsyncThrows))]
        [Trait("Unit/UseCase", "Limit - CreateLimit")]
        public async Task ShouldRethrowSameExceptionThatInsertAsyncThrows()
        {
            _limitRepositoryMock
                .Setup(x => x.CheckAccountByIdAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

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
