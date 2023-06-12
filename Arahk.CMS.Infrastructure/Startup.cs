using Arahk.CMS.Application.Repositories;
using Arahk.CMS.Domain.CMS;
using Arahk.CMS.Infrastructure.Persistants;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Arahk.CMS.Infrastructure;

public static class Startup
{
    public static IServiceCollection AddCMSInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<DefaultDbContext>(dbContextOptions =>
        {
#if DEBUG
            using IServiceScope scope = services.BuildServiceProvider().CreateScope();
            string connectionString = scope.ServiceProvider.GetRequiredService<IConfiguration>().GetConnectionString("Default")!;

            dbContextOptions.UseSqlServer(connectionString);
#else
            string connectionString = Environment.GetEnvironmentVariable("ASPNETCORE_CONN_STRING")!;

            dbContextOptions.UseSqlServer(connectionString);
#endif
        });

        services.AddScoped<IRepository<Content>, DefaultDbContext>();
        services.AddScoped<AuditInterceptor>();

        return services;
    }
}
