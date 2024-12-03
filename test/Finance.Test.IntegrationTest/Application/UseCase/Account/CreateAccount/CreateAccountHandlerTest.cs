using Finance.Application.UseCases.User.CreateUser;
using Finance.Domain.Exceptions;
using Finance.Infrastructure.Database.Repositories;
using Finance.Infrastructure.Services.Encryption;
using Finance.Infrastructure.Services.Token;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Finance.Test.IntegrationTest.Application.UseCase.User.CreateUser
{
    public class CreateUserHandlerTest : IClassFixture<CreateUserHandlerTestFixture>
    {
        private readonly CreateUserHandlerTestFixture _fixture;
        private readonly IConfiguration _configuration;

        public CreateUserHandlerTest(CreateUserHandlerTestFixture fixture)
        {
            _fixture = fixture;
            _configuration = _fixture.MakeConfiguration();
        }

        [Fact(DisplayName = nameof(ShouldThrowEmailInUseException))]
        [Trait("Integration/UseCase", "User - CreateUser")]
        public async Task ShouldThrowEmailInUseException()
        {
            var user = _fixture.MakeUserModel();
            var context = _fixture.MakeFinanceContext();

            var trackingInfo = await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
            trackingInfo.State = EntityState.Detached;

            var tokenService = new JwtBearerAdapter(configuration: _configuration);
            var encryptionService = new EncryptionService();
            var repository = new UserRepository(context);

            var sut = new CreateUserHandler(
                userRepository: repository,
                encryptionService: encryptionService,
                tokenService: tokenService,
                unitOfWork: context);

            var request = _fixture.MakeCreateUserRequest(email: user.Email);
            var act = () => sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<EmailInUseException>()
                .Where(x => x.Code == "email-in-use")
                .WithMessage("Email is already in use");
        }

        [Fact(DisplayName = nameof(ShouldThrowUsernameInUseException))]
        [Trait("Integration/UseCase", "User - CreateUser")]
        public async Task ShouldThrowUsernameInUseException()
        {
            var user = _fixture.MakeUserModel();
            var context = _fixture.MakeFinanceContext();

            var trackingInfo = await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
            trackingInfo.State = EntityState.Detached;

            var tokenService = new JwtBearerAdapter(configuration: _configuration);
            var encryptionService = new EncryptionService();
            var repository = new UserRepository(context);

            var sut = new CreateUserHandler(
                userRepository: repository,
                encryptionService: encryptionService,
                tokenService: tokenService,
                unitOfWork: context);

            var request = _fixture.MakeCreateUserRequest(username: user.Username);
            var act = () => sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UsernameInUseException>()
                .Where(x => x.Code == "username-in-use")
                .WithMessage("Username is already in use");
        }

        [Fact(DisplayName = nameof(ShouldThrowUnexpectedException))]
        [Trait("Integration/UseCase", "User - CreateUser")]
        public async Task ShouldThrowUnexpectedException()
        {
            var user = _fixture.MakeUserModel();
            var context = _fixture.MakeFinanceContext();

            var trackingInfo = await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
            trackingInfo.State = EntityState.Detached;

            var tokenService = new JwtBearerAdapter(configuration: _configuration);
            var encryptionService = new EncryptionService();
            var repository = new UserRepository(context);

            var sut = new CreateUserHandler(
                userRepository: repository,
                encryptionService: encryptionService,
                tokenService: tokenService,
                unitOfWork: context);

            await context.DisposeAsync();

            var request = _fixture.MakeCreateUserRequest(username: user.Username);
            var act = () => sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }

        [Fact(DisplayName = nameof(ShouldReturnTheCorrectResponseIfUserIsSuccessfullyCreated))]
        [Trait("Integration/UseCase", "User - CreateUser")]
        public async Task ShouldReturnTheCorrectResponseIfUserIsSuccessfullyCreated()
        {
            var tokenService = new JwtBearerAdapter(configuration: _configuration);
            var encryptionService = new EncryptionService();
            var context = _fixture.MakeFinanceContext();
            var repository = new UserRepository(context);

            var sut = new CreateUserHandler(
                userRepository: repository,
                encryptionService: encryptionService,
                tokenService: tokenService,
                unitOfWork: context);

            var request = _fixture.MakeCreateUserRequest();
            var response = await sut.Handle(request, _fixture.CancellationToken);

            var user = await context.Users.FirstOrDefaultAsync(x => x.UserId == response.UserId);

            user.Should().NotBeNull();
            user?.AccessTokenExpiresIn.Should().Be(response.AccessToken?.ExpiresIn);
            user?.AccessTokenValue.Should().Be(response.AccessToken?.Value);
            user?.UserId.Should().Be(response.UserId);
            user?.Active.Should().BeTrue();
            response.Active.Should().BeTrue();
            user?.CreatedAt.Should().Be(response.CreatdAt);
            user?.Email.Should().Be(response.Email);
            user?.EmailConfirmed.Should().BeFalse();
            response.EmailConfirmed.Should().BeFalse();
            user?.EmailConfirmed.Should().Be(response.EmailConfirmed);
            user?.Phone.Should().Be(response.Phone);
            user?.PhoneConfirmed.Should().BeFalse();
            response.PhoneConfirmed.Should().BeFalse();
            user?.PhoneConfirmed.Should().Be(response.PhoneConfirmed);
            user?.RefreshTokenExpiresIn.Should().Be(response.RefreshToken?.ExpiresIn);
            user?.RefreshTokenValue.Should().Be(response.RefreshToken?.Value);
            user?.Role.Should().Be(response.Role.ToString());
            user?.UpdatedAt.Should().Be(response.UpdatedAt);
            user?.Username.Should().Be(response.Username);
        }
    }
}