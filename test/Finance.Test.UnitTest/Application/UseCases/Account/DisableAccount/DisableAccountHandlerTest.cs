using Finance.Application.UseCases.Account.DisableAccount;
using Finance.Domain.Exceptions;
using Finance.Domain.Repositories;
using FluentAssertions;
using Moq;

namespace Finance.Test.UnitTest.Application.UseCases.Account.DisableAccount
{
    public class DisableAccountHandlerTest : IClassFixture<DisableAccountHandlerTestFixture>
    {
        private readonly DisableAccountHandlerTestFixture _fixture;
        private readonly DisableAccountHandler _sut;
        private readonly Mock<IAccountRepository> _accountRepositoryMock;

        public DisableAccountHandlerTest(DisableAccountHandlerTestFixture fixture)
        {
            _fixture = fixture;
            _accountRepositoryMock = new();

            _sut = new(accountRepository: _accountRepositoryMock.Object);
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
    }
}