using Finance.Application.Services;
using Finance.Application.UseCases.Account.UpdatePassword;
using Finance.Domain.Entities;
using Finance.Domain.Exceptions;
using Finance.Domain.Repositories;
using Finance.Domain.SeedWork;
using FluentAssertions;
using Moq;

namespace Finance.Test.UnitTest.Application.UseCases.Account.UpdatePassword
{
    public class UpdatePasswordHandlerTest : IClassFixture<UpdatePasswordHandlerTestFixture>
    {
        private readonly UpdatePasswordHandlerTestFixture _fixture;
        private readonly UpdatePasswordHandler _sut;
        private readonly Mock<IAccountRepository> _accountRepositoryMock;
        private readonly Mock<IEncryptionService> _encryptionServiceMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public UpdatePasswordHandlerTest(UpdatePasswordHandlerTestFixture fixture)
        {
            _fixture = fixture;
            _accountRepositoryMock = new();
            _encryptionServiceMock = new();
            _unitOfWorkMock = new();

            _sut = new(
                accountRepository: _accountRepositoryMock.Object,
                encryptionService: _encryptionServiceMock.Object,
                unitOfWork: _unitOfWorkMock.Object);
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

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatVerifyAsyncThrows))]
        [Trait("Unit/UseCase", "Account - UpdatePassword")]
        public async void ShouldRethrowSameExceptionThatVerifyAsyncThrows()
        {
            var account = _fixture.MakeAccountEntity();
            _accountRepositoryMock
                .Setup(x => x.FindAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(account);

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
        [Trait("Unit/UseCase", "Account - UpdatePassword")]
        public async void ShouldThrowInvalidPasswordExceptionIfVerifyAsyncReturnsFalse()
        {
            var account = _fixture.MakeAccountEntity();
            _accountRepositoryMock
                .Setup(x => x.FindAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(account);

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
        [Trait("Unit/UseCase", "Account - UpdatePassword")]
        public async void ShouldRethrowSameExceptionThatEncryptAsyncThrows()
        {
            var account = _fixture.MakeAccountEntity();
            _accountRepositoryMock
                .Setup(x => x.FindAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(account);

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
        [Trait("Unit/UseCase", "Account - UpdatePassword")]
        public async void ShouldRethrowSameExceptionThatUpdateAsyncThrows()
        {
            var account = _fixture.MakeAccountEntity();
            _accountRepositoryMock
                .Setup(x => x.FindAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(account);

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

            _accountRepositoryMock
                .Setup(x => x.UpdateAsync(
                    It.IsAny<AccountEntity>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new UnexpectedException());

            var request = _fixture.MakeUpdatePasswordRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatCommitAsyncThrows))]
        [Trait("Unit/UseCase", "Account - UpdatePassword")]
        public async void ShouldRethrowSameExceptionThatCommitAsyncThrows()
        {
            var account = _fixture.MakeAccountEntity();
            _accountRepositoryMock
                .Setup(x => x.FindAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(account);

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
        [Trait("Unit/UseCase", "Account - UpdatePassword")]
        public async void ShouldReturnTheCorrectResponseIfPasswordIsSuccessfullyUpdated()
        {
            var account = _fixture.MakeAccountEntity();
            _accountRepositoryMock
                .Setup(x => x.FindAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(account);

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

            account.Password.Should().Be(password);

            response.AccessToken?.Value.Should().Be(account.AccessToken?.Value);
            response.AccessToken?.ExpiresIn.Should().Be(account.AccessToken?.ExpiresIn);
            response.AccountId.Should().Be(account.Id);
            response.Active.Should().Be(account.Active);
            response.CreatdAt.Should().Be(account.CreatedAt);
            response.Email.Should().Be(account.Email);
            response.EmailConfirmed.Should().Be(account.EmailConfirmed);
            response.Phone.Should().Be(account.Phone);
            response.PhoneConfirmed.Should().Be(account.PhoneConfirmed);
            response.RefreshToken?.Value.Should().Be(account.RefreshToken?.Value);
            response.RefreshToken?.ExpiresIn.Should().Be(account.RefreshToken?.ExpiresIn);
            response.Role.Should().Be(account.Role);
            response.Username.Should().Be(account.Username);
        }
    }
}