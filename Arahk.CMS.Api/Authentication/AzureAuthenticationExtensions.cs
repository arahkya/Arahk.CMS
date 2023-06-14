using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;

namespace Arahk.CMS.Api.Authentication;

public static class AzureAuthenticationExtensions
{
    public static IServiceCollection AddAzureAuthentication(this IServiceCollection services)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddMicrosoftIdentityWebApi(jwtBearerOptions => { }, microsoftIdentityOptions =>
        {
            string azureInstance = Environment.GetEnvironmentVariable("AZURE_AD_INSTANCE")!;
            string ClientId = Environment.GetEnvironmentVariable("AZURE_AD_CLIENTID")!;
            string TenantId = Environment.GetEnvironmentVariable("AZURE_AD_TENANTID")!;

            microsoftIdentityOptions.Instance = azureInstance;
            microsoftIdentityOptions.ClientId = ClientId;
            microsoftIdentityOptions.TenantId = TenantId;
        });

        return services;
    }
}