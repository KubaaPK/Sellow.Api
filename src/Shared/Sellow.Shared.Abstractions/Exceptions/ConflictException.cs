using System.Net;

namespace Sellow.Shared.Abstractions.Exceptions;

public abstract class ConflictException : PresentableException
{
    public override HttpStatusCode StatusCode => HttpStatusCode.Conflict;

    public ConflictException(string message) : base(message)
    {
    }
}