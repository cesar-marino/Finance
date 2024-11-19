using Finance.Application.UseCases.Account.CreateAccount;
using Finance.Domain.Exceptions;
using Finance.Infrastructure.Database.Repositories;
using Finance.Infrastructure.Services.Encryption;
using Finance.Infrastructure.Services.Token;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;

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
        public async void ShouldThrowEmailInUseException()
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
    }
}