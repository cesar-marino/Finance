using Finance.Application.UseCases.Account.CreateAccount;
using Finance.Domain.Exceptions;
using Finance.Domain.Repositories;
using FluentAssertions;
using Moq;

namespace Finance.Test.UnitTest.Application.UseCases.Account.CreateAccount
{
    public class CreateAccountHandlerTest : IClassFixture<CreateAccountHandlerTestFixture>
    {
        private readonly CreateAccountHandlerTestFixture _fixture;
        private readonly CreateAccountHandler _sut;
        private readonly Mock<IAccountRepository> _accountRepositoryMock;

        public CreateAccountHandlerTest(CreateAccountHandlerTestFixture fixture)
        {
            _fixture = fixture;
            _accountRepositoryMock = new();

            _sut = new(
                accountRepository: _accountRepositoryMock.Object
            );
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatCheckEmailAsyncThrows))]
        [Trait("Unit/UseCase", "Account - CreateAccount")]
        public async Task ShouldRethrowSameExceptionThatCheckEmailAsyncThrows()
        {
            _accountRepositoryMock
                .Setup(x => x.CheckEmailAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new UnexpectedException());

            var request = _fixture.MakeCreateAccountRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }

        [Fact(DisplayName = nameof(ShouldThrowEmailInUseExceptionIfCheckEmailAsyncReturnsTrue))]
        [Trait("Unit/UseCase", "Account - CreateAccount")]
        public async Task ShouldThrowEmailInUseExceptionIfCheckEmailAsyncReturnsTrue()
        {
            _accountRepositoryMock
                .Setup(x => x.CheckEmailAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            var request = _fixture.MakeCreateAccountRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<EmailInUseException>()
                .Where(x => x.Code == "email-in-use")
                .WithMessage("Email is already in use");
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatCheckUsernameAsyncThrows))]
        [Trait("Unit/UseCase", "Account - CreateAccount")]
        public async Task ShouldRethrowSameExceptionThatCheckUsernameAsyncThrows()
        {
            _accountRepositoryMock
                .Setup(x => x.CheckEmailAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            _accountRepositoryMock
                .Setup(x => x.CheckUsernameAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new UnexpectedException());

            var request = _fixture.MakeCreateAccountRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }

        [Fact(DisplayName = nameof(ShouldThrowUsernameInUseExceptionIfCheckEmailAsyncReturnsTrue))]
        [Trait("Unit/UseCase", "Account - CreateAccount")]
        public async Task ShouldThrowUsernameInUseExceptionIfCheckEmailAsyncReturnsTrue()
        {
            _accountRepositoryMock
                .Setup(x => x.CheckEmailAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            _accountRepositoryMock
                .Setup(x => x.CheckUsernameAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            var request = _fixture.MakeCreateAccountRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UsernameInUseException>()
                .Where(x => x.Code == "username-in-use")
                .WithMessage("Username is already in use");
        }
    }
}