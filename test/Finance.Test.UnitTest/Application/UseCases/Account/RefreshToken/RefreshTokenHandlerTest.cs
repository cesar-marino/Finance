using Finance.Application.Services;
using Finance.Application.UseCases.Account.RefreshToken;
using Finance.Domain.Exceptions;
using FluentAssertions;
using Moq;

namespace Finance.Test.UnitTest.Application.UseCases.Account.RefreshToken
{
    public class RefreshTokenHandlerTest : IClassFixture<RefreshTokenHandlerTestFixture>
    {
        private readonly RefreshTokenHandlerTestFixture _fixture;
        private readonly RefreshTokenHandler _sut;
        private readonly Mock<ITokenService> _tokenServiceMock;

        public RefreshTokenHandlerTest(RefreshTokenHandlerTestFixture fixture)
        {
            _fixture = fixture;
            _tokenServiceMock = new();

            _sut = new(
                tokenService: _tokenServiceMock.Object
            );
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatGetUsernameFromTokenAsyncThrows))]
        [Trait("Unit/UseCase", "Account - RefreshToken")]
        public async void ShouldRethrowSameExceptionThatGetUsernameFromTokenAsyncThrows()
        {
            _tokenServiceMock
                .Setup(x => x.GetUsernameFromTokenAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new UnexpectedException());

            var request = _fixture.MakeRefreshTokenRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }
    }
}