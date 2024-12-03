using Finance.Application.UseCases.User.UpdateEmail;
using Finance.Domain.Exceptions;
using Finance.Infrastructure.Database.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace Finance.Test.IntegrationTest.Application.UseCase.User.UpdateEmail
{
    public class UpdateEmailHandlerTest(UpdateEmailHandlerTestFixture fixture) : IClassFixture<UpdateEmailHandlerTestFixture>
    {
        private readonly UpdateEmailHandlerTestFixture _fixture = fixture;

        [Fact(DisplayName = nameof(ShouldThrowEmailInUseException))]
        [Trait("INtegration/UseCase", "User - UpdateEmail")]
        public async Task ShouldThrowEmailInUseException()
        {
            var context = _fixture.MakeFinanceContext();
            var repository = new UserRepository(context);

            var user = _fixture.MakeUserModel();
            var trackingInfo = await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
            trackingInfo.State = EntityState.Detached;

            var sut = new UpdateEmailHandler(userRepository: repository, unitOfWork: context);

            var request = _fixture.MakeUpdateEmailRequest(email: user.Email);
            var act = () => sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<EmailInUseException>()
                .Where(x => x.Code == "email-in-use")
                .WithMessage("Email is already in use");
        }

        [Fact(DisplayName = nameof(ShouldThrowNotFoundException))]
        [Trait("INtegration/UseCase", "User - UpdateEmail")]
        public async Task ShouldThrowNotFoundException()
        {
            var context = _fixture.MakeFinanceContext();
            var repository = new UserRepository(context);

            var user = _fixture.MakeUserModel();
            var trackingInfo = await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
            trackingInfo.State = EntityState.Detached;

            var sut = new UpdateEmailHandler(userRepository: repository, unitOfWork: context);

            var request = _fixture.MakeUpdateEmailRequest();
            var act = () => sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<NotFoundException>()
                .Where(x => x.Code == "not-found")
                .WithMessage("User not found");
        }

        [Fact(DisplayName = nameof(ShouldThrowUnexpectedException))]
        [Trait("INtegration/UseCase", "User - UpdateEmail")]
        public async Task ShouldThrowUnexpectedException()
        {
            var context = _fixture.MakeFinanceContext();
            var repository = new UserRepository(context);

            var sut = new UpdateEmailHandler(userRepository: repository, unitOfWork: context);

            await context.DisposeAsync();

            var request = _fixture.MakeUpdateEmailRequest();
            var act = () => sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }

        [Fact(DisplayName = nameof(ShouldReturnTheCorrectResponseIfEmailIsSuccessfullyUpdated))]
        [Trait("INtegration/UseCase", "User - UpdateEmail")]
        public async Task ShouldReturnTheCorrectResponseIfEmailIsSuccessfullyUpdated()
        {
            var context = _fixture.MakeFinanceContext();
            var repository = new UserRepository(context);

            var user = _fixture.MakeUserModel();
            var trackingInfo = await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
            trackingInfo.State = EntityState.Detached;

            var sut = new UpdateEmailHandler(userRepository: repository, unitOfWork: context);

            var request = _fixture.MakeUpdateEmailRequest(userId: user.UserId);
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
            userDb?.Email.Should().Be(request.Email);
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