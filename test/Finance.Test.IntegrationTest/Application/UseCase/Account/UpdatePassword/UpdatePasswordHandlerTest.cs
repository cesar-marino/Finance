using Finance.Application.UseCases.Account.UpdatePassword;
using Finance.Domain.Exceptions;
using Finance.Infrastructure.Database.Repositories;
using Finance.Infrastructure.Services.Encryption;
using FluentAssertions;

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
    }
}