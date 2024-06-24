namespace Sellow.Modules.Auth.Core.Auth;

internal sealed record ExternalAuthUser(Guid Id, string Email, string Username, string Password);