using Finance.Application.UseCases.User.RevokeAllAccess;
using Finance.Domain.Exceptions;
using Finance.Infrastructure.Database.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace Finance.Test.IntegrationTest.Application.UseCase.User.RevokeAllAccess
{
    public class RevokeAllAccessHandlerTest(RevokeAllAccessHandlerTestFixture fixture) : IClassFixture<RevokeAllAccessHandlerTestFixture>
    {
        private readonly RevokeAllAccessHandlerTestFixture _fixture = fixture;

        [Fact(DisplayName = nameof(ShouldThrowUnexpectedException))]
        [Trait("Integration/UseCase", "User - RevokeAllAccess")]
        public async Task ShouldThrowUnexpectedException()
        {
            var context = _fixture.MakeFinanceContext();
            var repository = new UserRepository(context);

            var sut = new RevokeAllAccessHandler(userRepository: repository, unitOfWork: context);

            await context.DisposeAsync();

            var request = _fixture.MakeRevokeAllAccessRequest();
            var act = () => sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }

        [Fact(DisplayName = nameof(ShouldRevokeAllAccessSuccessfully))]
        [Trait("Integration/UseCase", "User - RevokeAllAccess")]
        public async Task ShouldRevokeAllAccessSuccessfully()
        {
            var context = _fixture.MakeFinanceContext();
            var repository = new UserRepository(context);

            var users = _fixture.MakeAccounModelList();
            await context.Users.AddRangeAsync(users);
            await context.SaveChangesAsync();

            var sut = new RevokeAllAccessHandler(userRepository: repository, unitOfWork: context);

            var request = _fixture.MakeRevokeAllAccessRequest();
            await sut.Handle(request, _fixture.CancellationToken);

            var usersDb = await context.Users.ToListAsync();
            usersDb.Should().NotBeNull();
            usersDb.ForEach((user) =>
            {
                user?.AccessTokenExpiresIn.Should().BeNull();
                user?.AccessTokenValue.Should().BeNull();
                user?.RefreshTokenExpiresIn.Should().BeNull();
                user?.RefreshTokenValue.Should().BeNull();
            });
        }
    }
}