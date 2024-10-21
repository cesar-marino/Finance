using Finance.Application.UseCases.Tag.GetTag;
using Finance.Domain.Entities;
using Finance.Domain.Exceptions;
using Finance.Domain.Repositories;
using FluentAssertions;
using Moq;

namespace Finance.Test.UnitTest.Application.UseCases.Tag.GetTag
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

            _sut = new(
                tagRepository: _tagRepositoryMock.Object);
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatFindAsyncThrows))]
        [Trait("Unit/UseCase", "Tag - GetTag")]
        public async Task ShouldRethrowSameExceptionThatFindAsyncThrows()
        {
            _tagRepositoryMock
                .Setup(x => x.FindAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new NotFoundException("Tag"));

            var request = _fixture.MakeGetTagRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<NotFoundException>()
                .Where(x => x.Code == "not-found")
                .WithMessage("Tag not found");
        }

        [Fact(DisplayName = nameof(ShouldReturnTheCorrectResponseIfFindAsyncReturnsTag))]
        [Trait("Unit/UseCase", "Tag - GetTag")]
        public async Task ShouldReturnTheCorrectResponseIfFindAsyncReturnsTag()
        {
            var tag = _fixture.MakeTagEntity();
            _tagRepositoryMock
                .Setup(x => x.FindAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(tag);

            var request = _fixture.MakeGetTagRequest();
            var response = await _sut.Handle(request, _fixture.CancellationToken);

            response.AccountId.Should().Be(tag.AccountId);
            response.Active.Should().Be(tag.Active);
            response.CreatedAt.Should().Be(tag.CreatedAt);
            response.Name.Should().Be(tag.Name);
            response.TagId.Should().Be(tag.Id);
            response.UpdatedAt.Should().Be(tag.UpdatedAt);
        }
    }
}
