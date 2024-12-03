using Finance.Application.UseCases.User.RevokeAllAccess;
using Finance.Domain.Entities;
using Finance.Domain.Exceptions;
using Finance.Domain.Repositories;
using Finance.Domain.SeedWork;
using FluentAssertions;
using Moq;

namespace Finance.Test.UnitTest.Application.UseCases.User.RevokeAllAccess
{
    public class RevokeAllAccessHandlerTest : IClassFixture<RevokeAllAccessHandlerTestFixture>
    {
        private readonly RevokeAllAccessHandlerTestFixture _fixture;
        private readonly RevokeAllAccessHandler _sut;
        private readonly Mock<IUserRepository> _accountRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public RevokeAllAccessHandlerTest(RevokeAllAccessHandlerTestFixture fixture)
        {
            _fixture = fixture;
            _accountRepositoryMock = new();
            _unitOfWorkMock = new();

            _sut = new(
                userRepository: _accountRepositoryMock.Object,
                unitOfWork: _unitOfWorkMock.Object);
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatFindLoggedAccountsAsyncThrows))]
        [Trait("Unit/UseCase", "Account - RevokeAllAccess")]
        public async Task ShouldRethrowSameExceptionThatFindLoggedAccountsAsyncThrows()
        {
            _accountRepositoryMock
                .Setup(x => x.FindLoggedUsersAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(new UnexpectedException());

            var request = _fixture.MakeRevokeAllAccessRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatUpdateAsyncThrows))]
        [Trait("Unit/UseCase", "Account - RevokeAllAccess")]
        public async Task ShouldRethrowSameExceptionThatUpdateAsyncThrows()
        {
            var accounts = _fixture.MakeAccountList();
            _accountRepositoryMock
                .Setup(x => x.FindLoggedUsersAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(accounts);

            _accountRepositoryMock
                .Setup(x => x.UpdateAsync(
                    It.IsAny<UserEntity>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new UnexpectedException());

            var request = _fixture.MakeRevokeAllAccessRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatCommitAsyncThrows))]
        [Trait("Unit/UseCase", "Account - RevokeAllAccess")]
        public async Task ShouldRethrowSameExceptionThatCommitAsyncThrows()
        {
            var accounts = _fixture.MakeAccountList();
            _accountRepositoryMock
                .Setup(x => x.FindLoggedUsersAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(accounts);

            _unitOfWorkMock
                .Setup(x => x.CommitAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(new UnexpectedException());

            var request = _fixture.MakeRevokeAllAccessRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }

        [Fact(DisplayName = nameof(ShouldReturnTheCorrectResponseIfAccessAreSuccessfullyRevoked))]
        [Trait("Unit/UseCase", "Account - RevokeAllAccess")]
        public async Task ShouldReturnTheCorrectResponseIfAccessAreSuccessfullyRevoked()
        {
            var accounts = _fixture.MakeAccountList();
            _accountRepositoryMock
                .Setup(x => x.FindLoggedUsersAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(accounts);

            var request = _fixture.MakeRevokeAllAccessRequest();
            await _sut.Handle(request, _fixture.CancellationToken);

            accounts.ToList().ForEach((account) =>
            {
                account.AccessToken.Should().BeNull();
                account.RefreshToken.Should().BeNull();
            });
        }
    }
}