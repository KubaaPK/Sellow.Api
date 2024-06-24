using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Sellow.Shared.Infrastructure.Api;
using Sellow.Shared.Infrastructure.Exceptions;

namespace Sellow.Shared.Infrastructure;

internal static class Extensions
{
    public static IServiceCollection AddSharedInfrastructure(this IServiceCollection services)
        =>
            services
                .AddEndpointsApiExplorer()
                .AddSwagger()
                .AddVersioning()
                .AddExceptionHandling()
                .AddControllers()
                .ConfigureApplicationPartManager(manager =>
                    manager.FeatureProviders.Add(new InternalControllerFeatureProvider()))
                .Services;

    public static IApplicationBuilder UseSharedInfrastructure(this IApplicationBuilder app)
        => app
            .UseHttpsRedirection()
            .UseSwagger_()
            .UseExceptionHandling();
}