using Finance.Application.UseCases.Tag.Commons;
using Finance.Application.UseCases.Tag.CreateTag;
using Finance.Application.UseCases.Tag.DisableTag;
using Finance.Application.UseCases.Tag.EnableTag;
using Finance.Application.UseCases.Tag.GetTag;
using Finance.Application.UseCases.Tag.SerachTags;
using Finance.Application.UseCases.Tag.UpdateTag;
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

        [HttpPut("{userId:guid}/{tagId:guid}/disable")]
        [ProducesResponseType(typeof(TagResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Disable(
            [FromRoute] Guid userId,
            [FromRoute] Guid tagId,
            CancellationToken cancellationToken = default)
        {
            var request = new DisableTagRequest(userId: userId, tagId: tagId);
            var response = await mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpPut("{userId:guid}/{tagId:guid}/enable")]
        [ProducesResponseType(typeof(TagResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Enable(
            [FromRoute] Guid userId,
            [FromRoute] Guid tagId,
            CancellationToken cancellationToken = default)
        {
            var request = new EnableTagRequest(userId: userId, tagId: tagId);
            var response = await mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpGet("{userId:guid}/{tagId:guid}")]
        [ProducesResponseType(typeof(TagResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(
            [FromRoute] Guid userId,
            [FromRoute] Guid tagId,
            CancellationToken cancellationToken = default)
        {
            var request = new GetTagRequest(userId: userId, tagId: tagId);
            var response = await mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(SearchTagsResponse), StatusCodes.Status200OK)]
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
