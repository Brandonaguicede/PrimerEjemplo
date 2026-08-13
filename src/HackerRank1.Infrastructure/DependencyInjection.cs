using HackerRank1.Application.Abstractions;
using HackerRank1.Application.Settings;
using HackerRank1.Infrastructure.Data;
using HackerRank1.Infrastructure.Persistence;
using HackerRank1.Infrastructure.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HackerRank1.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string? connectionString, JwtSettings jwtSettings)
    {
        if (string.IsNullOrWhiteSpace(connectionString))
            throw new InvalidOperationException("DefaultConnection connection string is required.");

        services.AddSingleton(jwtSettings);
        services.AddScoped<ILibraryRepository, LibraryRepository>();
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<ITokenService, JwtTokenService>();

        services.AddDbContextPool<LibraryContext>(options =>
            options.UseNpgsql(connectionString, npgsqlOptions =>
            {
                npgsqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 1,
                    maxRetryDelay: TimeSpan.FromSeconds(5),
                    errorCodesToAdd: null);
            }),
            poolSize: 20);

        return services;
    }

    public static void MigrateLibraryDatabase(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<LibraryContext>();
        db.Database.Migrate();
    }
}
