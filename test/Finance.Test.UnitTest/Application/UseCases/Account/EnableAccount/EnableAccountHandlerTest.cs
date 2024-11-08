using Finance.Application.UseCases.Account.EnableAccount;
using Finance.Domain.Entities;
using Finance.Domain.Exceptions;
using Finance.Domain.Repositories;
using FluentAssertions;
using Moq;

namespace Finance.Test.UnitTest.Application.UseCases.Account.EnableAccount
{
    public class EnableAccountHandlerTest : IClassFixture<EnableAccountHandlerTestFixture>
    {
        private readonly EnableAccountHandlerTestFixture _fixture;
        private readonly EnableAccountHandler _sut;
        private readonly Mock<IAccountRepository> _accountRepositoryMock;

        public EnableAccountHandlerTest(EnableAccountHandlerTestFixture fixture)
        {
            _fixture = fixture;
            _accountRepositoryMock = new();

            _sut = new(accountRepository: _accountRepositoryMock.Object);
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatFindAsyncThrows))]
        [Trait("Unit/UseCase", "Account - EnableAccount")]
        public async Task ShouldRethrowSameExceptionThatFindAsyncThrows()
        {
            _accountRepositoryMock
                .Setup(x => x.FindAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new NotFoundException("Account"));

            var request = _fixture.MakeEnableAccountRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<NotFoundException>()
                .Where(x => x.Code == "not-found")
                .WithMessage("Account not found");
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatUpdateAsyncThrows))]
        [Trait("Unit/UseCase", "Account - EnableAccount")]
        public async Task ShouldRethrowSameExceptionThatUpdateAsyncThrows()
        {
            var account = _fixture.MakeAccountEntity(active: false);
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

            var request = _fixture.MakeEnableAccountRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }
    }
}