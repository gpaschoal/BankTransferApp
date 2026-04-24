using BankTransferApp.Application.Handlers.Auth.UserSignIn;
using BankTransferApp.Domain.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankTransferApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class AuthenticationController : Controller
{
    [AllowAnonymous]
    [HttpPost]
    [Route("UserSignIn")]
    [ProducesResponseType(typeof(CustomResultData<Guid>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CustomResultData), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(CustomResultData), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SignIn(
        [FromServices] UserSignInHandler handler,
        [FromBody] UserSignInCommand command,
        CancellationToken cancellationToken)
    {
        var response = await handler.HandleAsync(command, cancellationToken);

        if (response.IsValid)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }
}
