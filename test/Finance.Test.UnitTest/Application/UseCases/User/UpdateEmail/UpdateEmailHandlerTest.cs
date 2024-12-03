using Finance.Application.UseCases.User.UpdateEmail;
using Finance.Domain.Entities;
using Finance.Domain.Exceptions;
using Finance.Domain.Repositories;
using Finance.Domain.SeedWork;
using FluentAssertions;
using Moq;

namespace Finance.Test.UnitTest.Application.UseCases.User.UpdateEmail
{
    public class UpdateEmailHandlerTest : IClassFixture<UpdateEmailHandlerTestFixture>
    {
        private readonly UpdateEmailHandlerTestFixture _fixture;
        private readonly UpdateEmailHandler _sut;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public UpdateEmailHandlerTest(UpdateEmailHandlerTestFixture fixture)
        {
            _fixture = fixture;
            _userRepositoryMock = new();
            _unitOfWorkMock = new();

            _sut = new(
                userRepository: _userRepositoryMock.Object,
                unitOfWork: _unitOfWorkMock.Object);
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatCheckEmailAsyncThrows))]
        [Trait("Unit/UseCase", "User - UpdateEmail")]
        public async Task ShouldRethrowSameExceptionThatCheckEmailAsyncThrows()
        {
            _userRepositoryMock
                .Setup(x => x.CheckEmailAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new UnexpectedException());

            var request = _fixture.MakeUpdateEmailRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }

        [Fact(DisplayName = nameof(ShouldThrowEmailInUseExceptionIfCheckEmailAsyncReturnsTrue))]
        [Trait("Unit/UseCase", "User - UpdateEmail")]
        public async Task ShouldThrowEmailInUseExceptionIfCheckEmailAsyncReturnsTrue()
        {
            _userRepositoryMock
                .Setup(x => x.CheckEmailAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            var request = _fixture.MakeUpdateEmailRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<EmailInUseException>()
                .Where(x => x.Code == "email-in-use")
                .WithMessage("Email is already in use");
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatFindAsyncThrows))]
        [Trait("Unit/UseCase", "User - UpdateEmail")]
        public async Task ShouldRethrowSameExceptionThatFindAsyncThrows()
        {
            _userRepositoryMock
                .Setup(x => x.CheckEmailAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            _userRepositoryMock
                .Setup(x => x.FindAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new NotFoundException("User"));

            var request = _fixture.MakeUpdateEmailRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<NotFoundException>()
                .Where(x => x.Code == "not-found")
                .WithMessage("User not found");
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatUpdateAsyncThrows))]
        [Trait("Unit/UseCase", "User - UpdateEmail")]
        public async Task ShouldRethrowSameExceptionThatUpdateAsyncThrows()
        {
            _userRepositoryMock
                .Setup(x => x.CheckEmailAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

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

            var request = _fixture.MakeUpdateEmailRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatCommitAsyncThrows))]
        [Trait("Unit/UseCase", "User - UpdateEmail")]
        public async Task ShouldRethrowSameExceptionThatCommitAsyncThrows()
        {
            _userRepositoryMock
                .Setup(x => x.CheckEmailAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            var user = _fixture.MakeUserEntity();
            _userRepositoryMock
                .Setup(x => x.FindAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);

            _unitOfWorkMock
                .Setup(x => x.CommitAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(new UnexpectedException());

            var request = _fixture.MakeUpdateEmailRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }

        [Fact(DisplayName = nameof(ShouldReturnTheCorrectResponseIfUserIsUpdatedSuccessfully))]
        [Trait("Unit/UseCase", "User - UpdateEmail")]
        public async Task ShouldReturnTheCorrectResponseIfUserIsUpdatedSuccessfully()
        {
            _userRepositoryMock
                .Setup(x => x.CheckEmailAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            var user = _fixture.MakeUserEntity();
            _userRepositoryMock
                .Setup(x => x.FindAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);

            var request = _fixture.MakeUpdateEmailRequest();
            var response = await _sut.Handle(request, _fixture.CancellationToken);

            response.AccessToken?.Value.Should().Be(user.AccessToken?.Value);
            response.AccessToken?.ExpiresIn.Should().Be(user.AccessToken?.ExpiresIn);
            response.UserId.Should().Be(user.Id);
            response.Active.Should().Be(user.Active);
            response.CreatdAt.Should().Be(user.CreatedAt);
            response.Email.Should().Be(request.Email);
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