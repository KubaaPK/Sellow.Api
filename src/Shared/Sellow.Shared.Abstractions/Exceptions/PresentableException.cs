using System.Net;

namespace Sellow.Shared.Abstractions.Exceptions;

public abstract class PresentableException : Exception
{
    public abstract HttpStatusCode StatusCode { get; }
    public abstract string ErrorCode { get; }

    public PresentableException(string message) : base(message)
    {
    }
}