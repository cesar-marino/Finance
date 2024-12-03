using Finance.Application.UseCases.Tag.UpdateTag;
using Finance.Domain.Entities;
using Finance.Domain.Exceptions;
using Finance.Domain.Repositories;
using Finance.Domain.SeedWork;
using FluentAssertions;
using Moq;

namespace Finance.Test.UnitTest.Application.UseCases.Tag.UpdateTag
{
    public class UpdateTagHandlerTest : IClassFixture<UpdateTagHandlerTestFixture>
    {
        private readonly UpdateTagHandlerTestFixture _fixture;
        private readonly UpdateTagHandler _sut;
        private readonly Mock<ITagRepository> _tagRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public UpdateTagHandlerTest(UpdateTagHandlerTestFixture fixture)
        {
            _fixture = fixture;
            _tagRepositoryMock = new();
            _unitOfWorkMock = new();

            _sut = new(
                tagRepository: _tagRepositoryMock.Object,
                unitOfWork: _unitOfWorkMock.Object);
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatFindAsyncThrows))]
        [Trait("Unit/UseCase", "Tag - UpdateTag")]
        public async Task ShouldRethrowSameExceptionThatFindAsyncThrows()
        {
            _tagRepositoryMock
                .Setup(x => x.FindAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new NotFoundException("Tag"));

            var request = _fixture.MakeUpdateTagRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<NotFoundException>()
                .Where(x => x.Code == "not-found")
                .WithMessage("Tag not found");
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatUpdateAsyncThrows))]
        [Trait("Unit/UseCase", "Tag - UpdateTag")]
        public async Task ShouldRethrowSameExceptionThatUpdateAsyncThrows()
        {
            var tag = _fixture.MakeTagEntity();
            _tagRepositoryMock
                .Setup(x => x.FindAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(tag);

            _tagRepositoryMock
                .Setup(x => x.UpdateAsync(
                    It.IsAny<TagEntity>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new UnexpectedException());

            var request = _fixture.MakeUpdateTagRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatCommitAsyncThrows))]
        [Trait("Unit/UseCase", "Tag - UpdateTag")]
        public async Task ShouldRethrowSameExceptionThatCommitAsyncThrows()
        {
            var tag = _fixture.MakeTagEntity();
            _tagRepositoryMock
                .Setup(x => x.FindAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(tag);

            _unitOfWorkMock
                .Setup(x => x.CommitAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(new UnexpectedException());

            var request = _fixture.MakeUpdateTagRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }

        [Fact(DisplayName = nameof(ShouldReturnTheCorrectResponseIfTagIsUpdatedSuccessfully))]
        [Trait("Unit/UseCase", "Tag - UpdateTag")]
        public async Task ShouldReturnTheCorrectResponseIfTagIsUpdatedSuccessfully()
        {
            var tag = _fixture.MakeTagEntity();
            _tagRepositoryMock
                .Setup(x => x.FindAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(tag);

            var request = _fixture.MakeUpdateTagRequest();
            var response = await _sut.Handle(request, _fixture.CancellationToken);

            response.UserId.Should().Be(tag.UserId);
            response.Active.Should().Be(tag.Active);
            response.CreatedAt.Should().Be(tag.CreatedAt);
            response.Name.Should().Be(request.Name);
            response.TagId.Should().Be(tag.Id);
        }
    }
}
