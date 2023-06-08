using Arahk.CMS.Infrastructure.Persistants;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;

namespace Arahk.CMS.Api.Tests;

public class TestWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override IHostBuilder? CreateHostBuilder()
    {
        IHostBuilder? hostBuilder = base.CreateHostBuilder();

        hostBuilder?.ConfigureServices(s =>
            {
                IEnumerable<ServiceDescriptor> registeredServices = s.Where(p => p.ServiceType.FullName?.Contains("DefaultDbContext") ?? false).ToList();

                foreach (ServiceDescriptor registeredService in registeredServices)
                {
                    s.Remove(registeredService);
                }

                s.AddDbContext<DefaultDbContext>(dbContextOpt =>
                {
                    dbContextOpt.UseInMemoryDatabase("TestDb");
                });
            });


        return hostBuilder;
    }
}