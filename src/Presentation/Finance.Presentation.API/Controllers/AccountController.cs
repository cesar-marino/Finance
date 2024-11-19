using Finance.Application.UseCases.Account.Authentication;
using Finance.Application.UseCases.Account.Commons;
using Finance.Application.UseCases.Account.CreateAccount;
using Finance.Application.UseCases.Account.DisableAccount;
using Finance.Application.UseCases.Account.EnableAccount;
using Finance.Application.UseCases.Account.GetCurrentAccount;
using Finance.Application.UseCases.Account.RefreshToken;
using Finance.Application.UseCases.Account.RevokeAccess;
using Finance.Application.UseCases.Account.RevokeAllAccess;
using Finance.Application.UseCases.Account.UpdateEmail;
using Finance.Application.UseCases.Account.UpdatePassword;
using Finance.Application.UseCases.Account.UpdateUsername;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Finance.Presentation.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController(IMediator mediator) : Controller
    {
        [HttpPost("login")]
        [ProducesResponseType(typeof(AccountResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login(
            [FromBody] AuthenticationRequest request,
            CancellationToken cancellationToken = default)
        {
            var response = await mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpPost("create")]
        [ProducesResponseType(typeof(AccountResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create(
            [FromBody] CreateAccountRequest request,
            CancellationToken cancellationToken = default)
        {
            var response = await mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpPut("{accountId:Guid}/disable")]
        [ProducesResponseType(typeof(AccountResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Disable(
            [FromRoute] Guid accountId,
            CancellationToken cancellationToken = default)
        {
            var request = new DisableAccountRequest(accountId: accountId);
            var response = await mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpPut("{accountId:Guid}/enable")]
        [ProducesResponseType(typeof(AccountResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Enable(
            [FromRoute] Guid accountId,
            CancellationToken cancellationToken = default)
        {
            var request = new EnableAccountRequest(accountId: accountId);
            var response = await mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpGet("current_account")]
        [ProducesResponseType(typeof(AccountResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CurrentAccount(
            [FromHeader] string accessToken,
            CancellationToken cancellationToken = default)
        {
            var request = new GetCurrentAccountRequest(accessToken: accessToken);
            var response = await mediator.Send(request, cancellationToken);
            return Ok(response);
        }


        [HttpPut("refresh")]
        [ProducesResponseType(typeof(AccountResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Refresh(
            [FromHeader] string accessToken,
            [FromHeader] string refreshToken,
            CancellationToken cancellationToken = default)
        {
            var request = new RefreshTokenRequest(accessToken: accessToken, refreshToken: refreshToken);
            var response = await mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpPut("{accountId:Guid}/revoke")]
        [ProducesResponseType(typeof(AccountResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Revoke(
            [FromRoute] Guid accountId,
            CancellationToken cancellationToken = default)
        {
            var request = new RevokeAccessRequest(accountId: accountId);
            var response = await mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpPut("revoke_all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RevokeAll(
            [FromRoute] Guid accountId,
            CancellationToken cancellationToken = default)
        {
            var request = new RevokeAllAccessRequest();
            await mediator.Send(request, cancellationToken);
            return Ok();
        }

        [HttpPut("update_email")]
        [ProducesResponseType(typeof(AccountResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateEmail(
            [FromBody] UpdateEmailRequest request,
            CancellationToken cancellationToken = default)
        {
            var response = await mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpPut("update_password")]
        [ProducesResponseType(typeof(AccountResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdatePassword(
            [FromBody] UpdatePasswordRequest request,
            CancellationToken cancellationToken = default)
        {
            var response = await mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpPut("update_username")]
        [ProducesResponseType(typeof(AccountResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateUsername(
            [FromBody] UpdateUsernameRequest request,
            CancellationToken cancellationToken = default)
        {
            var response = await mediator.Send(request, cancellationToken);
            return Ok(response);
        }
    }
}