using Finance.Application.UseCases.User.Authentication;
using Finance.Domain.Exceptions;
using Finance.Infrastructure.Database.Repositories;
using Finance.Infrastructure.Services.Encryption;
using Finance.Infrastructure.Services.Token;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Finance.Test.IntegrationTest.Application.UseCase.User.Authentication
{
    public class AuthenticationHandlerTest : IClassFixture<AuthenticationHandlerTestFixture>
    {
        private readonly AuthenticationHandlerTestFixture _fixture;
        private readonly IConfiguration _configuration;

        public AuthenticationHandlerTest(AuthenticationHandlerTestFixture fixture)
        {
            _fixture = fixture;
            _configuration = _fixture.MakeConfiguration();
        }

        [Fact(DisplayName = nameof(ShouldThrowNotFoundException))]
        [Trait("Integration/UseCase", "User - Authentication")]
        public async Task ShouldThrowNotFoundException()
        {
            var encryptionService = new EncryptionService();
            var tokenService = new JwtBearerAdapter(configuration: _configuration);
            var context = _fixture.MakeFinanceContext();
            var repository = new UserRepository(context);

            var sut = new AuthenticationHandler(
                userRepository: repository,
                encryptionService: encryptionService,
                tokenService: tokenService,
                unitOfWork: context);

            var request = _fixture.MakeAuthenticationRequest();
            var act = () => sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<NotFoundException>()
                .Where(x => x.Code == "not-found")
                .WithMessage("User not found");
        }

        [Fact(DisplayName = nameof(ShouldThrowInvalidPasswordException))]
        [Trait("Integration/UseCase", "User - Authentication")]
        public async Task ShouldThrowInvalidPasswordException()
        {
            var user = _fixture.MakeUserModel();
            var tokenService = new JwtBearerAdapter(configuration: _configuration);
            var encryptionService = new EncryptionService();

            var accessToken = await tokenService.GenerateAccessTokenAsync(user.ToEntity(), _fixture.CancellationToken);
            var refreshToken = await tokenService.GenerateRefreshTokenAsync(_fixture.CancellationToken);

            user.AccessTokenValue = accessToken.Value;
            user.AccessTokenExpiresIn = accessToken.ExpiresIn;
            user.RefreshTokenValue = refreshToken.Value;
            user.RefreshTokenExpiresIn = refreshToken.ExpiresIn;
            user.Passwrd = await encryptionService.EcnryptAsync(user.Passwrd);

            var context = _fixture.MakeFinanceContext();
            var trackingInfo = await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
            trackingInfo.State = EntityState.Detached;

            var repository = new UserRepository(context);

            var sut = new AuthenticationHandler(
                userRepository: repository,
                encryptionService: encryptionService,
                tokenService: tokenService,
                unitOfWork: context);

            var request = _fixture.MakeAuthenticationRequest(email: user.Email);
            var act = () => sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<InvalidPasswordException>()
                .Where(x => x.Code == "invalid-password")
                .WithMessage("Incorrect password");
        }

        [Fact(DisplayName = nameof(ShouldThrowDisableUserException))]
        [Trait("Integration/UseCase", "User - Authentication")]
        public async Task ShouldThrowDisableUserException()
        {
            var password = _fixture.Faker.Internet.Password();
            var user = _fixture.MakeUserModel(active: false, password: password);
            var tokenService = new JwtBearerAdapter(configuration: _configuration);
            var encryptionService = new EncryptionService();

            var accessToken = await tokenService.GenerateAccessTokenAsync(user.ToEntity(), _fixture.CancellationToken);
            var refreshToken = await tokenService.GenerateRefreshTokenAsync(_fixture.CancellationToken);

            user.AccessTokenValue = accessToken.Value;
            user.AccessTokenExpiresIn = accessToken.ExpiresIn;
            user.RefreshTokenValue = refreshToken.Value;
            user.RefreshTokenExpiresIn = refreshToken.ExpiresIn;
            user.Passwrd = await encryptionService.EcnryptAsync(user.Passwrd);

            var context = _fixture.MakeFinanceContext();
            var trackingInfo = await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
            trackingInfo.State = EntityState.Detached;

            var repository = new UserRepository(context);

            var sut = new AuthenticationHandler(
                userRepository: repository,
                encryptionService: encryptionService,
                tokenService: tokenService,
                unitOfWork: context);

            var request = _fixture.MakeAuthenticationRequest(email: user.Email, password: password);
            var act = () => sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<DisableUserException>()
                .Where(x => x.Code == "disable-user")
                .WithMessage("Disable user");
        }

        [Fact(DisplayName = nameof(ShouldThrowUnexpectedException))]
        [Trait("Integration/UseCase", "User - Authentication")]
        public async Task ShouldThrowUnexpectedException()
        {
            var password = _fixture.Faker.Internet.Password();
            var user = _fixture.MakeUserModel(password: password);
            var tokenService = new JwtBearerAdapter(configuration: _configuration);
            var encryptionService = new EncryptionService();

            var accessToken = await tokenService.GenerateAccessTokenAsync(user.ToEntity(), _fixture.CancellationToken);
            var refreshToken = await tokenService.GenerateRefreshTokenAsync(_fixture.CancellationToken);

            user.AccessTokenValue = accessToken.Value;
            user.AccessTokenExpiresIn = accessToken.ExpiresIn;
            user.RefreshTokenValue = refreshToken.Value;
            user.RefreshTokenExpiresIn = refreshToken.ExpiresIn;
            user.Passwrd = await encryptionService.EcnryptAsync(user.Passwrd);

            var context = _fixture.MakeFinanceContext();
            var trackingInfo = await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
            trackingInfo.State = EntityState.Detached;

            var repository = new UserRepository(context);

            var sut = new AuthenticationHandler(
                userRepository: repository,
                encryptionService: encryptionService,
                tokenService: tokenService,
                unitOfWork: context);

            await context.DisposeAsync();

            var request = _fixture.MakeAuthenticationRequest(email: user.Email, password: password);
            var act = () => sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }

        [Fact(DisplayName = nameof(ShouldReturnTheCorrectResponseIfUserIsSuccessfullyAuthenticated))]
        [Trait("Integration/UseCase", "User - Authentication")]
        public async Task ShouldReturnTheCorrectResponseIfUserIsSuccessfullyAuthenticated()
        {
            var password = _fixture.Faker.Internet.Password();
            var user = _fixture.MakeUserModel(password: password);
            var tokenService = new JwtBearerAdapter(configuration: _configuration);
            var encryptionService = new EncryptionService();

            var accessToken = await tokenService.GenerateAccessTokenAsync(user.ToEntity(), _fixture.CancellationToken);
            var refreshToken = await tokenService.GenerateRefreshTokenAsync(_fixture.CancellationToken);

            user.AccessTokenValue = accessToken.Value;
            user.AccessTokenExpiresIn = accessToken.ExpiresIn;
            user.RefreshTokenValue = refreshToken.Value;
            user.RefreshTokenExpiresIn = refreshToken.ExpiresIn;
            user.Passwrd = await encryptionService.EcnryptAsync(user.Passwrd);

            var context = _fixture.MakeFinanceContext();
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();

            var repository = new UserRepository(context);

            var sut = new AuthenticationHandler(
                userRepository: repository,
                encryptionService: encryptionService,
                tokenService: tokenService,
                unitOfWork: context);

            var request = _fixture.MakeAuthenticationRequest(email: user.Email, password: password);
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