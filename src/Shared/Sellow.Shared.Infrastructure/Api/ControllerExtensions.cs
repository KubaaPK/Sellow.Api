using Microsoft.AspNetCore.Http;

namespace Sellow.Shared.Infrastructure.Api;

public static class ControllerExtensions
{
    public static string GetActionUrl(this HttpRequest request)
        => $"{request.Scheme}://{request.Host.Value}/{request.Path}";
}