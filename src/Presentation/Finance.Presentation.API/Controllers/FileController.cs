using Finance.Application.UseCases.File.CreateFile;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Finance.Presentation.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FileController(IMediator mediator) : Controller
    {
        [HttpPost]
        public async Task<IActionResult> Create(
            IFormFile video,
            CancellationToken cancellationToken = default)
        {
            var stream = video.OpenReadStream();
            var request = new CreateFileRequest("name", stream);
            var response = await mediator.Send(request, cancellationToken);
            return StatusCode(201, response);
        }
    }
}