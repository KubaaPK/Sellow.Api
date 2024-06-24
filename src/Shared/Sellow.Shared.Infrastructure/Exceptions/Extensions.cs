using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Sellow.Shared.Infrastructure.Exceptions;

internal static class Extensions
{
    public static IServiceCollection AddExceptionHandling(this IServiceCollection services)
        => services.AddScoped<ExceptionHandlerMiddleware>();

    public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder app)
        => app.UseMiddleware<ExceptionHandlerMiddleware>();
}