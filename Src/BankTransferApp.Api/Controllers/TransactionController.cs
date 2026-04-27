using BankTransferApp.Application.Handlers.Transactions.DepositMoney;
using BankTransferApp.Application.Handlers.Transactions.TransferMoney;
using BankTransferApp.Application.Handlers.Transactions.WithdrawMoney;
using BankTransferApp.Domain.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankTransferApp.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
public class TransactionController : ControllerBase
{
    [HttpPost("Deposit")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DepositMoney(
        [FromServices] DepositMoneyHandler handler,
        [FromBody] DepositMoneyCommand command,
        CancellationToken cancellationToken)
    {
        var response = await handler.HandleAsync(command, cancellationToken);
        if (response.IsValid) return Ok();
        return BadRequest(response);
    }

    [Authorize]
    [HttpPost("Withdraw")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> WithdrawMoney(
        [FromServices] WithdrawMoneyHandler handler,
        [FromBody] WithdrawMoneyCommand command,
        CancellationToken cancellationToken)
    {
        var response = await handler.HandleAsync(command, cancellationToken);
        if (response.IsValid) return Ok();
        return BadRequest(response);
    }

    [Authorize]
    [HttpPost("Transfer")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> TransferMoney(
        [FromServices] TransferMoneyHandler handler,
        [FromBody] TransferMoneyCommand command,
        CancellationToken cancellationToken)
    {
        var response = await handler.HandleAsync(command, cancellationToken);
        if (response.IsValid) return Ok();
        return BadRequest(response);
    }
}
