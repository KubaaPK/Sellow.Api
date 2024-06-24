using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Sellow.Modules.EmailSending.Core.EmailClients.Sendgrid;

namespace Sellow.Modules.EmailSending.Core;

internal static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
        => services
            .AddSendgrid()
            .AddMediatR(options => options.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
}