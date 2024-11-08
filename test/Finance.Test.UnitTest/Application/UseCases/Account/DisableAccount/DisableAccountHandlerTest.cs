using Finance.Application.UseCases.Account.DisableAccount;
using Finance.Domain.Entities;
using Finance.Domain.Exceptions;
using Finance.Domain.Repositories;
using Finance.Domain.SeedWork;
using FluentAssertions;
using Moq;

namespace Finance.Test.UnitTest.Application.UseCases.Account.DisableAccount
{
    public class DisableAccountHandlerTest : IClassFixture<DisableAccountHandlerTestFixture>
    {
        private readonly DisableAccountHandlerTestFixture _fixture;
        private readonly DisableAccountHandler _sut;
        private readonly Mock<IAccountRepository> _accountRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public DisableAccountHandlerTest(DisableAccountHandlerTestFixture fixture)
        {
            _fixture = fixture;
            _accountRepositoryMock = new();
            _unitOfWorkMock = new();

            _sut = new(
                accountRepository: _accountRepositoryMock.Object,
                unitOfWork: _unitOfWorkMock.Object);
        }

        [Fact(DisplayName = nameof(ShouldRethorwSameExceptionThatFindAsyncThrows))]
        [Trait("Unit/UseCase", "Account - DisableAccount")]
        public async void ShouldRethorwSameExceptionThatFindAsyncThrows()
        {
            _accountRepositoryMock
                .Setup(x => x.FindAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new NotFoundException("Account"));

            var request = _fixture.MakeDisableAccountRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<NotFoundException>()
                .Where(x => x.Code == "not-found")
                .WithMessage("Account not found");
        }

        [Fact(DisplayName = nameof(ShouldRethorwSameExceptionThatUpdateAsyncThrows))]
        [Trait("Unit/UseCase", "Account - DisableAccount")]
        public async void ShouldRethorwSameExceptionThatUpdateAsyncThrows()
        {
            var account = _fixture.MakeAccountEntity();
            _accountRepositoryMock
                .Setup(x => x.FindAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(account);

            _accountRepositoryMock
                .Setup(x => x.UpdateAsync(
                    It.IsAny<AccountEntity>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new UnexpectedException());

            var request = _fixture.MakeDisableAccountRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }

        [Fact(DisplayName = nameof(ShouldRethorwSameExceptionThatCommitAsyncThrows))]
        [Trait("Unit/UseCase", "Account - DisableAccount")]
        public async void ShouldRethorwSameExceptionThatCommitAsyncThrows()
        {
            var account = _fixture.MakeAccountEntity();
            _accountRepositoryMock
                .Setup(x => x.FindAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(account);

            _unitOfWorkMock
                .Setup(x => x.CommitAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(new UnexpectedException());

            var request = _fixture.MakeDisableAccountRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }
    }
}