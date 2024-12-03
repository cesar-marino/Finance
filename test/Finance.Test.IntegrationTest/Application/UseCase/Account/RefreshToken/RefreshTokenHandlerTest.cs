using Finance.Application.UseCases.User.RefreshToken;
using Finance.Domain.Exceptions;
using Finance.Infrastructure.Database.Repositories;
using Finance.Infrastructure.Services.Token;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace Finance.Test.IntegrationTest.Application.UseCase.User.RefreshToken
{
    public class RefreshTokenHandlerTest(RefreshTokenHandlerTestFixture fixture) : IClassFixture<RefreshTokenHandlerTestFixture>
    {
        private readonly RefreshTokenHandlerTestFixture _fixture = fixture;

        [Fact(DisplayName = nameof(ShouldThrowInvalidTokenException))]
        [Trait("Integration/UseCase", "User - RefreshToken")]
        public async Task ShouldThrowInvalidTokenException()
        {
            var tokenService = new JwtBearerAdapter(_fixture.MakeConfiguration());
            var context = _fixture.MakeFinanceContext();
            var repository = new UserRepository(context);

            var sut = new RefreshTokenHandler(
                tokenService: tokenService,
                userRepository: repository,
                unitOfWork: context);

            var request = _fixture.MakeRefreshTokenRequest();
            var act = () => sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<InvalidTokenException>()
                .Where(x => x.Code == "invalid-token")
                .WithMessage("Token is invalid");
        }

        [Fact(DisplayName = nameof(ShouldThrowNotFoundException))]
        [Trait("Integration/UseCase", "User - RefreshToken")]
        public async Task ShouldThrowNotFoundException()
        {
            var tokenService = new JwtBearerAdapter(_fixture.MakeConfiguration());
            var user = _fixture.MakeUserModel();
            var token = await tokenService.GenerateAccessTokenAsync(user.ToEntity());

            var context = _fixture.MakeFinanceContext();
            var repository = new UserRepository(context);

            var sut = new RefreshTokenHandler(
                tokenService: tokenService,
                userRepository: repository,
                unitOfWork: context);

            var request = _fixture.MakeRefreshTokenRequest(accessToken: token.Value);
            var act = () => sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<NotFoundException>()
                .Where(x => x.Code == "not-found")
                .WithMessage("User not found");
        }

        [Fact(DisplayName = nameof(ShouldThrowUnauthorizedException))]
        [Trait("Integration/UseCase", "User - RefreshToken")]
        public async Task ShouldThrowUnauthorizedException()
        {
            var tokenService = new JwtBearerAdapter(_fixture.MakeConfiguration());
            var user = _fixture.MakeUserModel();
            var token = await tokenService.GenerateAccessTokenAsync(user.ToEntity());
            user.AccessTokenValue = token.Value;
            user.AccessTokenExpiresIn = token.ExpiresIn;

            var context = _fixture.MakeFinanceContext();
            var repository = new UserRepository(context);

            var trackingInfo = await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
            trackingInfo.State = EntityState.Detached;

            var sut = new RefreshTokenHandler(
                tokenService: tokenService,
                userRepository: repository,
                unitOfWork: context);

            var request = _fixture.MakeRefreshTokenRequest(accessToken: token.Value);
            var act = () => sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnauthorizedException>()
                .Where(x => x.Code == "unauthorized")
                .WithMessage("Unauthorized access");
        }

        [Fact(DisplayName = nameof(ShouldThrowUnexpectedException))]
        [Trait("Integration/UseCase", "User - RefreshToken")]
        public async Task ShouldThrowUnexpectedException()
        {
            var tokenService = new JwtBearerAdapter(_fixture.MakeConfiguration());
            var user = _fixture.MakeUserModel();
            var token = await tokenService.GenerateAccessTokenAsync(user.ToEntity());
            user.AccessTokenValue = token.Value;
            user.AccessTokenExpiresIn = token.ExpiresIn;

            var context = _fixture.MakeFinanceContext();
            var repository = new UserRepository(context);

            var trackingInfo = await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
            trackingInfo.State = EntityState.Detached;

            var sut = new RefreshTokenHandler(
                tokenService: tokenService,
                userRepository: repository,
                unitOfWork: context);

            await context.DisposeAsync();

            var request = _fixture.MakeRefreshTokenRequest(accessToken: token.Value);
            var act = () => sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }

        [Fact(DisplayName = nameof(ShouldReturnTheCorrectResponseIfTokensAreSuccessfullyRefreshed))]
        [Trait("Integration/UseCase", "User - RefreshToken")]
        public async Task ShouldReturnTheCorrectResponseIfTokensAreSuccessfullyRefreshed()
        {
            var tokenService = new JwtBearerAdapter(_fixture.MakeConfiguration());
            var user = _fixture.MakeUserModel();
            var token = await tokenService.GenerateAccessTokenAsync(user.ToEntity());
            user.AccessTokenValue = token.Value;
            user.AccessTokenExpiresIn = token.ExpiresIn;

            var context = _fixture.MakeFinanceContext();
            var repository = new UserRepository(context);

            var trackingInfo = await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
            trackingInfo.State = EntityState.Detached;

            var sut = new RefreshTokenHandler(
                tokenService: tokenService,
                userRepository: repository,
                unitOfWork: context);

            var request = _fixture.MakeRefreshTokenRequest(accessToken: token.Value, refreshToken: user.RefreshTokenValue);
            var response = await sut.Handle(request, _fixture.CancellationToken);

            var userDb = await context.Users.FirstOrDefaultAsync(x => x.UserId == response.UserId);
            userDb.Should().NotBeNull();
            userDb?.AccessTokenExpiresIn.Should().Be(response.AccessToken?.ExpiresIn);
            userDb?.AccessTokenValue.Should().Be(response.AccessToken?.Value);
            userDb?.UserId.Should().Be(response.UserId);
            userDb?.Active.Should().Be(response.Active);
            userDb?.CreatedAt.Should().Be(response.CreatdAt);
            userDb?.Email.Should().Be(response.Email);
            userDb?.EmailConfirmed.Should().Be(response.EmailConfirmed);
            userDb?.Phone.Should().Be(response.Phone);
            userDb?.PhoneConfirmed.Should().Be(response.PhoneConfirmed);
            userDb?.RefreshTokenExpiresIn.Should().Be(response.RefreshToken?.ExpiresIn);
            userDb?.RefreshTokenValue.Should().Be(response.RefreshToken?.Value);
            userDb?.Role.Should().Be(response.Role.ToString());
            userDb?.UpdatedAt.Should().Be(response.UpdatedAt);
            userDb?.Username.Should().Be(response.Username);
        }
    }
}