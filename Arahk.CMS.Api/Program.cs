using System.Net;
using Serilog;
using Arahk.CMS.Application;
using Arahk.CMS.Infrastructure;
using Arahk.CMS.Application.Common;
using Arahk.CMS.Api.Services;
using Arahk.CMS.Api.Authentication;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Server.Kestrel.Https;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddCMSApplication();
builder.Services.AddCMSInfrastructure();

builder.Services.AddCMSAuthentication();

builder.Services.AddTransient<IDateTimeProvider, DateTimeProvider>();
builder.Services.AddTransient<IUserIdProvider, UserIdProvider>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddProblemDetails(problemDetailCfg =>
{
    problemDetailCfg.CustomizeProblemDetails = (problemDetailContext) =>
    {
        if (problemDetailContext.ProblemDetails.Title == typeof(FluentValidation.ValidationException).FullName)
        {
            problemDetailContext.ProblemDetails.Status = (int)HttpStatusCode.BadRequest;
        }
    };
});

builder.Services.AddHttpContextAccessor();

builder.WebHost.UseKestrel(opt =>
{
    opt.ConfigureHttpsDefaults(cfgHttps =>
    {
        cfgHttps.CheckCertificateRevocation = false;
        cfgHttps.ClientCertificateMode = ClientCertificateMode.RequireCertificate;
        cfgHttps.ClientCertificateValidation = (x509Certificate2, x509Chain, sslPolicyErrors) => 
        {
            bool isValidThumbprint = x509Certificate2.Thumbprint.ToLower() == Environment.GetEnvironmentVariable("ASPNETCORE_CERT_THUMBPRINT")!.ToLower();

            return isValidThumbprint;
        };

        string certificatePfxPath = Environment.GetEnvironmentVariable("ASPNETCORE_CERT_PFX_PATH")!;
        string certificatePfxPasskey = Environment.GetEnvironmentVariable("ASPNETCORE_CERT_PASSKEY")!;

        cfgHttps.ServerCertificate = new X509Certificate2(certificatePfxPath, certificatePfxPasskey);
    });

    opt.Listen(IPAddress.Any, 8986, listenCfg =>
    {
        listenCfg.UseHttps();
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }