using Finance.Application.UseCases.User.UpdateUsername;
using Finance.Domain.Exceptions;
using Finance.Infrastructure.Database.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace Finance.Test.IntegrationTest.Application.UseCase.User.UpdateUsername
{
    public class UpdateUsernameHandlerTest(UpdateUsernameHandlerTestFixture fixture) : IClassFixture<UpdateUsernameHandlerTestFixture>
    {
        private readonly UpdateUsernameHandlerTestFixture _fixture = fixture;

        [Fact(DisplayName = nameof(ShouldThrowUsernameInUseException))]
        [Trait("Integration/UseCase", "User - UpdateUsername")]
        public async Task ShouldThrowUsernameInUseException()
        {
            var context = _fixture.MakeFinanceContext();
            var repository = new UserRepository(context);

            var user = _fixture.MakeUserModel();
            var trackingInfo = await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
            trackingInfo.State = EntityState.Detached;

            var sut = new UpdateUsernameHandler(userRepository: repository, unitOfWork: context);

            var request = _fixture.MakeUpdateUsernameRequest(username: user.Username);
            var act = () => sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UsernameInUseException>()
                .Where(x => x.Code == "username-in-use")
                .WithMessage("Username is already in use");
        }

        [Fact(DisplayName = nameof(ShouldThrowNotFoundException))]
        [Trait("Integration/UseCase", "User - UpdateUsername")]
        public async Task ShouldThrowNotFoundException()
        {
            var context = _fixture.MakeFinanceContext();
            var repository = new UserRepository(context);

            var sut = new UpdateUsernameHandler(userRepository: repository, unitOfWork: context);

            var request = _fixture.MakeUpdateUsernameRequest();
            var act = () => sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<NotFoundException>()
                .Where(x => x.Code == "not-found")
                .WithMessage("User not found");
        }

        [Fact(DisplayName = nameof(ShouldThrowUnexpectedException))]
        [Trait("Integration/UseCase", "User - UpdateUsername")]
        public async Task ShouldThrowUnexpectedException()
        {
            var context = _fixture.MakeFinanceContext();
            var repository = new UserRepository(context);

            var user = _fixture.MakeUserModel();
            var trackingInfo = await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
            trackingInfo.State = EntityState.Detached;

            var sut = new UpdateUsernameHandler(userRepository: repository, unitOfWork: context);

            await context.DisposeAsync();

            var request = _fixture.MakeUpdateUsernameRequest(userId: user.UserId);
            var act = () => sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }

        [Fact(DisplayName = nameof(ShouldReturnTheCorrectResponseIfUsernameIsSuccessfullyUpdated))]
        [Trait("Integration/UseCase", "User - UpdateUsername")]
        public async Task ShouldReturnTheCorrectResponseIfUsernameIsSuccessfullyUpdated()
        {
            var context = _fixture.MakeFinanceContext();
            var repository = new UserRepository(context);

            var user = _fixture.MakeUserModel();
            var trackingInfo = await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
            trackingInfo.State = EntityState.Detached;

            var sut = new UpdateUsernameHandler(userRepository: repository, unitOfWork: context);

            var request = _fixture.MakeUpdateUsernameRequest(userId: user.UserId);
            var response = await sut.Handle(request, _fixture.CancellationToken);

            var userDb = await context.Users.FirstOrDefaultAsync(x => x.UserId == response.UserId);
            userDb.Should().NotBeNull();
            userDb?.AccessTokenExpiresIn.Should().Be(response.AccessToken?.ExpiresIn);
            userDb?.AccessTokenValue.Should().Be(response.AccessToken?.Value);
            userDb?.UserId.Should().Be(response.UserId);
            userDb?.Active.Should().Be(response.Active);
            userDb?.Active.Should().Be(response.Active);
            userDb?.CreatedAt.Should().Be(response.CreatdAt);
            userDb?.Email.Should().Be(response.Email);
            userDb?.Email.Should().Be(response.Email);
            userDb?.EmailConfirmed.Should().Be(response.EmailConfirmed);
            userDb?.Phone.Should().Be(response.Phone);
            userDb?.PhoneConfirmed.Should().Be(response.PhoneConfirmed);
            userDb?.RefreshTokenExpiresIn.Should().Be(response.RefreshToken?.ExpiresIn);
            userDb?.RefreshTokenValue.Should().Be(response.RefreshToken?.Value);
            userDb?.Role.Should().Be(response.Role.ToString());
            userDb?.UpdatedAt.Should().Be(response.UpdatedAt);
            userDb?.Username.Should().Be(request.Username);
        }
    }
}