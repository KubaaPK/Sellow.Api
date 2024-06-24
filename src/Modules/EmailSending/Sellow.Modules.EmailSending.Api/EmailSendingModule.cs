using Microsoft.Extensions.DependencyInjection;
using Sellow.Modules.EmailSending.Core;

namespace Sellow.Modules.EmailSending.Api;

internal static class EmailSendingModule
{
    public static IServiceCollection AddEmailSendingModule(this IServiceCollection services)
        => services.AddCore();
}