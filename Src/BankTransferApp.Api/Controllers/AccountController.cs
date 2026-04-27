using BankTransferApp.Application.Handlers.Account.ActivateAccount;
using BankTransferApp.Application.Handlers.Account.CreateAccount;
using BankTransferApp.Application.Handlers.Account.DeactivateAccount;
using BankTransferApp.Domain.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankTransferApp.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
public class AccountController : ControllerBase
{
    [Authorize]
    [HttpPost]
    [ProducesResponseType(typeof(Result<Guid>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAccount(
        [FromServices] CreateAccountHandler handler,
        [FromBody] CreateAccountCommand command,
        CancellationToken cancellationToken)
    {
        var response = await handler.HandleAsync(command, cancellationToken);
        if (response.IsValid) return Ok(response);
        return BadRequest(response);
    }

    [Authorize]
    [HttpPost("Deactivate")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeactivateAccount(
        [FromServices] DeactivateAccountHandler handler,
        [FromBody] DeactivateAccountCommand command,
        CancellationToken cancellationToken)
    {
        var response = await handler.HandleAsync(command, cancellationToken);
        if (response.IsValid) return Ok();
        return BadRequest(response);
    }

    [Authorize]
    [HttpPost("Activate")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ActivateAccount(
        [FromServices] ActivateAccountHandler handler,
        [FromBody] ActivateAccountCommand command,
        CancellationToken cancellationToken)
    {
        var response = await handler.HandleAsync(command, cancellationToken);
        if (response.IsValid) return Ok();
        return BadRequest(response);
    }
}
