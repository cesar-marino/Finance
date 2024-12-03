using Finance.Application.UseCases.User.DisableUser;
using Finance.Domain.Exceptions;
using Finance.Infrastructure.Database.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace Finance.Test.IntegrationTest.Application.UseCase.User.DisableUser
{
    public class DisableUserHandlerTest(DisableUserHandlerTestFixture fixture) : IClassFixture<DisableUserHandlerTestFixture>
    {
        private readonly DisableUserHandlerTestFixture _fixture = fixture;

        [Fact(DisplayName = nameof(ShouldThrowNotFoundException))]
        [Trait("Integration/UseCase", "User - DisableUser")]
        public async Task ShouldThrowNotFoundException()
        {
            var context = _fixture.MakeFinanceContext();
            var repository = new UserRepository(context);

            var sut = new DisableUserHandler(userRepository: repository, unitOfWork: context);

            var request = _fixture.MakeDisableUserRequest();
            var act = () => sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<NotFoundException>()
                .Where(x => x.Code == "not-found")
                .WithMessage("User not found");
        }

        [Fact(DisplayName = nameof(ShouldThrowUnexpectedException))]
        [Trait("Integration/UseCase", "User - DisableUser")]
        public async Task ShouldThrowUnexpectedException()
        {
            var user = _fixture.MakeUserModel();
            var context = _fixture.MakeFinanceContext();

            var trackingInfo = await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
            trackingInfo.State = EntityState.Detached;

            var repository = new UserRepository(context);

            var sut = new DisableUserHandler(userRepository: repository, unitOfWork: context);

            await context.DisposeAsync();

            var request = _fixture.MakeDisableUserRequest(userId: user.UserId);
            var act = () => sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }

        [Fact(DisplayName = nameof(ShouldReturnTheCorrectResponseIfUserIsSuccessfullyDisabled))]
        [Trait("Integration/UseCase", "User - DisableUser")]
        public async Task ShouldReturnTheCorrectResponseIfUserIsSuccessfullyDisabled()
        {
            var user = _fixture.MakeUserModel();
            var context = _fixture.MakeFinanceContext();

            var trackingInfo = await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
            trackingInfo.State = EntityState.Detached;

            var repository = new UserRepository(context);

            var sut = new DisableUserHandler(userRepository: repository, unitOfWork: context);

            var request = _fixture.MakeDisableUserRequest(userId: user.UserId);
            var response = await sut.Handle(request, _fixture.CancellationToken);

            var userDb = await context.Users.FirstOrDefaultAsync(x => x.UserId == response.UserId);

            userDb.Should().NotBeNull();
            userDb?.AccessTokenExpiresIn.Should().Be(response.AccessToken?.ExpiresIn);
            userDb?.AccessTokenValue.Should().Be(response.AccessToken?.Value);
            userDb?.UserId.Should().Be(response.UserId);
            userDb?.Active.Should().BeFalse();
            userDb?.Active.Should().Be(response.Active);
            userDb?.CreatedAt.Should().Be(response.CreatdAt);
            userDb?.Email.Should().Be(response.Email);
            userDb?.EmailConfirmed.Should().Be(response.EmailConfirmed);
            userDb?.Phone.Should().Be(response.Phone);
            userDb?.PhoneConfirmed.Should().Be(response.PhoneConfirmed);
            userDb?.RefreshTokenExpiresIn.Should().Be(response.RefreshToken?.ExpiresIn);
            userDb?.RefreshTokenValue.Should().Be(response.RefreshToken?.Value);
            userDb?.Role.Should().Be(response.Role.ToString());
            userDb?.UpdatedAt.Should().Be(response.UpdatedAt);
            userDb?.Username.Should().Be(response.Username);
        }
    }
}