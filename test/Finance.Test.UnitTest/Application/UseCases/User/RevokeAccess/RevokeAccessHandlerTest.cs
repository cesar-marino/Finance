using Finance.Application.UseCases.User.RevokeAccess;
using Finance.Domain.Entities;
using Finance.Domain.Exceptions;
using Finance.Domain.Repositories;
using Finance.Domain.SeedWork;
using FluentAssertions;
using Moq;

namespace Finance.Test.UnitTest.Application.UseCases.User.RevokeAccess
{
    public class RevokeAccessHandlerTest : IClassFixture<RevokeAccessHandlerTestFixture>
    {
        private readonly RevokeAccessHandlerTestFixture _fixture;
        private readonly RevokeAccessHandler _sut;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public RevokeAccessHandlerTest(RevokeAccessHandlerTestFixture fixture)
        {
            _fixture = fixture;
            _userRepositoryMock = new();
            _unitOfWorkMock = new();

            _sut = new(
                userRepository: _userRepositoryMock.Object,
                unitOfWork: _unitOfWorkMock.Object);
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatFindAsyncThrows))]
        [Trait("Unit/UseCase", "User - RevokeAccess")]
        public async Task ShouldRethrowSameExceptionThatFindAsyncThrows()
        {
            _userRepositoryMock
                .Setup(x => x.FindAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new NotFoundException("User"));

            var request = _fixture.MakeRevokeAccessRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<NotFoundException>()
                .Where(x => x.Code == "not-found")
                .WithMessage("User not found");
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatUpdateAsyncThrows))]
        [Trait("Unit/UseCase", "User - RevokeAccess")]
        public async Task ShouldRethrowSameExceptionThatUpdateAsyncThrows()
        {
            var user = _fixture.MakeUserEntity();
            _userRepositoryMock
                .Setup(x => x.FindAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);

            _userRepositoryMock
                .Setup(x => x.UpdateAsync(
                    It.IsAny<UserEntity>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new UnexpectedException());

            var request = _fixture.MakeRevokeAccessRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatCommitAsyncThrows))]
        [Trait("Unit/UseCase", "User - RevokeAccess")]
        public async Task ShouldRethrowSameExceptionThatCommitAsyncThrows()
        {
            var user = _fixture.MakeUserEntity();
            _userRepositoryMock
                .Setup(x => x.FindAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);

            _unitOfWorkMock
                .Setup(x => x.CommitAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(new UnexpectedException());

            var request = _fixture.MakeRevokeAccessRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }

        [Fact(DisplayName = nameof(ShouldReturnTheCorrectResponseIfAccessIsSuccessfullyRevoked))]
        [Trait("Unit/UseCase", "User - RevokeAccess")]
        public async Task ShouldReturnTheCorrectResponseIfAccessIsSuccessfullyRevoked()
        {
            var user = _fixture.MakeUserEntity();
            _userRepositoryMock
                .Setup(x => x.FindAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);

            var request = _fixture.MakeRevokeAccessRequest();
            var response = await _sut.Handle(request, _fixture.CancellationToken);

            response.AccessToken.Should().BeNull();
            response.UserId.Should().Be(user.Id);
            response.Active.Should().Be(user.Active);
            response.CreatdAt.Should().Be(user.CreatedAt);
            response.Email.Should().Be(user.Email);
            response.EmailConfirmed.Should().Be(user.EmailConfirmed);
            response.Phone.Should().Be(user.Phone);
            response.PhoneConfirmed.Should().Be(user.PhoneConfirmed);
            response.RefreshToken.Should().BeNull();
            response.Role.Should().Be(user.Role);
            response.Username.Should().Be(user.Username);
        }
    }
}