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

            _sut = new();
        }
    }
}