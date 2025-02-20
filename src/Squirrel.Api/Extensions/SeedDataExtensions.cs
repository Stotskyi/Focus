using Bogus;
using Dapper;
using Squirrel.Application.Abstractions.Data;
using Squirrel.Domain.Session;

namespace Squirrel.Api.Extensions;

public static class SeedDataExtensions
{
    public static void SeedData(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();

        var sqlConnectionFactory = scope.ServiceProvider.GetRequiredService<ISqlConnectionFactory>();
        using var connection = sqlConnectionFactory.CreateConnection();

        var faker = new Faker();

        List<object> sessions = new();
        for (var i = 0; i < 100; i++)
        {
            sessions.Add(new
            {
                Id = Guid.NewGuid(),
                Duration = faker.Random.Int(1, 100),
                Type = faker.Random.Int(1, 100),
                UserId = Guid.NewGuid(),
                IsFinished = faker.Random.Bool()
            });
        }

        const string sql = $"""
                            INSERT INTO public.sessions
                            (id,  duration, type, user_id, is_finished)
                            VALUES(@Id, @Duration, @Type, @UserId, @IsFinished);
                            """;

        var rersponse = connection.Execute(sql, sessions);
    }
}