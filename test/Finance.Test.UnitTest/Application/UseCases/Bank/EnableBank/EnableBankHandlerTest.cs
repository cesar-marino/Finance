using Finance.Application.UseCases.Bank.EnableBank;
using Finance.Domain.Exceptions;
using Finance.Domain.Repositories;
using FluentAssertions;
using Moq;

namespace Finance.Test.UnitTest.Application.UseCases.Bank.EnableBank
{
    public class EnableBankHandlerTest : IClassFixture<EnableBankHandlerTestFixture>
    {
        private readonly EnableBankHandlerTestFixture _fixture;
        private readonly EnableBankHandler _sut;
        private readonly Mock<IBankRepository> _bankRepositoryMock;

        public EnableBankHandlerTest(EnableBankHandlerTestFixture fixture)
        {
            _fixture = fixture;
            _bankRepositoryMock = new();
            _sut = new(bankRepository: _bankRepositoryMock.Object);
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatFindAsyncThrows))]
        [Trait("Unit/UseCase", "Bank - EnableBank")]
        public async void ShouldRethrowSameExceptionThatFindAsyncThrows()
        {
            _bankRepositoryMock
                .Setup(x => x.FindAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new NotFoundException("Bank"));

            var request = _fixture.MakeEnableBankRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<NotFoundException>()
                .Where(x => x.Code == "not-found")
                .WithMessage("Bank not found");
        }

    }
}