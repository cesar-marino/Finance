using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Finance.Presentation.API.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class FileController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }

        [HttpPost("{id}")]
        public IActionResult Create(
            [FromRoute] string id,
            [FromForm] IFormFile? file,
            CancellationToken cancellationToken = default)
        {
            if (file != null)
                return Ok($"{id}: file is not null");

            return Ok($"id: {id}");
        }
    }
}