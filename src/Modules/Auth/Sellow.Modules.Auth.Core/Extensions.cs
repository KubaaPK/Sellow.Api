using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Sellow.Modules.Auth.Core.Auth;
using Sellow.Modules.Auth.Core.DAL;

namespace Sellow.Modules.Auth.Core;

internal static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
        => services
            .AddDatabase()
            .AddAuth()
            .AddMediatR(options => options.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()))
            .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), includeInternalTypes: true)
            .AddFluentValidationAutoValidation(options => options.DisableDataAnnotationsValidation = true);

    public static IApplicationBuilder UseCore(this IApplicationBuilder app)
        => app.UseAuth();
}