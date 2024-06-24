using Sellow.Shared.Abstractions.SharedKernel.ValueObjects;

namespace Sellow.Modules.Auth.Core.Domain;

internal sealed class User
{
    public Guid Id { get; set; }
    public Email Email { get; set; }
    public Username Username { get; set; }

    public User(Email email, Username username)
    {
        Email = email;
        Username = username;
    }
}