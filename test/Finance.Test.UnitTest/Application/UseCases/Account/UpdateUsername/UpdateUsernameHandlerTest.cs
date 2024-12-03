using Finance.Application.UseCases.User.UpdateUsername;
using Finance.Domain.Entities;
using Finance.Domain.Exceptions;
using Finance.Domain.Repositories;
using Finance.Domain.SeedWork;
using FluentAssertions;
using Moq;

namespace Finance.Test.UnitTest.Application.UseCases.User.UpdateUsername
{
    public class UpdateUsernameHandlerTest : IClassFixture<UpdateUsernameHandlerTestFixture>
    {
        private readonly UpdateUsernameHandlerTestFixture _fixture;
        private readonly UpdateUsernameHandler _sut;
        private readonly Mock<IUserRepository> _accountRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWork;

        public UpdateUsernameHandlerTest(UpdateUsernameHandlerTestFixture fixture)
        {
            _fixture = fixture;
            _accountRepositoryMock = new();
            _unitOfWork = new();

            _sut = new(
                userRepository: _accountRepositoryMock.Object,
                unitOfWork: _unitOfWork.Object);
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatCheckUsernameAsyncThrows))]
        [Trait("Unit/UseCase", "Account - UpdateUsername")]
        public async Task ShouldRethrowSameExceptionThatCheckUsernameAsyncThrows()
        {
            _accountRepositoryMock
                .Setup(x => x.CheckUsernameAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new UnexpectedException());

            var request = _fixture.MakeUpdateUsernameRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }

        [Fact(DisplayName = nameof(ShouldThrowUsernameInUseExceptionIfCheckUsernameAsyncReturnsTrue))]
        [Trait("Unit/UseCase", "Account - UpdateUsername")]
        public async Task ShouldThrowUsernameInUseExceptionIfCheckUsernameAsyncReturnsTrue()
        {
            _accountRepositoryMock
                .Setup(x => x.CheckUsernameAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            var request = _fixture.MakeUpdateUsernameRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UsernameInUseException>()
                .Where(x => x.Code == "username-in-use")
                .WithMessage("Username is already in use");
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatFindAsyncThrows))]
        [Trait("Unit/UseCase", "Account - UpdateUsername")]
        public async Task ShouldRethrowSameExceptionThatFindAsyncThrows()
        {
            _accountRepositoryMock
                .Setup(x => x.CheckUsernameAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            _accountRepositoryMock
                .Setup(x => x.FindAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new NotFoundException("Account"));

            var request = _fixture.MakeUpdateUsernameRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<NotFoundException>()
                .Where(x => x.Code == "not-found")
                .WithMessage("Account not found");
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatUpdateAsyncThrows))]
        [Trait("Unit/UseCase", "Account - UpdateUsername")]
        public async Task ShouldRethrowSameExceptionThatUpdateAsyncThrows()
        {
            _accountRepositoryMock
               .Setup(x => x.CheckUsernameAsync(
                   It.IsAny<string>(),
                   It.IsAny<CancellationToken>()))
               .ReturnsAsync(false);

            var account = _fixture.MakeUserEntity();
            _accountRepositoryMock
                .Setup(x => x.FindAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(account);

            _accountRepositoryMock
                .Setup(x => x.UpdateAsync(
                    It.IsAny<UserEntity>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new UnexpectedException());

            var request = _fixture.MakeUpdateUsernameRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatCommitAsyncThrows))]
        [Trait("Unit/UseCase", "Account - UpdateUsername")]
        public async Task ShouldRethrowSameExceptionThatCommitAsyncThrows()
        {
            _accountRepositoryMock
                .Setup(x => x.CheckUsernameAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            var account = _fixture.MakeUserEntity();
            _accountRepositoryMock
                .Setup(x => x.FindAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(account);

            _unitOfWork
                .Setup(x => x.CommitAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(new UnexpectedException());

            var request = _fixture.MakeUpdateUsernameRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }

        [Fact(DisplayName = nameof(ShouldReturnTheCorrectResponseIfAccountIsUpdatedSuccessfully))]
        [Trait("Unit/UseCase", "Account - UpdateUsername")]
        public async Task ShouldReturnTheCorrectResponseIfAccountIsUpdatedSuccessfully()
        {
            _accountRepositoryMock
               .Setup(x => x.CheckUsernameAsync(
                   It.IsAny<string>(),
                   It.IsAny<CancellationToken>()))
               .ReturnsAsync(false);

            var account = _fixture.MakeUserEntity();
            _accountRepositoryMock
                .Setup(x => x.FindAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(account);

            var request = _fixture.MakeUpdateUsernameRequest();
            var response = await _sut.Handle(request, _fixture.CancellationToken);

            response.AccessToken?.Value.Should().Be(account.AccessToken?.Value);
            response.AccessToken?.ExpiresIn.Should().Be(account.AccessToken?.ExpiresIn);
            response.UserId.Should().Be(account.Id);
            response.Active.Should().Be(account.Active);
            response.CreatdAt.Should().Be(account.CreatedAt);
            response.Email.Should().Be(account.Email);
            response.EmailConfirmed.Should().Be(account.EmailConfirmed);
            response.Phone.Should().Be(account.Phone);
            response.PhoneConfirmed.Should().Be(account.PhoneConfirmed);
            response.RefreshToken?.Value.Should().Be(account.RefreshToken?.Value);
            response.RefreshToken?.ExpiresIn.Should().Be(account.RefreshToken?.ExpiresIn);
            response.Role.Should().Be(account.Role);
            response.Username.Should().Be(request.Username);
        }
    }
}