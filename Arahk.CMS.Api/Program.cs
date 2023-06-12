using System.Net;
using Serilog;
using Arahk.CMS.Application;
using Arahk.CMS.Infrastructure;
using Arahk.CMS.Application.Common;
using Arahk.CMS.Api.Services;
using Arahk.CMS.Api.Authentication;
#if !DEBUG
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
#endif

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddCMSApplication();
builder.Services.AddCMSInfrastructure();

#if DEBUG
builder.Services.AddBypassAuthentication();
#else
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddMicrosoftIdentityWebApi(jwtBearerOptions => { }, microsoftIdentityOptions =>
{
    string azureInstance = Environment.GetEnvironmentVariable("AZURE_AD_INSTANCE")!;
    string ClientId = Environment.GetEnvironmentVariable("AZURE_AD_CLIENTID")!;
    string TenantId = Environment.GetEnvironmentVariable("AZURE_AD_TENANTID")!;

    microsoftIdentityOptions.Instance = azureInstance;
    microsoftIdentityOptions.ClientId = ClientId;
    microsoftIdentityOptions.TenantId = TenantId;
});
#endif

builder.Services.AddTransient<IDateTimeProvider, DateTimeProvider>();
builder.Services.AddTransient<IUserIdProvider, UserIdProvider>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

builder.WebHost.ConfigureKestrel(kestrelOpt =>
{
    kestrelOpt.ListenAnyIP(7104);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
app.UseSwagger();
app.UseSwaggerUI();
// }

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }