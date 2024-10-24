using Finance.Application.UseCases.Category.Commons;
using Finance.Application.UseCases.Category.CreateCategory;
using Finance.Application.UseCases.Category.DisableCategory;
using Finance.Application.UseCases.Category.EnableCategory;
using Finance.Application.UseCases.Category.GetCategory;
using Finance.Application.UseCases.Category.SearchCategories;
using Finance.Application.UseCases.Category.UpdateCategory;
using Finance.Domain.Enums;
using Finance.Domain.SeedWork;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Finance.Presentation.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoryController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(CategoryResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create(
            [FromBody] CreateCategoryRequest request,
            CancellationToken cancellationToken = default)
        {
            var response = await mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpPut("{accountId:Guid}/{categoryId:Guid}/disable")]
        [ProducesResponseType(typeof(CategoryResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Disable(
            [FromRoute] Guid accountId,
            [FromRoute] Guid categoryId,
            CancellationToken cancellationToken = default)
        {
            var request = new DisableCategoryRequest(accountId: accountId, categoryId: categoryId);
            var response = await mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpPut("{accountId:Guid}/{categoryId:Guid}/enable")]
        [ProducesResponseType(typeof(CategoryResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Enable(
            [FromRoute] Guid accountId,
            [FromRoute] Guid categoryId,
            CancellationToken cancellationToken = default)
        {
            var request = new EnableCategoryRequest(accountId: accountId, categoryId: categoryId);
            var response = await mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpGet("{accountId:Guid}/{categoryId:Guid}")]
        [ProducesResponseType(typeof(CategoryResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(
            [FromRoute] Guid accountId,
            [FromRoute] Guid categoryId,
            CancellationToken cancellationToken = default)
        {
            var request = new GetCategoryRequest(accountId: accountId, categoryId: categoryId);
            var response = await mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(SearchCategoriesResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Search(
            [FromQuery] bool? active = null,
            [FromQuery] CategoryType? categoryType = null,
            [FromQuery] string? name = null,
            [FromQuery(Name = "current_page")] int? currentPage = null,
            [FromQuery(Name = "per_page")] int? perPage = null,
            [FromQuery] string? orderBy = null,
            [FromQuery] SearchOrder? order = null,
            CancellationToken cancellationToken = default)
        {
            var request = new SearchCategoriesRequest(
                currentPage: currentPage,
                perPage: perPage,
                orderBy: orderBy,
                order: order,
                active: active,
                name: name,
                categoryType: categoryType);

            var response = await mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpPut]
        [ProducesResponseType(typeof(CategoryResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(
            [FromBody] UpdateCategoryRequest request,
            CancellationToken cancellationToken = default)
        {
            var response = await mediator.Send(request, cancellationToken);
            return Ok(response);
        }
    }
}
