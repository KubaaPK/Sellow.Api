using MediatR;

namespace Sellow.Modules.Auth.Contracts.IntegrationEvents;

public sealed record UserCreated(Guid UserId, string Email, string Username) : INotification;