using Finance.Application.UseCases.Account.RevokeAllAccess;
using Finance.Domain.Exceptions;
using Finance.Domain.Repositories;
using FluentAssertions;
using Moq;

namespace Finance.Test.UnitTest.Application.UseCases.Account.RevokeAllAccess
{
    public class RevokeAllAccessHandlerTest : IClassFixture<RevokeAllAccessHandlerTestFixture>
    {
        private readonly RevokeAllAccessHandlerTestFixture _fixture;
        private readonly RevokeAllAccessHandler _sut;
        private readonly Mock<IAccountRepository> _accountRepositoryMock;

        public RevokeAllAccessHandlerTest(RevokeAllAccessHandlerTestFixture fixture)
        {
            _fixture = fixture;
            _accountRepositoryMock = new();

            _sut = new(
                accountRepository: _accountRepositoryMock.Object);
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatFindLoggedAccountsAsyncThrows))]
        [Trait("Unit/UseCase", "Account - RevokeAllAccess")]
        public async void ShouldRethrowSameExceptionThatFindLoggedAccountsAsyncThrows()
        {
            _accountRepositoryMock
                .Setup(x => x.FindLoggedAccountsAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(new UnexpectedException());

            var request = _fixture.MakeRevokeAllAccessRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }
    }
}