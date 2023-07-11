using System.Security.Claims;
using System.Text.Encodings.Web;
using Arahk.CMS.Api.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace Arahk.CMS.Api.Tests.Authentication
{
    public class TestAuthenticationScheme : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public static string DefaultName = "TestScheme";

        public TestAuthenticationScheme(
            IOptionsMonitor<AuthenticationSchemeOptions> options
            , ILoggerFactory logger
            , UrlEncoder encoder
            , ISystemClock clock
        ) : base(options, logger, encoder, clock)
        {

        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            await Task.CompletedTask;
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimsNames.Name, "Test User"),
                new Claim(ClaimsNames.EmailAddress, "test-user@test.com"),
                new Claim(ClaimsNames.UserId, "5120e192-60b4-4349-a99a-dbcf6e9a7cb1")
            }, DefaultName);
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            return AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, authenticationScheme: DefaultName));
        }
    }
}