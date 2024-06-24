using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.Extensions.DependencyInjection;
using Sellow.Shared.Infrastructure.Options;

namespace Sellow.Modules.Auth.Core.Auth.Firebase;

internal static class Extensions
{
    public static IServiceCollection AddFirebase(this IServiceCollection services)
    {
        var firebaseOptions = services.GetOptions<FirebaseOptions>("Firebase");

        FirebaseApp.Create(new AppOptions
        {
            ProjectId = firebaseOptions.ProjectId,
            Credential = GoogleCredential.FromFile(firebaseOptions.ApiKeyFilePath)
        });

        services.AddScoped<IAuthService, FirebaseAuthService>();

        return services;
    }
}