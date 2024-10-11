using Finance.Application.UseCases.Tag.EnabledTag;
using Finance.Domain.Entities;
using Finance.Domain.Exceptions;
using Finance.Domain.Repositories;
using Finance.Domain.SeedWork;
using FluentAssertions;
using Moq;

namespace Finance.Test.UnitTests.Application.UseCases.Tag.EnabledTag
{
    public class EnabledTagHandlerTest : IClassFixture<EnabledTagHandlerTestFixture>
    {
        private readonly EnabledTagHandlerTestFixture _fixture;
        private readonly EnabledTagHandler _sut;
        private readonly Mock<ITagRepository> _tagRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public EnabledTagHandlerTest(EnabledTagHandlerTestFixture fixture)
        {
            _fixture = fixture;
            _tagRepositoryMock = new();
            _unitOfWorkMock = new();

            _sut = new(
                tagRepository: _tagRepositoryMock.Object,
                unitOfWork: _unitOfWorkMock.Object);
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

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatUpdateAsyncThrows))]
        [Trait("Unit/UseCase", "Tag - EnabledTag")]
        public async Task ShouldRethrowSameExceptionThatUpdateAsyncThrows()
        {
            var tag = _fixture.MakeTagEntity();
            _tagRepositoryMock
                .Setup(x => x.FindAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(tag);

            _tagRepositoryMock
                .Setup(x => x.UpdateAsync(It.IsAny<TagEntity>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new UnexpectedException());

            var request = _fixture.MakeEnableTagRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatCommitAsyncThrows))]
        [Trait("Unit/UseCase", "Tag - EnabledTag")]
        public async Task ShouldRethrowSameExceptionThatCommitAsyncThrows()
        {
            var tag = _fixture.MakeTagEntity();
            _tagRepositoryMock
                .Setup(x => x.FindAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(tag);

            _unitOfWorkMock
                .Setup(x => x.CommitAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(new UnexpectedException());

            var request = _fixture.MakeEnableTagRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }
    }
}
