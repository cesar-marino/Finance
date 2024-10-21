using Finance.Application.UseCases.Tag.CreateTag;
using Finance.Domain.Entities;
using Finance.Domain.Exceptions;
using Finance.Domain.Repositories;
using Finance.Domain.SeedWork;
using FluentAssertions;
using Moq;

namespace Finance.Test.UnitTest.Application.UseCases.Tag.CreateTag
{
    public class CreateTagHandlerTest : IClassFixture<CreateTagHandlerTestFixture>
    {
        private readonly CreateTagHandlerTestFixture _fixture;
        private readonly CreateTagHandler _sut;
        private readonly Mock<ITagRepository> _tagRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public CreateTagHandlerTest(CreateTagHandlerTestFixture fixture)
        {
            _fixture = fixture;
            _tagRepositoryMock = new();
            _unitOfWorkMock = new();

            _sut = new(
                tagRepository: _tagRepositoryMock.Object,
                unitOfWork: _unitOfWorkMock.Object);
        }

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatInsertAsynThrows))]
        [Trait("Unit/UseCase", "Tag - CreateTag")]
        public async Task ShouldRethrowSameExceptionThatInsertAsynThrows()
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

        [Fact(DisplayName = nameof(ShouldRethrowSameExceptionThatInsertAsynThrows))]
        [Trait("Unit/UseCase", "Tag - CreateTag")]
        public async Task ShouldRethrowSameExceptionThatCommitAsyncAsynThrows()
        {
            _unitOfWorkMock
                .Setup(x => x.CommitAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(new UnexpectedException());

            var request = _fixture.MakeCreateTagRequest();
            var act = () => _sut.Handle(request, _fixture.CancellationToken);

            await act.Should().ThrowExactlyAsync<UnexpectedException>()
                .Where(x => x.Code == "unexpected")
                .WithMessage("An unexpected error occurred");
        }

        [Fact(DisplayName = nameof(ShouldReturnTheCorrectResponseIfTagIsAddedSucccessfully))]
        [Trait("Unit/UseCase", "Tag - CreateTag")]
        public async Task ShouldReturnTheCorrectResponseIfTagIsAddedSucccessfully()
        {
            var request = _fixture.MakeCreateTagRequest();
            var response = await _sut.Handle(request, _fixture.CancellationToken);

            response.AccountId.Should().Be(request.AccountId);
            response.Active.Should().BeTrue();
            response.Name.Should().Be(request.Name);
        }
    }
}
