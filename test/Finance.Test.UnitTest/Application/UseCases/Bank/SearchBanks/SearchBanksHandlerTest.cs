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

        [Fact(DisplayName = nameof(ShouldReturnTheCorrectResponseIfTheSearchIsSuccessful))]
        [Trait("Unit/UseCase", "Bank - SearchBanks")]
        public async void ShouldReturnTheCorrectResponseIfTheSearchIsSuccessful()
        {
            var banks = _fixture.MakeSearchBanksResult();
            _bankRepositoryMock
                .Setup(x => x.SearchAsync(
                    It.IsAny<bool?>(),
                    It.IsAny<string?>(),
                    It.IsAny<string?>(),
                    It.IsAny<int?>(),
                    It.IsAny<int?>(),
                    It.IsAny<string?>(),
                    It.IsAny<SearchOrder?>()))
                .ReturnsAsync(banks);

            var request = _fixture.MakeSearchBanksRequest();
            var response = await _sut.Handle(request, _fixture.CancellationToken);

            response.CurrentPage.Should().Be(banks.CurrentPage);
            response.Order.Should().Be(banks.Order);
            response.OrderBy.Should().Be(banks.OrderBy);
            response.PerPage.Should().Be(banks.PerPage);
            response.Total.Should().Be(banks.Items.Count);
            response.Items.ToList().ForEach((item) =>
            {
                var result = banks.Items.FirstOrDefault(x => x.Id == item.BankId);
                result.Should().NotBeNull();
                result?.Active.Should().Be(item.Active);
                result?.Code.Should().Be(item.Code);
                result?.Color.Should().Be(item.Color);
                result?.CreatedAt.Should().Be(item.CreatedAt);
                result?.Id.Should().Be(item.BankId);
                result?.Logo.Should().Be(item.Logo);
                result?.Name.Should().Be(item.Name);
                result?.UpdatedAt.Should().Be(item.UpdatedAt);
            });
        }
    }
}