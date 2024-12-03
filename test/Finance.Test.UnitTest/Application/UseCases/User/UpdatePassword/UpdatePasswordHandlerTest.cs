using Finance.Application.Services;
using Finance.Application.UseCases.User.UpdatePassword;
using Finance.Domain.Entities;
using Finance.Domain.Exceptions;
using Finance.Domain.Repositories;
using Finance.Domain.SeedWork;
using FluentAssertions;
using Moq;

namespace Finance.Test.UnitTest.Application.UseCases.User.UpdatePassword
{
    public class UpdatePasswordHandlerTest : IClassFixture<UpdatePasswordHandlerTestFixture>
    {
        private readonly UpdatePasswordHandlerTestFixture _fixture;
        private readonly UpdatePasswordHandler _sut;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IEncryptionService> _encryptionServiceMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public UpdatePasswordHandlerTest(UpdatePasswordHandlerTestFixture fixture)
        {
            _fixture = fixture;
            _userRepositoryMock = new();
            _encryptionServiceMock = new();
            _unitOfWorkMock = new();

            _sut = new(
                userRepository: _userRepositoryMock.Object,
                encryptionService: _encryptionServiceMock.Object,
                unitOfWork: _unitOfWorkMock.Object);
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatFindAsyncThrows))]
        [Trait("Unit/UseCase", "User - UpdatePassword")]
        public async void ShouldRethrowSameExceptionThatFindAsyncThrows()
        {
            _userRepositoryMock
                .Setup(x => x.FindAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new NotFoundException("User"));

            var request = _fixture.MakeUpdatePasswordRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<NotFoundException>()
                .Where(x => x.Code == "not-found")
                .WithMessage("User not found");
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatVerifyAsyncThrows))]
        [Trait("Unit/UseCase", "User - UpdatePassword")]
        public async void ShouldRethrowSameExceptionThatVerifyAsyncThrows()
        {
            var user = _fixture.MakeUserEntity();
            _userRepositoryMock
                .Setup(x => x.FindAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);

            _encryptionServiceMock
                .Setup(x => x.VerifyAsync(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new UnexpectedException());

            var request = _fixture.MakeUpdatePasswordRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }

        [Fact(DisplayName = nameof(ShouldThrowInvalidPasswordExceptionIfVerifyAsyncReturnsFalse))]
        [Trait("Unit/UseCase", "User - UpdatePassword")]
        public async void ShouldThrowInvalidPasswordExceptionIfVerifyAsyncReturnsFalse()
        {
            var user = _fixture.MakeUserEntity();
            _userRepositoryMock
                .Setup(x => x.FindAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);

            _encryptionServiceMock
                .Setup(x => x.VerifyAsync(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            var request = _fixture.MakeUpdatePasswordRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<InvalidPasswordException>()
                .Where(x => x.Code == "invalid-password")
                .WithMessage("Incorrect password");
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatEncryptAsyncThrows))]
        [Trait("Unit/UseCase", "User - UpdatePassword")]
        public async void ShouldRethrowSameExceptionThatEncryptAsyncThrows()
        {
            var user = _fixture.MakeUserEntity();
            _userRepositoryMock
                .Setup(x => x.FindAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);

            _encryptionServiceMock
                .Setup(x => x.VerifyAsync(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            _encryptionServiceMock
                .Setup(x => x.EcnryptAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new UnexpectedException());

            var request = _fixture.MakeUpdatePasswordRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatUpdateAsyncThrows))]
        [Trait("Unit/UseCase", "User - UpdatePassword")]
        public async void ShouldRethrowSameExceptionThatUpdateAsyncThrows()
        {
            var user = _fixture.MakeUserEntity();
            _userRepositoryMock
                .Setup(x => x.FindAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);

            _encryptionServiceMock
                .Setup(x => x.VerifyAsync(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            var password = _fixture.Faker.Internet.Password();
            _encryptionServiceMock
                .Setup(x => x.EcnryptAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(password);

            _userRepositoryMock
                .Setup(x => x.UpdateAsync(
                    It.IsAny<UserEntity>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new UnexpectedException());

            var request = _fixture.MakeUpdatePasswordRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatCommitAsyncThrows))]
        [Trait("Unit/UseCase", "User - UpdatePassword")]
        public async void ShouldRethrowSameExceptionThatCommitAsyncThrows()
        {
            var user = _fixture.MakeUserEntity();
            _userRepositoryMock
                .Setup(x => x.FindAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);

            _encryptionServiceMock
                .Setup(x => x.VerifyAsync(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            var password = _fixture.Faker.Internet.Password();
            _encryptionServiceMock
                .Setup(x => x.EcnryptAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(password);

            _unitOfWorkMock
                .Setup(x => x.CommitAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(new UnexpectedException());

            var request = _fixture.MakeUpdatePasswordRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }

        [Fact(DisplayName = nameof(ShouldReturnTheCorrectResponseIfPasswordIsSuccessfullyUpdated))]
        [Trait("Unit/UseCase", "User - UpdatePassword")]
        public async void ShouldReturnTheCorrectResponseIfPasswordIsSuccessfullyUpdated()
        {
            var user = _fixture.MakeUserEntity();
            _userRepositoryMock
                .Setup(x => x.FindAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);

            _encryptionServiceMock
                .Setup(x => x.VerifyAsync(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            var password = _fixture.Faker.Internet.Password();
            _encryptionServiceMock
                .Setup(x => x.EcnryptAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(password);

            var request = _fixture.MakeUpdatePasswordRequest();
            var response = await _sut.Handle(request, _fixture.CancellationToken);

            user.Password.Should().Be(password);

            response.AccessToken?.Value.Should().Be(user.AccessToken?.Value);
            response.AccessToken?.ExpiresIn.Should().Be(user.AccessToken?.ExpiresIn);
            response.UserId.Should().Be(user.Id);
            response.Active.Should().Be(user.Active);
            response.CreatdAt.Should().Be(user.CreatedAt);
            response.Email.Should().Be(user.Email);
            response.EmailConfirmed.Should().Be(user.EmailConfirmed);
            response.Phone.Should().Be(user.Phone);
            response.PhoneConfirmed.Should().Be(user.PhoneConfirmed);
            response.RefreshToken?.Value.Should().Be(user.RefreshToken?.Value);
            response.RefreshToken?.ExpiresIn.Should().Be(user.RefreshToken?.ExpiresIn);
            response.Role.Should().Be(user.Role);
            response.Username.Should().Be(user.Username);
        }
    }
}