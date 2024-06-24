using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sellow.Shared.Infrastructure.Options;

namespace Sellow.Shared.Infrastructure.DAL.Postgres;

public static class Extensions
{
    public static IServiceCollection AddPostgres<T>(this IServiceCollection services) where T : DbContext
    {
        var postgresOptions = services.GetOptions<PostgresOptions>("Postgres");

        return services.AddDbContext<T>(options => options.UseNpgsql(postgresOptions.ConnectionString));
    }
}