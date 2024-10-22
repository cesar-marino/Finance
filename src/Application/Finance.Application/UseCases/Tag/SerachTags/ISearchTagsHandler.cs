using MediatR;

namespace Finance.Application.UseCases.Tag.SerachTags
{
    public interface ISearchTagsHandler : IRequestHandler<SearchTagsRequest, SearchTagsResponse>
    {
    }
}
