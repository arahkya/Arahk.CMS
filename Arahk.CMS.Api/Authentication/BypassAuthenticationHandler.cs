using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace Arahk.CMS.Api.Authentication;

public class TestAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    public TestAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> option, ILoggerFactory loggerFactory, UrlEncoder urlEncoder, ISystemClock systemClock)
        : base(option, loggerFactory, urlEncoder, systemClock)
    {

    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var claims = new[] { new Claim(ClaimTypes.Name, "Test user") };
        var identity = new ClaimsIdentity(claims, "Test");
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, BypassAuthenticationDefaults.SchemaName);

        var result = AuthenticateResult.Success(ticket);

        await Task.CompletedTask;

        return result;
    }
}