using Arahk.CMS.Application.Repositories;
using Arahk.CMS.Domain.CMS;
using Arahk.CMS.Infrastructure.Persistants;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Arahk.CMS.Infrastructure;

public static class Startup
{
    public static IServiceCollection AddCMSInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<DefaultDbContext>(dbContextOptions =>
        {
            // dbContextOptions.UseInMemoryDatabase("Default");
            
            dbContextOptions.UseSqlServer("Server=localhost; Database=CMSDB; User Id=SA; Password=Password!123; TrustServerCertificate=True");
        });

        services.AddScoped<IRepository<Content>, DefaultDbContext>();
        services.AddScoped<AuditInterceptor>();

        return services;
    }
}
