using BankTransferApp.Application.Handlers.Account.CreateAccount;
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
}
