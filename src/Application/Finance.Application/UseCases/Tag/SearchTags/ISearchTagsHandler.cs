using MediatR;

namespace Finance.Application.UseCases.Tag.SearchTags
{
    public interface ISearchTagsHandler : IRequestHandler<SearchTagsRequest, SearchTagsResponse>
    {
    }
}
