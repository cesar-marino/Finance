using Finance.Application.Services;
using Finance.Application.UseCases.Account.GetCurrentAccount;
using Finance.Domain.Exceptions;
using FluentAssertions;
using Moq;

namespace Finance.Test.UnitTest.Application.UseCases.Account.GetCurrentAccount
{
    public class GetCurrentAccountHandlerTest : IClassFixture<GetCurrentAccountHandlerTestFixture>
    {
        private readonly GetCurrentAccountHandlerTestFixture _fixture;
        private readonly GetCurrentAccountHandler _sut;
        private readonly Mock<ITokenService> _tokenServiceMock;

        public GetCurrentAccountHandlerTest(GetCurrentAccountHandlerTestFixture fixture)
        {
            _fixture = fixture;
            _tokenServiceMock = new();

            _sut = new(
                tokenService: _tokenServiceMock.Object);
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatGetUsernameFromTokenAsync))]
        [Trait("Unit/UseCase", "Account - GetCurrentAccount")]
        public async Task ShouldRethrowSameExceptionThatGetUsernameFromTokenAsync()
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
    }
}