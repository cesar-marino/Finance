using Finance.Application.UseCases.Account.UpdatePassword;
using Finance.Domain.Exceptions;
using Finance.Infrastructure.Database.Repositories;
using Finance.Infrastructure.Services.Encryption;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace Finance.Test.IntegrationTest.Application.UseCase.Account.UpdatePassword
{
    public class UpdatePasswordHandlerTest(UpdatePasswordHandlerTestFixture fixture) : IClassFixture<UpdatePasswordHandlerTestFixture>
    {
        private readonly UpdatePasswordHandlerTestFixture _fixture = fixture;

        [Fact(DisplayName = nameof(ShouldThrowNotFoundException))]
        [Trait("Integration/UseCase", "Account - UpdatePassword")]
        public async Task ShouldThrowNotFoundException()
        {
            var context = _fixture.MakeFinanceContext();
            var repository = new AccountRepository(context);
            var encryptionService = new EncryptionService();

            var sut = new UpdatePasswordHandler(
                accountRepository: repository,
                encryptionService: encryptionService,
                unitOfWork: context);

            var request = _fixture.MakeUpdatePasswordRequest();
            var act = () => sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<NotFoundException>()
                .Where(x => x.Code == "not-found")
                .WithMessage("Account not found");
        }

        [Fact(DisplayName = nameof(ShouldThrowInvalidPasswordException))]
        [Trait("Integration/UseCase", "Account - UpdatePassword")]
        public async Task ShouldThrowInvalidPasswordException()
        {
            var context = _fixture.MakeFinanceContext();
            var repository = new AccountRepository(context);
            var encryptionService = new EncryptionService();

            var account = _fixture.MakeAccountModel();
            account.Passwrd = await encryptionService.EcnryptAsync(account.Passwrd);
            var trackingInfo = await context.Accounts.AddAsync(account);
            await context.SaveChangesAsync();
            trackingInfo.State = EntityState.Detached;

            var sut = new UpdatePasswordHandler(
                accountRepository: repository,
                encryptionService: encryptionService,
                unitOfWork: context);

            var request = _fixture.MakeUpdatePasswordRequest(accountId: account.AccountId);
            var act = () => sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<InvalidPasswordException>()
                .Where(x => x.Code == "invalid-password")
                .WithMessage("Incorrect password");
        }

        [Fact(DisplayName = nameof(ShouldThrowUnexpectedException))]
        [Trait("Integration/UseCase", "Account - UpdatePassword")]
        public async Task ShouldThrowUnexpectedException()
        {
            var context = _fixture.MakeFinanceContext();
            var repository = new AccountRepository(context);
            var encryptionService = new EncryptionService();

            var account = _fixture.MakeAccountModel();
            account.Passwrd = await encryptionService.EcnryptAsync(account.Passwrd);
            var trackingInfo = await context.Accounts.AddAsync(account);
            await context.SaveChangesAsync();
            trackingInfo.State = EntityState.Detached;

            var sut = new UpdatePasswordHandler(
                accountRepository: repository,
                encryptionService: encryptionService,
                unitOfWork: context);

            await context.DisposeAsync();

            var request = _fixture.MakeUpdatePasswordRequest(accountId: account.AccountId, currentPassword: account.Passwrd);
            var act = () => sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }

        [Fact(DisplayName = nameof(ShouldReturnTheCorrectResponseIfPasswordIsSuccessfullyUpdated))]
        [Trait("Integration/UseCase", "Account - UpdatePassword")]
        public async Task ShouldReturnTheCorrectResponseIfPasswordIsSuccessfullyUpdated()
        {
            var context = _fixture.MakeFinanceContext();
            var repository = new AccountRepository(context);
            var encryptionService = new EncryptionService();

            var currentPassword = _fixture.Faker.Internet.Password();
            var currentPasswordEncrypted = await encryptionService.EcnryptAsync(currentPassword);
            var newPassword = _fixture.Faker.Internet.Password();
            var newPasswordEncrypted = await encryptionService.EcnryptAsync(newPassword);

            var account = _fixture.MakeAccountModel();
            account.Passwrd = currentPasswordEncrypted;
            var trackingInfo = await context.Accounts.AddAsync(account);
            await context.SaveChangesAsync();
            trackingInfo.State = EntityState.Detached;

            var sut = new UpdatePasswordHandler(
                accountRepository: repository,
                encryptionService: encryptionService,
                unitOfWork: context);

            var request = _fixture.MakeUpdatePasswordRequest(
                accountId: account.AccountId,
                currentPassword: currentPassword,
                newPassword: newPassword);

            var response = await sut.Handle(request, _fixture.CancellationToken);

            var accountDb = await context.Accounts.FirstOrDefaultAsync(x => x.AccountId == response.AccountId);
            accountDb.Should().NotBeNull();
            accountDb?.AccessTokenExpiresIn.Should().Be(response.AccessToken?.ExpiresIn);
            accountDb?.AccessTokenValue.Should().Be(response.AccessToken?.Value);
            accountDb?.AccountId.Should().Be(response.AccountId);
            accountDb?.Active.Should().Be(response.Active);
            accountDb?.Active.Should().Be(response.Active);
            accountDb?.CreatedAt.Should().Be(response.CreatdAt);
            accountDb?.Email.Should().Be(response.Email);
            accountDb?.Email.Should().Be(response.Email);
            accountDb?.EmailConfirmed.Should().Be(response.EmailConfirmed);
            accountDb?.Phone.Should().Be(response.Phone);
            accountDb?.PhoneConfirmed.Should().Be(response.PhoneConfirmed);
            accountDb?.RefreshTokenExpiresIn.Should().Be(response.RefreshToken?.ExpiresIn);
            accountDb?.RefreshTokenValue.Should().Be(response.RefreshToken?.Value);
            accountDb?.Role.Should().Be(response.Role.ToString());
            accountDb?.UpdatedAt.Should().Be(response.UpdatedAt);
            accountDb?.Username.Should().Be(response.Username);
            // accountDb?.Passwrd.Should().Be(newPasswordEncrypted);
        }
    }
}