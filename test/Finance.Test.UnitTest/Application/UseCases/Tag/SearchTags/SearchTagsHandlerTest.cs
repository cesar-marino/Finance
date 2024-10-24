using Finance.Application.UseCases.Tag.SerachTags;
using Finance.Domain.Exceptions;
using Finance.Domain.Repositories;
using Finance.Domain.SeedWork;
using FluentAssertions;
using Moq;

namespace Finance.Test.UnitTest.Application.UseCases.Tag.SearchTags
{
    public class SearchTagsHandlerTest : IClassFixture<SearchTagsHandlerTestFixture>
    {
        private readonly SearchTagsHandlerTestFixture _fixture;
        private readonly SearchTagsHandler _sut;
        private readonly Mock<ITagRepository> _tagRepositoryMock;

        public SearchTagsHandlerTest(SearchTagsHandlerTestFixture fixture)
        {
            _fixture = fixture;
            _tagRepositoryMock = new();

            _sut = new(
                tagRepository: _tagRepositoryMock.Object);
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatSearchAsyncThrows))]
        [Trait("Unit/UseCase", "Tag - SearchTags")]
        public async Task ShouldRethrowSameExceptionThatSearchAsyncThrows()
        {
            _tagRepositoryMock
                .Setup(x => x.SearchAsync(
                    It.IsAny<bool?>(),
                    It.IsAny<string?>(),
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<string?>(),
                    It.IsAny<SearchOrder>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new UnexpectedException());

            var request = _fixture.MakeSearchTagsRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }

        [Fact(DisplayName = nameof(ShouldReturnTheCorrectResponseIfSearchAsyncReturnsValidResults))]
        [Trait("Unit/UseCase", "Tag - SearchTags")]
        public async Task ShouldReturnTheCorrectResponseIfSearchAsyncReturnsValidResults()
        {
            var results = _fixture.MakeSearchResult();
            _tagRepositoryMock
                .Setup(x => x.SearchAsync(
                    It.IsAny<bool?>(),
                    It.IsAny<string?>(),
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<string?>(),
                    It.IsAny<SearchOrder>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(results);

            var request = _fixture.MakeSearchTagsRequest();
            var response = await _sut.Handle(request, _fixture.CancellationToken);

            response.CurrentPage.Should().Be(results.CurrentPage);
            response.Order.Should().Be(results.Order);
            response.OrderBy.Should().Be(results.OrderBy);
            response.PerPage.Should().Be(results.PerPage);
            response.Total.Should().Be(results.Total);

            response.Items.ToList().ForEach((item) =>
            {
                var result = results.Items.FirstOrDefault(x => x.Id == item.TagId);
                result.Should().NotBeNull();
                result!.AccountId.Should().Be(item.AccountId);
                result!.Active.Should().Be(item.Active);
                result!.CreatedAt.Should().Be(item.CreatedAt);
                result!.Id.Should().Be(item.TagId);
                result!.Name.Should().Be(item.Name);
                result!.UpdatedAt.Should().Be(item.UpdatedAt);
            });
        }
    }
}
