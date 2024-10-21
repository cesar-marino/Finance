using MediatR;

namespace Finance.Test.UnitTest.Application.UseCases.Tag.SearchTags
{
    public interface ISearchTagsHandler : IRequestHandler<SearchTagsRequest, SearchTagsResponse>
    {
    }
}
