using Finance.Application.UseCases.Account.RefreshToken;
using Finance.Domain.Exceptions;
using Finance.Infrastructure.Database.Repositories;
using Finance.Infrastructure.Services.Token;
using FluentAssertions;

namespace Finance.Test.IntegrationTest.Application.UseCase.Account.RefreshToken
{
    public class RefreshTokenHandlerTest(RefreshTokenHandlerTestFixture fixture) : IClassFixture<RefreshTokenHandlerTestFixture>
    {
        private readonly RefreshTokenHandlerTestFixture _fixture = fixture;

        [Fact(DisplayName = nameof(ShouldThrowInvalidTokenException))]
        [Trait("Integration/UseCase", "Account - RefreshToken")]
        public async Task ShouldThrowInvalidTokenException()
        {
            var tokenService = new JwtBearerAdapter(_fixture.MakeConfiguration());
            var context = _fixture.MakeFinanceContext();
            var repository = new AccountRepository(context);

            var sut = new RefreshTokenHandler(
                tokenService: tokenService,
                accountRepository: repository,
                unitOfWork: context);

            var request = _fixture.MakeRefreshTokenRequest();
            var act = () => sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<InvalidTokenException>()
                .Where(x => x.Code == "invalid-token")
                .WithMessage("Token is invalid");
        }
    }
}