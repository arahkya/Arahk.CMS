using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace Arahk.CMS.Api.Authentication.Mock;

public class MockAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    public MockAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> option, ILoggerFactory loggerFactory, UrlEncoder urlEncoder, ISystemClock systemClock)
        : base(option, loggerFactory, urlEncoder, systemClock)
    {

    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var claims = new[] { 
            new Claim(ClaimsNames.Name, "Arahk Yambupha"),
            new Claim(ClaimsNames.EmailAddress, "arahk@outlook.com"),
            new Claim(ClaimsNames.UserId, "1e5601b9-649b-4b59-9dbf-9db0179bd9d6")
        };
        var identity = new ClaimsIdentity(claims, MockAuthenticationDefaults.SchemaName);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, MockAuthenticationDefaults.SchemaName);

        var result = AuthenticateResult.Success(ticket);

        await Task.CompletedTask;

        return result;
    }
}