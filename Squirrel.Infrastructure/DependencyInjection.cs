using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Squirrel.Application.Abstractions.Caching;
using Squirrel.Application.Abstractions.Data;
using Squirrel.Domain.Abstractions;
using Squirrel.Domain.Session;
using Squirrel.Domain.Statistics;
using Squirrel.Infrastructure.Caching;
using Squirrel.Infrastructure.Data;
using Squirrel.Infrastructure.Repositories;

namespace Squirrel.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        AddPersistence(services, configuration);
        
        AddCaching(services, configuration);
        
        AddHealthChecks(services, configuration);
        
        return services;
    }
    
    private static void AddPersistence(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString =
            configuration.GetConnectionString("Database") ??
            throw new ArgumentNullException(nameof(configuration));

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(connectionString).UseSnakeCaseNamingConvention();
        });

        services.AddScoped<ISessionRepository, SessionRepository>();

        services.AddScoped<IStatisticsRepository, StatisticsRepository>();
        
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

        services.AddSingleton<ISqlConnectionFactory>(_ =>
            new SqlConnectionFactory(connectionString));
    }
    
    private static void AddCaching(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Redis") ??
                               throw new ArgumentNullException(nameof(configuration));
        
        services.AddStackExchangeRedisCache(options => options.Configuration = connectionString);

        services.AddSingleton<ICacheService, CacheService>();
    }

    private static void AddHealthChecks(IServiceCollection services, IConfiguration configuration)
    {
        services.AddHealthChecks()
            .AddNpgSql(configuration.GetConnectionString("Database")!)
            .AddRedis(configuration.GetConnectionString("Redis")!);
    }
}