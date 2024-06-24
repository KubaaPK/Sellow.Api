namespace Sellow.Modules.Auth.Core.Auth.Firebase;

internal sealed class FirebaseOptions
{
    public string? ApiKeyFilePath { get; set; }
    public string? ProjectId { get; set; }
    public string? Authority { get; set; }
    public string? ValidIssuer { get; set; }
    public string? ValidAudience { get; set; }
}