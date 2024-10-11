using Finance.Application.UseCases.Tag.CreateTag;
using Finance.Domain.Entities;
using Finance.Domain.Exceptions;
using Finance.Domain.Repositories;
using Finance.Domain.SeedWork;
using FluentAssertions;
using Moq;

namespace Finance.Test.UnitTests.Application.UseCases.Tag.CreateTag
{
    public class CreateTagHandlerTest : IClassFixture<CreateTagHandlerTestFixture>
    {
        private readonly CreateTagHandlerTestFixture _fixture;
        private readonly CreateTagHandler _sut;
        private readonly Mock<ITagRepository> _tagRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWork;

        public CreateTagHandlerTest(CreateTagHandlerTestFixture fixture)
        {
            _fixture = fixture;
            _tagRepositoryMock = new();
            _unitOfWork = new();

            _sut = new(
                tagRepository: _tagRepositoryMock.Object,
                unitOfWork: _unitOfWork.Object);
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatInsertAsyncThrows))]
        [Trait("Unit/UseCase", "Tag - CreateTag")]
        public async Task ShouldRethrowSameExceptionThatInsertAsyncThrows()
        {
            _tagRepositoryMock
                .Setup(x => x.InsertAsync(It.IsAny<TagEntity>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new UnexpectedException());

            var request = _fixture.MakeCreateTagRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatCommitAsyncThrows))]
        [Trait("Unit/UseCase", "Tag - CreateTag")]
        public async Task ShouldRethrowSameExceptionThatCommitAsyncThrows()
        {
            _unitOfWork
                .Setup(x => x.CommitAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(new UnexpectedException());

            var request = _fixture.MakeCreateTagRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }
    }
}
