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
    }
}
