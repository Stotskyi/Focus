using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Squirrel.Application.Behaviours;

namespace Squirrel.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            
            configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));
            
            configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
            
            configuration.AddOpenBehavior(typeof(QueryCachingBehaviour<,>));
        });
        
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
        
        return services;
    }
}