using Finance.Application.UseCases.Bank.GetBank;
using Finance.Domain.Exceptions;
using Finance.Domain.Repositories;
using Finance.Domain.SeedWork;
using FluentAssertions;
using Moq;

namespace Finance.Test.UnitTest.Application.UseCases.Bank.GetBank
{
    public class GetBankHandlerTest : IClassFixture<GetBankHandlerTestFixture>
    {
        private readonly GetBankHandlerTestFixture _fixture;
        private readonly GetBankHandler _sut;
        private readonly Mock<IBankRepository> _bankRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public GetBankHandlerTest(GetBankHandlerTestFixture fixture)
        {
            _fixture = fixture;
            _bankRepositoryMock = new();
            _unitOfWorkMock = new();

            _sut = new(bankRepository: _bankRepositoryMock.Object);
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatFindAsyncThrows))]
        [Trait("Unit/UseCase", "Bank - GetBank")]
        public async Task ShouldRethrowSameExceptionThatFindAsyncThrows()
        {
            _bankRepositoryMock
                .Setup(x => x.FindAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new NotFoundException("Bank"));

            var request = _fixture.MakeGetBankRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<NotFoundException>()
                .Where(x => x.Code == "not-found")
                .WithMessage("Bank not found");
        }

        [Fact(DisplayName = nameof(ShouldReturnTheCorrectResponseIfBankIsFound))]
        [Trait("Unit/UseCase", "Bank - GetBank")]
        public async Task ShouldReturnTheCorrectResponseIfBankIsFound()
        {
            var bank = _fixture.MakeBankEntity();
            _bankRepositoryMock
               .Setup(x => x.FindAsync(
                   It.IsAny<Guid>(),
                   It.IsAny<CancellationToken>()))
               .ReturnsAsync(bank);

            var request = _fixture.MakeGetBankRequest();
            var response = await _sut.Handle(request, _fixture.CancellationToken);

            response.Active.Should().Be(bank.Active);
            response.BankId.Should().Be(bank.Id);
            response.Code.Should().Be(bank.Code);
            response.Color.Should().Be(bank.Color);
            response.CreatedAt.Should().Be(bank.CreatedAt);
            response.Logo.Should().Be(bank.Logo);
            response.Name.Should().Be(bank.Name);
        }
    }
}