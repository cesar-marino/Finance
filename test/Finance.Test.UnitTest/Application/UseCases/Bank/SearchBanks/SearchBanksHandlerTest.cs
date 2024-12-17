using Finance.Application.UseCases.Bank.SearchBanks;
using Finance.Domain.Exceptions;
using Finance.Domain.Repositories;
using Finance.Domain.SeedWork;
using FluentAssertions;
using Moq;

namespace Finance.Test.UnitTest.Application.UseCases.Bank.SearchBanks
{
    public class SearchBanksHandlerTest : IClassFixture<SearchBanksHandlerTestFixture>
    {
        private readonly SearchBanksHandlerTestFixture _fixture;
        private readonly SearchBanksHandler _sut;
        private readonly Mock<IBankRepository> _bankRepositoryMock;

        public SearchBanksHandlerTest(SearchBanksHandlerTestFixture fixture)
        {
            _fixture = fixture;
            _bankRepositoryMock = new();
            _sut = new(bankRepository: _bankRepositoryMock.Object);
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatSearchAsyncThrows))]
        [Trait("Unit/UseCase", "Bank - SearchBanks")]
        public async void ShouldRethrowSameExceptionThatSearchAsyncThrows()
        {
            _bankRepositoryMock
                .Setup(x => x.SearchAsync(
                    It.IsAny<bool?>(),
                    It.IsAny<string?>(),
                    It.IsAny<string?>(),
                    It.IsAny<int?>(),
                    It.IsAny<int?>(),
                    It.IsAny<string?>(),
                    It.IsAny<SearchOrder?>()))
                .ThrowsAsync(new UnexpectedException());

            var request = _fixture.MakeSearchBanksRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }
    }
}