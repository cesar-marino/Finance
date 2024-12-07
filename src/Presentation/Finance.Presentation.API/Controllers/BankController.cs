using Microsoft.AspNetCore.Mvc;

namespace Finance.Presentation.API.Controllers
{
    public class BankController : Controller
    {
        public async Task<IActionResult> Create([FromBody] IFormFile file)
        {
            await Task.CompletedTask;
            return Ok();
        }
    }
}