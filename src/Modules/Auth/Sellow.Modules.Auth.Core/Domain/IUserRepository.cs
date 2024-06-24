namespace Sellow.Modules.Auth.Core.Domain;

internal interface IUserRepository
{
    Task Save(User user, CancellationToken cancellationToken = default);
    Task<bool> IsUserUnique(User user, CancellationToken cancellationToken = default);
    Task Delete(User user, CancellationToken cancellationToken = default);
}