using Finance.Application.Commons;
using Finance.Application.UseCases.Tag.Commons;
using Finance.Application.UseCases.Tag.CreateTag;
using Finance.Application.UseCases.Tag.DisableTag;
using Finance.Application.UseCases.Tag.EnableTag;
using Finance.Application.UseCases.Tag.GetTag;
using Finance.Application.UseCases.Tag.SerachTags;
using Finance.Application.UseCases.Tag.UpdateTag;
using Finance.Domain.Entities;
using Finance.Domain.SeedWork;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Finance.Presentation.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TagController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(TagResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create(
            [FromBody] CreateTagRequest request,
            CancellationToken cancellationToken = default)
        {
            var response = await mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpGet("{accountId:guid}/{tagId:guid}/disable")]
        [ProducesResponseType(typeof(TagResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Disable(
            [FromRoute] Guid accountId,
            [FromRoute] Guid tagId,
            CancellationToken cancellationToken = default)
        {
            var request = new DisableTagRequest(accountId: accountId, tagId: tagId);
            var response = await mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpGet("{accountId:guid}/{tagId:guid}/enable")]
        [ProducesResponseType(typeof(TagResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Enable(
            [FromRoute] Guid accountId,
            [FromRoute] Guid tagId,
            CancellationToken cancellationToken = default)
        {
            var request = new EnableTagRequest(accountId: accountId, tagId: tagId);
            var response = await mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpGet("{accountId:guid}/{tagId:guid}")]
        [ProducesResponseType(typeof(TagResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(
            [FromRoute] Guid accountId,
            [FromRoute] Guid tagId,
            CancellationToken cancellationToken = default)
        {
            var request = new GetTagRequest(accountId: accountId, tagId: tagId);
            var response = await mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(SearchPaginationResponse<TagEntity>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Search(
            [FromQuery] bool? active = null,
            [FromQuery] string? name = null,
            [FromQuery(Name = "current_page")] int? currentPage = null,
            [FromQuery(Name = "per_page")] int? perPage = null,
            [FromQuery] string? orderBy = null,
            [FromQuery] SearchOrder? order = null,
            CancellationToken cancellationToken = default)
        {
            var request = new SearchTagsRequest(
                currentPage: currentPage,
                perPage: perPage,
                orderBy: orderBy,
                order: order,
                active: active,
                name: name);

            var response = await mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpPut]
        [ProducesResponseType(typeof(TagResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(
            [FromBody] UpdateTagRequest request,
            CancellationToken cancellationToken = default)
        {
            var response = await mediator.Send(request, cancellationToken);
            return Ok(response);
        }
    }
}
