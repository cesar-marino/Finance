using Finance.Application.UseCases.Category.DisableCategory;
using Finance.Domain.Exceptions;
using Finance.Infrastructure.Database.Repositories;
using FluentAssertions;

namespace Finance.Test.IntegrationTest.Application.UseCase.Category.DisableCategory
{
    public class DisableCategoryHandlerTest(DisableCategoryHandlerTestFixture fixture) : IClassFixture<DisableCategoryHandlerTestFixture>
    {
        private readonly DisableCategoryHandlerTestFixture _fixture = fixture;

        [Fact(DisplayName = nameof(ShouldThrowNotFoundException))]
        public async Task ShouldThrowNotFoundException()
        {
            var context = _fixture.MakeFinanceContext();
            var repository = new CategoryRepository(context);

            var sut = new DisableCategoryHandler(categoryRepository: repository, unitOfWork: context);

            var request = _fixture.MakeDisableCategoryRequest();
            var act = () => sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<NotFoundException>()
                .Where(x => x.Code == "not-found")
                .WithMessage("Category not found");
        }
    }
}