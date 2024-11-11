using Finance.Application.UseCases.Account.Authentication;
using Finance.Domain.Exceptions;
using Finance.Domain.Repositories;
using FluentAssertions;
using Moq;

namespace Finance.Test.UnitTest.Application.UseCases.Account.Authentication
{
    public class AuthenticationHandlerTest : IClassFixture<AuthenticationHandlerTestFixture>
    {
        private readonly AuthenticationHandlerTestFixture _fixture;
        private readonly AuthenticationHandler _sut;
        private readonly Mock<IAccountRepository> _accountRepositoryMock;

        public AuthenticationHandlerTest(AuthenticationHandlerTestFixture fixture)
        {
            _fixture = fixture;
            _accountRepositoryMock = new();

            _sut = new(
                accountRepository: _accountRepositoryMock.Object);
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatFindByEmailAsyncThrows))]
        [Trait("Unit/UseCase", "Account - Authentication")]
        public async Task ShouldRethrowSameExceptionThatFindByEmailAsyncThrows()
        {
            _accountRepositoryMock
                .Setup(x => x.FindByEmailAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new NotFoundException("Account"));

            var request = _fixture.MakeAuthenticationRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<NotFoundException>()
                .Where(x => x.Code == "not-found")
                .WithMessage("Account not found");
        }
    }
}