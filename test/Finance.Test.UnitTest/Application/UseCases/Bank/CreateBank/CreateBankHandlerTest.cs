using Finance.Application.Services;
using Finance.Application.UseCases.Bank.CreateBank;
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
        private readonly Mock<IStorageService> _storageServiceMock;
        private readonly Mock<IBankRepository> _bankRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public CreateBankHandlerTest(CreateBankHandlerTestFixture fixture)
        {
            _fixture = fixture;
            _bankRepositoryMock = new();
            _unitOfWorkMock = new();
            _storageServiceMock = new();

            _sut = new(storageService: _storageServiceMock.Object);
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatUploadAsyncThrows))]
        [Trait("Unit/UseCase", "Bank - CreateBank")]
        public async Task ShouldRethrowSameExceptionThatUploadAsyncThrows()
        {
            _storageServiceMock
                .Setup(x => x.UploadAsync(
                    It.IsAny<string>(),
                    It.IsAny<byte[]>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new UnexpectedException());

            var request = _fixture.MakeCreateBankRequest(_fixture.Faker.Random.Bytes(50000));
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }
    }
}