using Arahk.CMS.Api.Authentication.Mock;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Certificate;

namespace Arahk.CMS.Api.Authentication;

public static class AuthenticationExtensions
{
    public static IServiceCollection AddCMSAuthentication(this IServiceCollection services)
    {
        services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = MockAuthenticationDefaults.SchemaName;
            opt.DefaultChallengeScheme = CertificateAuthenticationDefaults.AuthenticationScheme;
            opt.DefaultScheme = CertificateAuthenticationDefaults.AuthenticationScheme;
        })
        .AddScheme<AuthenticationSchemeOptions,MockAuthenticationHandler>(MockAuthenticationDefaults.SchemaName, cfg => {})
        .AddCertificate(CertificateAuthenticationDefaults.AuthenticationScheme, cfg =>
        {
            cfg.AllowedCertificateTypes = CertificateTypes.All;
            cfg.Events = new()
            {
                OnChallenge = (validateContext) =>
                {
                    return Task.CompletedTask;
                },
                OnAuthenticationFailed = (validateContext) =>
                {
                    return Task.CompletedTask;
                },
                OnCertificateValidated = (validateContext) =>
                {
                    return Task.CompletedTask;
                }
            };
        });

        return services;
    }
}