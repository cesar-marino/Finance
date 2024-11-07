using Finance.Application.Services;
using Finance.Application.UseCases.Account.CreateAccount;
using Finance.Domain.Entities;
using Finance.Domain.Exceptions;
using FluentAssertions;
using Moq;

namespace Finance.Test.UnitTest.Application.UseCases.Account.CreateAccount
{
    public class CreateAccountHandlerTest : IClassFixture<CreateAccountHandlerTestFixture>
    {
        private readonly CreateAccountHandlerTestFixture _fixture;
        private readonly CreateAccountHandler _sut;
        private readonly Mock<IAccountService> _accountServiceMock;

        public CreateAccountHandlerTest(CreateAccountHandlerTestFixture fixture)
        {
            _fixture = fixture;
            _accountServiceMock = new();

            _sut = new(accountService: _accountServiceMock.Object);
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatCreateAsyncThrows))]
        [Trait("Unit/UseCase", "Account - CreateAccount")]
        public async Task ShouldRethrowSameExceptionThatCreateAsyncThrows()
        {
            _accountServiceMock.Setup(x => x.CreateAsync(
                It.IsAny<AccountEntity>(),
                It.IsAny<CancellationToken>()))
                .ThrowsAsync(new UnexpectedException());

            var request = _fixture.MakeCreateAccountRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }

        [Fact(DisplayName = nameof(ShouldReturnTheCorrectResponseIfAccountIsCreatedSuccessfully))]
        [Trait("Unit/UseCase", "Account - CreateAccount")]
        public async Task ShouldReturnTheCorrectResponseIfAccountIsCreatedSuccessfully()
        {
            var account = _fixture.MakeAccountEntity();
            _accountServiceMock.Setup(x => x.CreateAsync(
                    It.IsAny<AccountEntity>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(account);

            var request = _fixture.MakeCreateAccountRequest();
            var response = await _sut.Handle(request, _fixture.CancellationToken);
        }
    }
}