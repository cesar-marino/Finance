using Finance.Application.UseCases.Tag.UpdateTag;
using Finance.Domain.Exceptions;
using Finance.Domain.Repositories;
using FluentAssertions;
using Moq;

namespace Finance.Test.UnitTests.Application.UseCases.Tag.UpdateTag
{
    public class UpdateTagHandlerTest : IClassFixture<UpdateTagHandlerTestFixture>
    {
        private readonly UpdateTagHandlerTestFixture _fixture;
        private readonly UpdateTagHandler _sut;
        private readonly Mock<ITagRepository> _tagRepositoryMock;

        public UpdateTagHandlerTest(UpdateTagHandlerTestFixture fixture)
        {
            _fixture = fixture;
            _tagRepositoryMock = new();

            _sut = new(
                tagRepository: _tagRepositoryMock.Object);
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatFindAsyncThrows))]
        [Trait("Unit/UseCase", "Tag - UpdateTag")]
        public async Task ShouldRethrowSameExceptionThatFindAsyncThrows()
        {
            _tagRepositoryMock
                .Setup(x => x.FindAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new NotFoundException("Tag"));

            var request = _fixture.MakeUpdateTagRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<NotFoundException>()
                .Where(x => x.Code == "not-found")
                .WithMessage("Tag not found");
        }
    }
}
