using Finance.Application.UseCases.Tag.DisableTag;
using Finance.Domain.Exceptions;
using Finance.Domain.Repositories;
using FluentAssertions;
using Moq;

namespace Finance.Test.UnitTests.Application.UseCases.Tag.DisableTag
{
    public class DisableTagHandlerTest : IClassFixture<DisableTagHandlerTestFixture>
    {
        private readonly DisableTagHandlerTestFixture _fixtrure;
        private readonly DisableTagHandler _sut;
        private readonly Mock<ITagRepository> _tagRepositoryMock;

        public DisableTagHandlerTest(DisableTagHandlerTestFixture fixtrure)
        {
            _fixtrure = fixtrure;
            _tagRepositoryMock = new();

            _sut = new(
                tagRepository: _tagRepositoryMock.Object);
        }

        [Fact(DisplayName = nameof(ShouldThrowSameExceptionThatFindAsyncThrows))]
        [Trait("Unit/UseCase", "Tag - DisableTag")]
        public async Task ShouldThrowSameExceptionThatFindAsyncThrows()
        {
            _tagRepositoryMock
                .Setup(x => x.FindAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new NotFoundException("Tag"));

            var request = _fixtrure.MakeDisableTagRequest();
            var act = () => _sut.Handle(request, _fixtrure.CancellationToken);

            await act.Should().ThrowExactlyAsync<NotFoundException>()
                .Where(x => x.Code == "not-found")
                .WithMessage("Tag not found");
        }
    }
}
