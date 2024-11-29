using Finance.Application.UseCases.Goal.Commons;
using Finance.Application.UseCases.Goal.CreateGoal;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Finance.Presentation.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GoalController(IMediator mediator) : Controller
    {
        [HttpPost]
        [ProducesResponseType(typeof(GoalResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CreateGoalRequest request, CancellationToken cancellationToken = default)
        {
            var response = await mediator.Send(request, cancellationToken);
            return StatusCode(201, response);
        }
    }
}