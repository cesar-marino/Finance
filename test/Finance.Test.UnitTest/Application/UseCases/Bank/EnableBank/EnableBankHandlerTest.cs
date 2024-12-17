using Finance.Application.UseCases.Bank.EnableBank;
using Finance.Domain.Entities;
using Finance.Domain.Exceptions;
using Finance.Domain.Repositories;
using Finance.Domain.SeedWork;
using FluentAssertions;
using Moq;

namespace Finance.Test.UnitTest.Application.UseCases.Bank.EnableBank
{
    public class EnableBankHandlerTest : IClassFixture<EnableBankHandlerTestFixture>
    {
        private readonly EnableBankHandlerTestFixture _fixture;
        private readonly EnableBankHandler _sut;
        private readonly Mock<IBankRepository> _bankRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public EnableBankHandlerTest(EnableBankHandlerTestFixture fixture)
        {
            _fixture = fixture;
            _bankRepositoryMock = new();
            _unitOfWorkMock = new();

            _sut = new(
                bankRepository: _bankRepositoryMock.Object,
                unitOfWork: _unitOfWorkMock.Object);
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

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatUpdateAsyncThrows))]
        [Trait("Unit/UseCase", "Bank - EnableBank")]
        public async void ShouldRethrowSameExceptionThatUpdateAsyncThrows()
        {
            var bank = _fixture.MakeBankEntity(active: false);
            _bankRepositoryMock
                .Setup(x => x.FindAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(bank);

            _bankRepositoryMock
                .Setup(x => x.UpdateAsync(
                    It.IsAny<BankEntity>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new UnexpectedException());

            var request = _fixture.MakeEnableBankRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatCommitAsyncThrows))]
        [Trait("Unit/UseCase", "Bank - EnableBank")]
        public async void ShouldRethrowSameExceptionThatCommitAsyncThrows()
        {
            var bank = _fixture.MakeBankEntity(active: false);
            _bankRepositoryMock
                .Setup(x => x.FindAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(bank);

            _unitOfWorkMock
                .Setup(x => x.CommitAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(new UnexpectedException());

            var request = _fixture.MakeEnableBankRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }

        [Fact(DisplayName = nameof(ShouldReturnTheCorrectResponseIfBankIsSuccessfullyEnabled))]
        [Trait("Unit/UseCase", "Bank - EnableBank")]
        public async void ShouldReturnTheCorrectResponseIfBankIsSuccessfullyEnabled()
        {
            var bank = _fixture.MakeBankEntity(active: false);
            _bankRepositoryMock
                .Setup(x => x.FindAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(bank);

            var request = _fixture.MakeEnableBankRequest();
            var response = await _sut.Handle(request, _fixture.CancellationToken);

            response.Active.Should().BeTrue();
            response.BankId.Should().Be(bank.Id);
            response.Code.Should().Be(bank.Code);
            response.Color.Should().Be(bank.Color);
            response.CreatedAt.Should().Be(bank.CreatedAt);
            response.Logo.Should().Be(bank.Logo);
            response.Name.Should().Be(bank.Name);
        }
    }
}