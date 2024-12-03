using Finance.Application.UseCases.User.EnableUser;
using Finance.Domain.Entities;
using Finance.Domain.Exceptions;
using Finance.Domain.Repositories;
using Finance.Domain.SeedWork;
using FluentAssertions;
using Moq;

namespace Finance.Test.UnitTest.Application.UseCases.User.EnableUser
{
    public class EnableUserHandlerTest : IClassFixture<EnableUserHandlerTestFixture>
    {
        private readonly EnableUserHandlerTestFixture _fixture;
        private readonly EnableUserHandler _sut;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public EnableUserHandlerTest(EnableUserHandlerTestFixture fixture)
        {
            _fixture = fixture;
            _userRepositoryMock = new();
            _unitOfWorkMock = new();

            _sut = new(
                userRepository: _userRepositoryMock.Object,
                unitOfWork: _unitOfWorkMock.Object);
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatFindAsyncThrows))]
        [Trait("Unit/UseCase", "User - EnableUser")]
        public async Task ShouldRethrowSameExceptionThatFindAsyncThrows()
        {
            _userRepositoryMock
                .Setup(x => x.FindAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new NotFoundException("User"));

            var request = _fixture.MakeEnableUserRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<NotFoundException>()
                .Where(x => x.Code == "not-found")
                .WithMessage("User not found");
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatUpdateAsyncThrows))]
        [Trait("Unit/UseCase", "User - EnableUser")]
        public async Task ShouldRethrowSameExceptionThatUpdateAsyncThrows()
        {
            var user = _fixture.MakeUserEntity(active: false);
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

            var request = _fixture.MakeEnableUserRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatCommitAsyncThrows))]
        [Trait("Unit/UseCase", "User - EnableUser")]
        public async Task ShouldRethrowSameExceptionThatCommitAsyncThrows()
        {
            var user = _fixture.MakeUserEntity(active: false);
            _userRepositoryMock
                .Setup(x => x.FindAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);

            _unitOfWorkMock
                .Setup(x => x.CommitAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(new UnexpectedException());

            var request = _fixture.MakeEnableUserRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }

        [Fact(DisplayName = nameof(ShouldReturnTheCorrectResponseIfUserIsEnabledSuccessfully))]
        [Trait("Unit/UseCase", "User - EnableUser")]
        public async Task ShouldReturnTheCorrectResponseIfUserIsEnabledSuccessfully()
        {
            var user = _fixture.MakeUserEntity(active: false);
            _userRepositoryMock
                .Setup(x => x.FindAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);

            var request = _fixture.MakeEnableUserRequest();
            var response = await _sut.Handle(request, _fixture.CancellationToken);

            response.AccessToken?.Value.Should().Be(user.AccessToken?.Value);
            response.AccessToken?.ExpiresIn.Should().Be(user.AccessToken?.ExpiresIn);
            response.UserId.Should().Be(user.Id);
            response.Active.Should().BeTrue();
            response.CreatdAt.Should().Be(user.CreatedAt);
            response.Email.Should().Be(user.Email);
            response.EmailConfirmed.Should().BeFalse();
            response.Phone.Should().Be(user.Phone);
            response.PhoneConfirmed.Should().Be(user.PhoneConfirmed);
            response.RefreshToken?.Value.Should().Be(user.RefreshToken?.Value);
            response.RefreshToken?.ExpiresIn.Should().Be(user.RefreshToken?.ExpiresIn);
            response.Role.Should().Be(user.Role);
            response.Username.Should().Be(user.Username);
        }
    }
}