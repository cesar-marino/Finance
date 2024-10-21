using Finance.Application.UseCases.Tag.GetTag;
using Finance.Domain.Exceptions;
using Finance.Domain.Repositories;
using FluentAssertions;
using Moq;

namespace Finance.Test.UnitTests.Application.UseCases.Tag.GetTag
{
    public class GetTagHandlerTest : IClassFixture<GetTagHandlerTestFixture>
    {
        private readonly GetTagHandlerTestFixture _fixture;
        private readonly GetTagHandler _sut;
        private readonly Mock<ITagRepository> _tagRepositoryMock;

        public GetTagHandlerTest(GetTagHandlerTestFixture fixture)
        {
            _fixture = fixture;
            _tagRepositoryMock = new();

            _sut = new(tagRepository: _tagRepositoryMock.Object);
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatFindAsyncThrows))]
        [Trait("Unit/UseCase", "Tag - GetTag")]
        public async Task ShouldRethrowSameExceptionThatFindAsyncThrows()
        {
            _tagRepositoryMock
                .Setup(x => x.FindAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new NotFoundException("Tag"));

            var request = _fixture.MakeGetTagRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<NotFoundException>()
                .Where(x => x.Code == "not-found")
                .WithMessage("Tag not found");
        }
    }
}