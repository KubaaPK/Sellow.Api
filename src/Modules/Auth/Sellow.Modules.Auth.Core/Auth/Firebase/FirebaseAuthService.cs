using System.Net;
using FirebaseAdmin.Auth;
using Microsoft.Extensions.Logging;
using Sellow.Shared.Abstractions.Exceptions;

namespace Sellow.Modules.Auth.Core.Auth.Firebase;

internal sealed class FirebaseAuthService : IAuthService
{
    private readonly ILogger<FirebaseAuthService> _logger;

    public FirebaseAuthService(ILogger<FirebaseAuthService> logger)
    {
        _logger = logger;
    }

    public async Task CreateUser(ExternalAuthUser user, CancellationToken cancellationToken = default)
    {
        await FirebaseAuth.DefaultInstance.CreateUserAsync(new UserRecordArgs
        {
            Uid = user.Id.ToString(),
            Email = user.Email,
            DisplayName = user.Username,
            Password = user.Password,
            Disabled = true,
            EmailVerified = false
        }, cancellationToken);

        _logger.LogInformation("Firebase user '{Id}' was created", user.Id);
    }

    public async Task<PasswordlessExternalAuthUser> ActivateUser(Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            var firebaseUser = await FirebaseAuth.DefaultInstance.GetUserAsync(id.ToString(), cancellationToken);
            if (firebaseUser.Disabled is false)
            {
                throw new FirebaseUserCannotBeActivated();
            }

            await FirebaseAuth.DefaultInstance.UpdateUserAsync(new UserRecordArgs
            {
                Uid = id.ToString(),
                Disabled = false,
                EmailVerified = true
            }, cancellationToken);

            _logger.LogInformation("Firebase user '{Id}' was activated", id);

            return new PasswordlessExternalAuthUser(
                Guid.Parse(firebaseUser.Uid),
                firebaseUser.Email,
                firebaseUser.DisplayName
            );
        }
        catch (FirebaseAuthException firebaseAuthException)
        {
            if (firebaseAuthException.AuthErrorCode == AuthErrorCode.UserNotFound)
            {
                throw new FirebaseUserCannotBeActivated();
            }

            throw;
        }
    }
}

internal sealed class FirebaseUserCannotBeActivated : PresentableException
{
    public override HttpStatusCode StatusCode => HttpStatusCode.UnprocessableEntity;
    public override string ErrorCode => "user_cannot_be_activated";

    public FirebaseUserCannotBeActivated() : base("User cannot be activated. Was not found or is already active.")
    {
    }
}