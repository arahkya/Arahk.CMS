using System.Net;
using Serilog;
using Arahk.CMS.Application;
using Arahk.CMS.Infrastructure;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddCMSApplication();
builder.Services.AddCMSInfrastructure();

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
