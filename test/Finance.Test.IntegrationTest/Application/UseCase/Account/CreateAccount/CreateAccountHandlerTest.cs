using Finance.Application.UseCases.Account.CreateAccount;
using Finance.Domain.Exceptions;
using Finance.Infrastructure.Database.Repositories;
using Finance.Infrastructure.Services.Encryption;
using Finance.Infrastructure.Services.Token;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Finance.Test.IntegrationTest.Application.UseCase.Account.CreateAccount
{
    public class CreateAccountHandlerTest : IClassFixture<CreateAccountHandlerTestFixture>
    {
        private readonly CreateAccountHandlerTestFixture _fixture;
        private readonly IConfiguration _configuration;

        public CreateAccountHandlerTest(CreateAccountHandlerTestFixture fixture)
        {
            _fixture = fixture;
            _configuration = _fixture.MakeConfiguration();
        }

        [Fact(DisplayName = nameof(ShouldThrowEmailInUseException))]
        [Trait("Integration/UseCase", "Account - CreateAccount")]
        public async Task ShouldThrowEmailInUseException()
        {
            var account = _fixture.MakeAccountModel();
            var context = _fixture.MakeFinanceContext();

            var trackingInfo = await context.Accounts.AddAsync(account);
            await context.SaveChangesAsync();
            trackingInfo.State = EntityState.Detached;

            var tokenService = new JwtBearerAdapter(configuration: _configuration);
            var encryptionService = new EncryptionService();
            var repository = new AccountRepository(context);

            var sut = new CreateAccountHandler(
                accountRepository: repository,
                encryptionService: encryptionService,
                tokenService: tokenService,
                unitOfWork: context);

            var request = _fixture.MakeCreateAccountRequest(email: account.Email);
            var act = () => sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<EmailInUseException>()
                .Where(x => x.Code == "email-in-use")
                .WithMessage("Email is already in use");
        }

        [Fact(DisplayName = nameof(ShouldThrowUsernameInUseException))]
        [Trait("Integration/UseCase", "Account - CreateAccount")]
        public async Task ShouldThrowUsernameInUseException()
        {
            var account = _fixture.MakeAccountModel();
            var context = _fixture.MakeFinanceContext();

            var trackingInfo = await context.Accounts.AddAsync(account);
            await context.SaveChangesAsync();
            trackingInfo.State = EntityState.Detached;

            var tokenService = new JwtBearerAdapter(configuration: _configuration);
            var encryptionService = new EncryptionService();
            var repository = new AccountRepository(context);

            var sut = new CreateAccountHandler(
                accountRepository: repository,
                encryptionService: encryptionService,
                tokenService: tokenService,
                unitOfWork: context);

            var request = _fixture.MakeCreateAccountRequest(username: account.Username);
            var act = () => sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UsernameInUseException>()
                .Where(x => x.Code == "username-in-use")
                .WithMessage("Username is already in use");
        }

        [Fact(DisplayName = nameof(ShouldThrowUnexpectedException))]
        [Trait("Integration/UseCase", "Account - CreateAccount")]
        public async Task ShouldThrowUnexpectedException()
        {
            var account = _fixture.MakeAccountModel();
            var context = _fixture.MakeFinanceContext();

            var trackingInfo = await context.Accounts.AddAsync(account);
            await context.SaveChangesAsync();
            trackingInfo.State = EntityState.Detached;

            var tokenService = new JwtBearerAdapter(configuration: _configuration);
            var encryptionService = new EncryptionService();
            var repository = new AccountRepository(context);

            var sut = new CreateAccountHandler(
                accountRepository: repository,
                encryptionService: encryptionService,
                tokenService: tokenService,
                unitOfWork: context);

            await context.DisposeAsync();

            var request = _fixture.MakeCreateAccountRequest(username: account.Username);
            var act = () => sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }

        [Fact(DisplayName = nameof(ShouldReturnTheCorrectResponseIfAccountIsSuccessfullyCreated))]
        [Trait("Integration/UseCase", "Account - CreateAccount")]
        public async Task ShouldReturnTheCorrectResponseIfAccountIsSuccessfullyCreated()
        {
            var tokenService = new JwtBearerAdapter(configuration: _configuration);
            var encryptionService = new EncryptionService();
            var context = _fixture.MakeFinanceContext();
            var repository = new AccountRepository(context);

            var sut = new CreateAccountHandler(
                accountRepository: repository,
                encryptionService: encryptionService,
                tokenService: tokenService,
                unitOfWork: context);

            var request = _fixture.MakeCreateAccountRequest();
            var response = await sut.Handle(request, _fixture.CancellationToken);

            var account = await context.Accounts.FirstOrDefaultAsync(x => x.AccountId == response.AccountId);

            account.Should().NotBeNull();
            account?.AccessTokenExpiresIn.Should().Be(response.AccessToken?.ExpiresIn);
            account?.AccessTokenValue.Should().Be(response.AccessToken?.Value);
            account?.AccountId.Should().Be(response.AccountId);
            account?.Active.Should().BeTrue();
            response.Active.Should().BeTrue();
            account?.CreatedAt.Should().Be(response.CreatdAt);
            account?.Email.Should().Be(response.Email);
            account?.EmailConfirmed.Should().BeFalse();
            response.EmailConfirmed.Should().BeFalse();
            account?.EmailConfirmed.Should().Be(response.EmailConfirmed);
            account?.Phone.Should().Be(response.Phone);
            account?.PhoneConfirmed.Should().BeFalse();
            response.PhoneConfirmed.Should().BeFalse();
            account?.PhoneConfirmed.Should().Be(response.PhoneConfirmed);
            account?.RefreshTokenExpiresIn.Should().Be(response.RefreshToken?.ExpiresIn);
            account?.RefreshTokenValue.Should().Be(response.RefreshToken?.Value);
            account?.Role.Should().Be(response.Role.ToString());
            account?.UpdatedAt.Should().Be(response.UpdatedAt);
            account?.Username.Should().Be(response.Username);
        }
    }
}