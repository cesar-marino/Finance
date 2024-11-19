using Finance.Infrastructure.Database.Repositories;

namespace Finance.Test.IntegrationTest.Application.UseCase.Account.CreateAccount
{
    public class CreateAccountHandlerTest : IClassFixture<CreateAccountHandlerTestFixture>
    {
        private readonly CreateAccountHandlerTestFixture _fixture;

        public CreateAccountHandlerTest(CreateAccountHandlerTestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact(DisplayName = nameof(ShouldThrowEmailInUseException))]
        [Trait("Integration/UseCase", "Account - CreateAccount")]
        public async void ShouldThrowEmailInUseException()
        {
            var account = _fixture.MakeAccountModel();
            var context = _fixture.MakeFinanceContext();

            await context.Accounts.AddAsync(account);
            await context.SaveChangesAsync();

            var repository = new AccountRepository(context);


            // When

            // Then
        }
    }
}