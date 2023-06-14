using System.Net;
using Serilog;
using Arahk.CMS.Application;
using Arahk.CMS.Infrastructure;
using Arahk.CMS.Application.Common;
using Arahk.CMS.Api.Services;
using Arahk.CMS.Api.Authentication;

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
builder.Services.AddAzureAuthentication();
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

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }