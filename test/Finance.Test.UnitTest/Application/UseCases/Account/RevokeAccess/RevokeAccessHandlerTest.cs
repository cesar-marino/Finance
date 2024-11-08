using Finance.Application.UseCases.Account.RevokeAccess;
using Finance.Domain.Exceptions;
using Finance.Domain.Repositories;
using FluentAssertions;
using Moq;

namespace Finance.Test.UnitTest.Application.UseCases.Account.RevokeAccess
{
    public class RevokeAccessHandlerTest : IClassFixture<RevokeAccessHandlerTestFixture>
    {
        private readonly RevokeAccessHandlerTestFixture _fixture;
        private readonly RevokeAccessHandler _sut;
        private readonly Mock<IAccountRepository> _accountRepositoryMock;

        public RevokeAccessHandlerTest(RevokeAccessHandlerTestFixture fixture)
        {
            _fixture = fixture;
            _accountRepositoryMock = new();

            _sut = new(
                accountRepository: _accountRepositoryMock.Object);
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatFindAsyncThrows))]
        [Trait("Unit/UseCase", "Account - RevokeAccess")]
        public async Task ShouldRethrowSameExceptionThatFindAsyncThrows()
        {
            _accountRepositoryMock
                .Setup(x => x.FindAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new NotFoundException("Account"));

            var request = _fixture.MakeRevokeAccessRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<NotFoundException>()
                .Where(x => x.Code == "not-found")
                .WithMessage("Account not found");
        }
    }
}