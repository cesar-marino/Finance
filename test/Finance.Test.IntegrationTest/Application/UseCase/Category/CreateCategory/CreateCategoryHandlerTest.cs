using Finance.Application.UseCases.Category.CreateCategory;
using Finance.Domain.Exceptions;
using Finance.Infrastructure.Database.Repositories;
using FluentAssertions;

namespace Finance.Test.IntegrationTest.Application.UseCase.Category.CreateCategory
{
    public class CreateCategoryHandlerTest(CreateCategoryHandlerTestFixture fixture) : IClassFixture<CreateCategoryHandlerTestFixture>
    {
        private readonly CreateCategoryHandlerTestFixture _fixture = fixture;

        [Fact(DisplayName = nameof(ShouldThrowNotFoundException))]
        [Trait("Integration/UseCase", "Category - CreateCategory")]
        public async Task ShouldThrowNotFoundException()
        {
            var context = _fixture.MakeFinanceContext();
            var repository = new CategoryRepository(context);

            var sut = new CreateCategoryHandler(categoryRepository: repository, unitOfWork: context);

            var request = _fixture.MakeCreateCategoryRequest();
            var act = () => sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<NotFoundException>()
                .Where(x => x.Code == "not-found")
                .WithMessage("Account not found");
        }
    }
}