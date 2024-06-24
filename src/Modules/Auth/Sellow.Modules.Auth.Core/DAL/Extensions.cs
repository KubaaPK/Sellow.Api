using Microsoft.Extensions.DependencyInjection;
using Sellow.Modules.Auth.Core.DAL.Repositories;
using Sellow.Modules.Auth.Core.Domain;
using Sellow.Shared.Infrastructure.DAL.Postgres;

namespace Sellow.Modules.Auth.Core.DAL;

internal static class Extensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services)
        => services
            .AddPostgres<AuthDbContext>()
            .AddScoped<IUserRepository, UserRepository>();
}