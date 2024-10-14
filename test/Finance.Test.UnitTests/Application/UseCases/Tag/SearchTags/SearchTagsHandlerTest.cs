using Finance.Application.UseCases.Tag.SearchTags;
using Finance.Domain.Exceptions;
using Finance.Domain.Repositories;
using Finance.Domain.SeedWork;
using FluentAssertions;
using Moq;

namespace Finance.Test.UnitTests.Application.UseCases.Tag.SearchTags
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
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<bool?>(),
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

        [Fact(DisplayName = nameof(ShouldReturnTheCorrectResponseIfSearchAsyncReturnsTagList))]
        [Trait("Unit/UseCase", "Tag - SearchTags")]
        public async Task ShouldReturnTheCorrectResponseIfSearchAsyncReturnsTagList()
        {
            var searchResponse = _fixture.MakeSearchResponse();
            _tagRepositoryMock
                .Setup(x => x.SearchAsync(
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<bool?>(),
                    It.IsAny<string?>(),
                    It.IsAny<SearchOrder>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(searchResponse);

            var request = _fixture.MakeSearchTagsRequest();
            var response = await _sut.Handle(request, _fixture.CancellationToken);

            response.Order.Should().Be(request.Order);
            response.Page.Should().Be(request.Page);
            response.PerPage.Should().Be(request.PerPage);
            response.Items.ToList().ForEach(responseItem =>
            {
                var item = searchResponse.Items.FirstOrDefault(x => x.Id == responseItem.Id);
                item?.Name.Should().Be(responseItem.Name);
                item?.Active.Should().Be(responseItem.Active);
                item?.CreatedAt.Should().Be(responseItem.CreatedAt);
                item?.UpdatedAt.Should().Be(responseItem.UpdatedAt);
            });
        }
    }
}
