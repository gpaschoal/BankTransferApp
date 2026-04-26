using BankTransferApp.Application.Constants;
using BankTransferApp.Domain.Services;
using System.Security.Claims;

namespace BankTransferApp.Api.Middlewares;

public class UserContextMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context, IUserContextService userContext, IHttpContextAccessor httpContextAccessor)
    {
        var user = httpContextAccessor.HttpContext!.User;

        var identity = (ClaimsIdentity)user.Identity;

        var userIdClaim = identity?.Claims?.SingleOrDefault(x => x.Type == JwtCustomClaims.USER_IDENTIFIER);

        if (!string.IsNullOrWhiteSpace(userIdClaim?.Value))
        {
            userContext.SetCurrentUserId(Guid.Parse(userIdClaim.Value));
        }

        await next(context);
    }
}
