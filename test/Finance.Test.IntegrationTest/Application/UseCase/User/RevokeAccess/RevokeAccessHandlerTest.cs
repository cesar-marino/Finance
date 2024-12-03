using Finance.Application.UseCases.User.RevokeAccess;
using Finance.Domain.Exceptions;
using Finance.Infrastructure.Database.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace Finance.Test.IntegrationTest.Application.UseCase.User.RevokeAccess
{
    public class RevokeAccessHandlerTest(RevokeAccessHandlerTestFixture fixture) : IClassFixture<RevokeAccessHandlerTestFixture>
    {
        private readonly RevokeAccessHandlerTestFixture _fixture = fixture;

        [Fact(DisplayName = nameof(ShouldThrowNotFoundException))]
        [Trait("Integration/UseCase", "User - RevokeAccess")]
        public async Task ShouldThrowNotFoundException()
        {
            var context = _fixture.MakeFinanceContext();
            var repository = new UserRepository(context);

            var sut = new RevokeAccessHandler(userRepository: repository, unitOfWork: context);

            var request = _fixture.MakeRevokeAccessRequest();
            var act = () => sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<NotFoundException>()
                .Where(x => x.Code == "not-found")
                .WithMessage("User not found");
        }

        [Fact(DisplayName = nameof(ShouldThrowUnexpectedException))]
        [Trait("Integration/UseCase", "User - RevokeAccess")]
        public async Task ShouldThrowUnexpectedException()
        {
            var user = _fixture.MakeUserModel();
            var context = _fixture.MakeFinanceContext();
            var repository = new UserRepository(context);

            var trackingInfo = await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
            trackingInfo.State = EntityState.Detached;

            var sut = new RevokeAccessHandler(userRepository: repository, unitOfWork: context);

            await context.DisposeAsync();

            var request = _fixture.MakeRevokeAccessRequest();
            var act = () => sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }

        [Fact(DisplayName = nameof(ShouldReturnTheCorrectResponseIfAccessIsSuccessfullyRevoked))]
        [Trait("Integration/UseCase", "User - RevokeAccess")]
        public async Task ShouldReturnTheCorrectResponseIfAccessIsSuccessfullyRevoked()
        {
            var user = _fixture.MakeUserModel();
            var context = _fixture.MakeFinanceContext();
            var repository = new UserRepository(context);

            var trackingInfo = await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
            trackingInfo.State = EntityState.Detached;

            var sut = new RevokeAccessHandler(userRepository: repository, unitOfWork: context);
            var request = _fixture.MakeRevokeAccessRequest(userId: user.UserId);
            var response = await sut.Handle(request, _fixture.CancellationToken);

            var userDb = await context.Users.FirstOrDefaultAsync(x => x.UserId == response.UserId);
            userDb.Should().NotBeNull();
            userDb?.AccessTokenExpiresIn.Should().Be(response.AccessToken?.ExpiresIn);
            userDb?.AccessTokenExpiresIn.Should().BeNull();
            userDb?.AccessTokenValue.Should().Be(response.AccessToken?.Value);
            userDb?.AccessTokenValue.Should().BeNull();
            userDb?.UserId.Should().Be(response.UserId);
            userDb?.Active.Should().Be(response.Active);
            userDb?.CreatedAt.Should().Be(response.CreatdAt);
            userDb?.Email.Should().Be(response.Email);
            userDb?.EmailConfirmed.Should().Be(response.EmailConfirmed);
            userDb?.Phone.Should().Be(response.Phone);
            userDb?.PhoneConfirmed.Should().Be(response.PhoneConfirmed);
            userDb?.RefreshTokenExpiresIn.Should().Be(response.RefreshToken?.ExpiresIn);
            userDb?.RefreshTokenExpiresIn.Should().BeNull();
            userDb?.RefreshTokenValue.Should().Be(response.RefreshToken?.Value);
            userDb?.RefreshTokenValue.Should().BeNull();
            userDb?.Role.Should().Be(response.Role.ToString());
            userDb?.UpdatedAt.Should().Be(response.UpdatedAt);
            userDb?.Username.Should().Be(response.Username);
        }
    }
}