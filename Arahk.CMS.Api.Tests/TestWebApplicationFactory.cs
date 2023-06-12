using Arahk.CMS.Domain.CMS;
using Arahk.CMS.Infrastructure.Persistants;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;

namespace Arahk.CMS.Api.Tests;

public class TestWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(s =>
        {
            IEnumerable<ServiceDescriptor> registeredServices = s.Where(p =>
                (p.ServiceType.FullName?.Contains("DefaultDbContext") ?? false)
            ).ToList();

            foreach (ServiceDescriptor registeredService in registeredServices)
            {
                s.Remove(registeredService);
            }

            s.AddDbContext<DefaultDbContext>(dbContextOpt =>
            {
                dbContextOpt.UseInMemoryDatabase("TestDb");
            });

            using IServiceScope scope = s.BuildServiceProvider().CreateScope();
            DefaultDbContext dbContext = scope.ServiceProvider.GetRequiredService<DefaultDbContext>();

            dbContext.Contents.Add(new Content
            {
                Id = Guid.Parse("a186c328-2f7d-4872-8f71-cabd985a7c83"),
                Title = "Existed Title",
                Message = "Existed Message"
            });

            int effectedRows = dbContext.SaveChanges();
        });

        base.ConfigureWebHost(builder);
    }
}