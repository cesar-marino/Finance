using Finance.Application.Services;
using Finance.Application.UseCases.Account.GetCurrentAccount;
using Finance.Domain.Exceptions;
using Finance.Domain.Repositories;
using FluentAssertions;
using Moq;

namespace Finance.Test.UnitTest.Application.UseCases.Account.GetCurrentAccount
{
    public class GetCurrentAccountHandlerTest : IClassFixture<GetCurrentAccountHandlerTestFixture>
    {
        private readonly GetCurrentAccountHandlerTestFixture _fixture;
        private readonly GetCurrentAccountHandler _sut;
        private readonly Mock<ITokenService> _tokenServiceMock;
        private readonly Mock<IAccountRepository> _accountRepositoryMock;

        public GetCurrentAccountHandlerTest(GetCurrentAccountHandlerTestFixture fixture)
        {
            _fixture = fixture;
            _tokenServiceMock = new();
            _accountRepositoryMock = new();

            _sut = new(
                tokenService: _tokenServiceMock.Object,
                accountRepository: _accountRepositoryMock.Object);
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatGetUsernameFromTokenAsyncThrows))]
        [Trait("Unit/UseCase", "Account - GetCurrentAccount")]
        public async Task ShouldRethrowSameExceptionThatGetUsernameFromTokenAsyncThrows()
        {
            _tokenServiceMock
                .Setup(x => x.GetUsernameFromTokenAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new UnexpectedException());

            var request = _fixture.MakeGetCurrentAccountRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatFindByUsernameAsyncThrows))]
        [Trait("Unit/UseCase", "Account - GetCurrentAccount")]
        public async Task ShouldRethrowSameExceptionThatFindByUsernameAsyncThrows()
        {
            var username = _fixture.Faker.Internet.UserName();
            _tokenServiceMock
                .Setup(x => x.GetUsernameFromTokenAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(username);

            _accountRepositoryMock
                .Setup(x => x.FindByUsernameAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new NotFoundException("Account"));

            var request = _fixture.MakeGetCurrentAccountRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<NotFoundException>()
                .Where(x => x.Code == "not-found")
                .WithMessage("Account not found");
        }

        [Fact(DisplayName = nameof(ShouldReturnTheCorrectResponseIfAccountIsFound))]
        [Trait("Unit/UseCase", "Account - GetCurrentAccount")]
        public async Task ShouldReturnTheCorrectResponseIfAccountIsFound()
        {
            var username = _fixture.Faker.Internet.UserName();
            _tokenServiceMock
                .Setup(x => x.GetUsernameFromTokenAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(username);

            var account = _fixture.MakeAccountEntity();
            _accountRepositoryMock
                .Setup(x => x.FindByUsernameAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(account);

            var request = _fixture.MakeGetCurrentAccountRequest();
            var response = await _sut.Handle(request, _fixture.CancellationToken);

            response.AccessToken.Should().NotBeNull();
            response.AccessToken?.Value.Should().Be(account.AccessToken?.Value);
            response.AccessToken?.ExpiresIn.Should().Be(account.AccessToken?.ExpiresIn);
            response.AccountId.Should().Be(account.Id);
            response.Active.Should().BeTrue();
            response.CreatdAt.Should().Be(account.CreatedAt);
            response.Email.Should().Be(account.Email);
            response.EmailConfirmed.Should().Be(account.EmailConfirmed);
            response.Phone.Should().Be(account.Phone);
            response.PhoneConfirmed.Should().Be(account.PhoneConfirmed);
            response.RefreshToken.Should().NotBeNull();
            response.RefreshToken?.Value.Should().Be(account.RefreshToken?.Value);
            response.RefreshToken?.ExpiresIn.Should().Be(account.RefreshToken?.ExpiresIn);
            response.Role.Should().Be(account.Role);
            response.Username.Should().Be(account.Username);
        }
    }
}