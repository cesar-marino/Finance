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
        private readonly CreateBankHandlerTestFixture _fixture;
        private readonly CreateBankHandler _sut;
        private readonly Mock<IBankRepository> _bankRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

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
        public async Task ShouldRethrowSameExceptionThatInsertAsyncThrows()
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
        public async Task ShouldRethrowSameExceptionThatCommitAsyncThrows()
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

        [Fact(DisplayName = nameof(ShouldReturnTheCorrectResponseIfBankIsSuccessfullyCreated))]
        [Trait("Unit/UseCase", "Bank - CreateBank")]
        public async Task ShouldReturnTheCorrectResponseIfBankIsSuccessfullyCreated()
        {
            var request = _fixture.MakeCreateBankRequest();
            var response = await _sut.Handle(request, _fixture.CancellationToken);

            response.Active.Should().BeTrue();
            response.Code.Should().Be(request.Code);
            response.Color.Should().Be(request.Color);
            response.Logo.Should().Be(request.Logo);
            response.Name.Should().Be(request.Name);
        }
    }
}