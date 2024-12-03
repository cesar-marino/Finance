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
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public RevokeAllAccessHandlerTest(RevokeAllAccessHandlerTestFixture fixture)
        {
            _fixture = fixture;
            _userRepositoryMock = new();
            _unitOfWorkMock = new();

            _sut = new(
                userRepository: _userRepositoryMock.Object,
                unitOfWork: _unitOfWorkMock.Object);
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatFindLoggedUsersAsyncThrows))]
        [Trait("Unit/UseCase", "User - RevokeAllAccess")]
        public async Task ShouldRethrowSameExceptionThatFindLoggedUsersAsyncThrows()
        {
            _userRepositoryMock
                .Setup(x => x.FindLoggedUsersAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(new UnexpectedException());

            var request = _fixture.MakeRevokeAllAccessRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatUpdateAsyncThrows))]
        [Trait("Unit/UseCase", "User - RevokeAllAccess")]
        public async Task ShouldRethrowSameExceptionThatUpdateAsyncThrows()
        {
            var users = _fixture.MakeUserList();
            _userRepositoryMock
                .Setup(x => x.FindLoggedUsersAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(users);

            _userRepositoryMock
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
        [Trait("Unit/UseCase", "User - RevokeAllAccess")]
        public async Task ShouldRethrowSameExceptionThatCommitAsyncThrows()
        {
            var users = _fixture.MakeUserList();
            _userRepositoryMock
                .Setup(x => x.FindLoggedUsersAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(users);

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
        [Trait("Unit/UseCase", "User - RevokeAllAccess")]
        public async Task ShouldReturnTheCorrectResponseIfAccessAreSuccessfullyRevoked()
        {
            var users = _fixture.MakeUserList();
            _userRepositoryMock
                .Setup(x => x.FindLoggedUsersAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(users);

            var request = _fixture.MakeRevokeAllAccessRequest();
            await _sut.Handle(request, _fixture.CancellationToken);

            users.ToList().ForEach((user) =>
            {
                user.AccessToken.Should().BeNull();
                user.RefreshToken.Should().BeNull();
            });
        }
    }
}