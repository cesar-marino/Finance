using Finance.Application.UseCases.Account.UpdatePassword;
using Finance.Domain.Exceptions;
using Finance.Domain.Repositories;
using FluentAssertions;
using Moq;

namespace Finance.Test.UnitTest.Application.UseCases.Account.UpdatePassword
{
    public class UpdatePasswordHandlerTest : IClassFixture<UpdatePasswordHandlerTestFixture>
    {
        private readonly UpdatePasswordHandlerTestFixture _fixture;
        private readonly UpdatePasswordHandler _sut;
        private readonly Mock<IAccountRepository> _accountRepositoryMock;

        public UpdatePasswordHandlerTest(UpdatePasswordHandlerTestFixture fixture)
        {
            _fixture = fixture;
            _accountRepositoryMock = new();
            _sut = new(
                accountRepository: _accountRepositoryMock.Object);
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatFindAsyncThrows))]
        [Trait("Unit/UseCase", "Account - UpdatePassword")]
        public async void ShouldRethrowSameExceptionThatFindAsyncThrows()
        {
            _accountRepositoryMock
                .Setup(x => x.FindAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new NotFoundException("Account"));

            var request = _fixture.MakeUpdatePasswordRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<NotFoundException>()
                .Where(x => x.Code == "not-found")
                .WithMessage("Account not found");
        }
    }
}