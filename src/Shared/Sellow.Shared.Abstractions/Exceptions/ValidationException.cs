using System.Net;

namespace Sellow.Shared.Abstractions.Exceptions;

public abstract class ValidationException : PresentableException
{
    public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

    public ValidationException(string message) : base(message)
    {
    }
}