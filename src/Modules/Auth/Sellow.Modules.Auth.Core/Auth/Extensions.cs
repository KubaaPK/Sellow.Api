using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Sellow.Modules.Auth.Core.Auth.Firebase;

namespace Sellow.Modules.Auth.Core.Auth;

internal static class Extensions
{
    public static IServiceCollection AddAuth(this IServiceCollection services)
        => services.AddFirebase();

    public static IApplicationBuilder UseAuth(this IApplicationBuilder app)
        => app
            .UseAuthentication()
            .UseAuthorization();
}