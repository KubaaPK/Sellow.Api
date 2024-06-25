using MediatR;
using Sellow.Modules.Auth.Contracts.IntegrationEvents;
using Sellow.Modules.Auth.Core.Auth;

namespace Sellow.Modules.Auth.Core.Features;

internal sealed record ActivateUser(Guid Id) : IRequest;

internal sealed class ActivateUserHandler : IRequestHandler<ActivateUser>
{
    private readonly IAuthService _authService;
    private readonly IPublisher _publisher;

    public ActivateUserHandler(IAuthService authService, IPublisher publisher)
    {
        _authService = authService;
        _publisher = publisher;
    }

    public async Task Handle(ActivateUser request, CancellationToken cancellationToken)
    {
        var user = await _authService.ActivateUser(request.Id, cancellationToken);

        var userActivated = new UserActivated(user.Id, user.Email, user.Username);
        await _publisher.Publish(userActivated, cancellationToken);
    }
}