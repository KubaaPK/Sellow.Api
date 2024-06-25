namespace Sellow.Modules.Auth.Core.Auth;

internal interface IAuthService
{
    Task CreateUser(ExternalAuthUser user, CancellationToken cancellationToken = default);
    Task<PasswordlessExternalAuthUser> ActivateUser(Guid id, CancellationToken cancellationToken = default);
}