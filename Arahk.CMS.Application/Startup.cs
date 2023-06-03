using Arahk.CMS.Application.CQRS.Behaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;

namespace Arahk.CMS.Application;

public static class Startup
{
    public static IServiceCollection AddCMSApplication(this IServiceCollection services)
    {
        services.AddMediatR(mediatorCfg =>
        {
            mediatorCfg.RegisterServicesFromAssembly(typeof(Startup).Assembly);
        });

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidateBehavior<,>));

        services.AddValidatorsFromAssembly(typeof(Startup).Assembly);

        return services;
    }
}
