using Finance.Application.UseCases.User.UpdatePassword;
using Finance.Domain.Exceptions;
using Finance.Infrastructure.Database.Repositories;
using Finance.Infrastructure.Services.Encryption;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace Finance.Test.IntegrationTest.Application.UseCase.User.UpdatePassword
{
    public class UpdatePasswordHandlerTest(UpdatePasswordHandlerTestFixture fixture) : IClassFixture<UpdatePasswordHandlerTestFixture>
    {
        private readonly UpdatePasswordHandlerTestFixture _fixture = fixture;

        [Fact(DisplayName = nameof(ShouldThrowNotFoundException))]
        [Trait("Integration/UseCase", "User - UpdatePassword")]
        public async Task ShouldThrowNotFoundException()
        {
            var context = _fixture.MakeFinanceContext();
            var repository = new UserRepository(context);
            var encryptionService = new EncryptionService();

            var sut = new UpdatePasswordHandler(
                userRepository: repository,
                encryptionService: encryptionService,
                unitOfWork: context);

            var request = _fixture.MakeUpdatePasswordRequest();
            var act = () => sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<NotFoundException>()
                .Where(x => x.Code == "not-found")
                .WithMessage("User not found");
        }

        [Fact(DisplayName = nameof(ShouldThrowInvalidPasswordException))]
        [Trait("Integration/UseCase", "User - UpdatePassword")]
        public async Task ShouldThrowInvalidPasswordException()
        {
            var context = _fixture.MakeFinanceContext();
            var repository = new UserRepository(context);
            var encryptionService = new EncryptionService();

            var user = _fixture.MakeUserModel();
            user.Passwrd = await encryptionService.EcnryptAsync(user.Passwrd);
            var trackingInfo = await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
            trackingInfo.State = EntityState.Detached;

            var sut = new UpdatePasswordHandler(
                userRepository: repository,
                encryptionService: encryptionService,
                unitOfWork: context);

            var request = _fixture.MakeUpdatePasswordRequest(userId: user.UserId);
            var act = () => sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<InvalidPasswordException>()
                .Where(x => x.Code == "invalid-password")
                .WithMessage("Incorrect password");
        }

        [Fact(DisplayName = nameof(ShouldThrowUnexpectedException))]
        [Trait("Integration/UseCase", "User - UpdatePassword")]
        public async Task ShouldThrowUnexpectedException()
        {
            var context = _fixture.MakeFinanceContext();
            var repository = new UserRepository(context);
            var encryptionService = new EncryptionService();

            var user = _fixture.MakeUserModel();
            user.Passwrd = await encryptionService.EcnryptAsync(user.Passwrd);
            var trackingInfo = await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
            trackingInfo.State = EntityState.Detached;

            var sut = new UpdatePasswordHandler(
                userRepository: repository,
                encryptionService: encryptionService,
                unitOfWork: context);

            await context.DisposeAsync();

            var request = _fixture.MakeUpdatePasswordRequest(userId: user.UserId, currentPassword: user.Passwrd);
            var act = () => sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }

        [Fact(DisplayName = nameof(ShouldReturnTheCorrectResponseIfPasswordIsSuccessfullyUpdated))]
        [Trait("Integration/UseCase", "User - UpdatePassword")]
        public async Task ShouldReturnTheCorrectResponseIfPasswordIsSuccessfullyUpdated()
        {
            var context = _fixture.MakeFinanceContext();
            var repository = new UserRepository(context);
            var encryptionService = new EncryptionService();

            var currentPassword = _fixture.Faker.Internet.Password();
            var currentPasswordEncrypted = await encryptionService.EcnryptAsync(currentPassword);
            var newPassword = _fixture.Faker.Internet.Password();
            var newPasswordEncrypted = await encryptionService.EcnryptAsync(newPassword);

            var user = _fixture.MakeUserModel();
            user.Passwrd = currentPasswordEncrypted;
            var trackingInfo = await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
            trackingInfo.State = EntityState.Detached;

            var sut = new UpdatePasswordHandler(
                userRepository: repository,
                encryptionService: encryptionService,
                unitOfWork: context);

            var request = _fixture.MakeUpdatePasswordRequest(
                userId: user.UserId,
                currentPassword: currentPassword,
                newPassword: newPassword);

            var response = await sut.Handle(request, _fixture.CancellationToken);

            var userDb = await context.Users.FirstOrDefaultAsync(x => x.UserId == response.UserId);
            userDb.Should().NotBeNull();
            userDb?.AccessTokenExpiresIn.Should().Be(response.AccessToken?.ExpiresIn);
            userDb?.AccessTokenValue.Should().Be(response.AccessToken?.Value);
            userDb?.UserId.Should().Be(response.UserId);
            userDb?.Active.Should().Be(response.Active);
            userDb?.Active.Should().Be(response.Active);
            userDb?.CreatedAt.Should().Be(response.CreatdAt);
            userDb?.Email.Should().Be(response.Email);
            userDb?.Email.Should().Be(response.Email);
            userDb?.EmailConfirmed.Should().Be(response.EmailConfirmed);
            userDb?.Phone.Should().Be(response.Phone);
            userDb?.PhoneConfirmed.Should().Be(response.PhoneConfirmed);
            userDb?.RefreshTokenExpiresIn.Should().Be(response.RefreshToken?.ExpiresIn);
            userDb?.RefreshTokenValue.Should().Be(response.RefreshToken?.Value);
            userDb?.Role.Should().Be(response.Role.ToString());
            userDb?.UpdatedAt.Should().Be(response.UpdatedAt);
            userDb?.Username.Should().Be(response.Username);
            // userDb?.Passwrd.Should().Be(newPasswordEncrypted);
        }
    }
}