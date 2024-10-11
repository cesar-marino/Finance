using Finance.Application.UseCases.Tag.EnabledTag;
using Finance.Domain.Exceptions;
using Finance.Domain.Repositories;
using FluentAssertions;
using Moq;

namespace Finance.Test.UnitTests.Application.UseCases.Tag.EnabledTag
{
    public class EnabledTagHandlerTest : IClassFixture<EnabledTagHandlerTestFixture>
    {
        private readonly EnabledTagHandlerTestFixture _fixture;
        private readonly EnabledTagHandler _sut;
        private readonly Mock<ITagRepository> _tagRepositoryMock;

        public EnabledTagHandlerTest(EnabledTagHandlerTestFixture fixture)
        {
            _fixture = fixture;
            _tagRepositoryMock = new();

            _sut = new(
                tagRepository: _tagRepositoryMock.Object);
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatFindAsyncThrows))]
        [Trait("Unit/UseCase", "Tag - EnabledTag")]
        public async Task ShouldRethrowSameExceptionThatFindAsyncThrows()
        {
            _tagRepositoryMock
                .Setup(x => x.FindAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new NotFoundException("Tag"));

            var request = _fixture.MakeEnableTagRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<NotFoundException>()
                .Where(x => x.Code == "not-found")
                .WithMessage("Tag not found");
        }
    }
}
