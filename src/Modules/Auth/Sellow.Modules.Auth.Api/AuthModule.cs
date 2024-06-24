using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Sellow.Modules.Auth.Core;

namespace Sellow.Modules.Auth.Api;

internal static class AuthModule
{
    public static IServiceCollection AddAuthModule(this IServiceCollection services)
        => services.AddCore();

    public static IApplicationBuilder UseAuthModule(this IApplicationBuilder app)
        => app.UseCore();
}