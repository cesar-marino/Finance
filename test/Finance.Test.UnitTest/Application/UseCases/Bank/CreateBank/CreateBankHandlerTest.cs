using Finance.Application.UseCases.Bank.CreateBank;
using Finance.Domain.Entities;
using Finance.Domain.Exceptions;
using Finance.Domain.Repositories;
using Finance.Domain.SeedWork;
using FluentAssertions;
using Moq;

namespace Finance.Test.UnitTest.Application.UseCases.Bank.CreateBank
{
    public class CreateBankHandlerTest : IClassFixture<CreateBankHandlerTestFixture>
    {
        private CreateBankHandlerTestFixture _fixture;
        private CreateBankHandler _sut;
        private Mock<IBankRepository> _bankRepositoryMock;
        private Mock<IUnitOfWork> _unitOfWorkMock;

        public CreateBankHandlerTest(CreateBankHandlerTestFixture fixture)
        {
            _fixture = fixture;
            _bankRepositoryMock = new();
            _unitOfWorkMock = new();

            _sut = new(
                bankRepository: _bankRepositoryMock.Object,
                unitOfWork: _unitOfWorkMock.Object);
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatInsertAsyncThrows))]
        [Trait("Unit/UseCase", "Bank - CreateBank")]
        public async void ShouldRethrowSameExceptionThatInsertAsyncThrows()
        {
            _bankRepositoryMock
                .Setup(x => x.InsertAsync(
                    It.IsAny<BankEntity>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new UnexpectedException());

            var request = _fixture.MakeCreateBankRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatCommitAsyncThrows))]
        [Trait("Unit/UseCase", "Bank - CreateBank")]
        public async void ShouldRethrowSameExceptionThatCommitAsyncThrows()
        {
            _unitOfWorkMock
                .Setup(x => x.CommitAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(new UnexpectedException());

            var request = _fixture.MakeCreateBankRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }
    }
}