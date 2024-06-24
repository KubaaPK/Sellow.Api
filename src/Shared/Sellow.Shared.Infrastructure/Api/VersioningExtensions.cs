using Asp.Versioning;
using Microsoft.Extensions.DependencyInjection;

namespace Sellow.Shared.Infrastructure.Api;

internal static class VersioningExtensions
{
    public static IServiceCollection AddVersioning(this IServiceCollection services)
        => services
            .AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
            })
            .Services;
}