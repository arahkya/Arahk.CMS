using Microsoft.AspNetCore.Authentication;

namespace Arahk.CMS.Api.Authentication;

public static class ByPassAuthenticationExtensions
{
    public static IServiceCollection AddBypassAuthentication(this IServiceCollection services)
    {
        services.AddAuthentication(BypassAuthenticationDefaults.SchemaName).AddScheme<AuthenticationSchemeOptions, TestAuthenticationHandler>(BypassAuthenticationDefaults.SchemaName, option => { });

        return services;
    }
}