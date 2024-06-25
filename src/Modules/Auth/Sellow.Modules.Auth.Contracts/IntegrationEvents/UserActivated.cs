using MediatR;

namespace Sellow.Modules.Auth.Contracts.IntegrationEvents;

public sealed record UserActivated(Guid UserId, string Email, string Username) : INotification;