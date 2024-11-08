using Finance.Application.UseCases.Account.UpdateEmail;
using Finance.Domain.Exceptions;
using Finance.Domain.Repositories;
using FluentAssertions;
using Moq;

namespace Finance.Test.UnitTest.Application.UseCases.Account.UpdateEmail
{
    public class UpdateEmailHandlerTest : IClassFixture<UpdateEmailHandlerTestFixture>
    {
        private readonly UpdateEmailHandlerTestFixture _fixture;
        private readonly UpdateEmailHandler _sut;
        private readonly Mock<IAccountRepository> _accountRepositoryMock;

        public UpdateEmailHandlerTest(UpdateEmailHandlerTestFixture fixture)
        {
            _fixture = fixture;
            _accountRepositoryMock = new();

            _sut = new(accountRepository: _accountRepositoryMock.Object);
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatFindAsyncThrows))]
        [Trait("Unit/UseCase", "Account - UpdateEmail")]
        public async Task ShouldRethrowSameExceptionThatFindAsyncThrows()
        {
            _accountRepositoryMock
                .Setup(x => x.FindAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new NotFoundException("Account"));

            var request = _fixture.MakeUpdateEmailRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<NotFoundException>()
                .Where(x => x.Code == "not-found")
                .WithMessage("Account not found");
        }
    }
}