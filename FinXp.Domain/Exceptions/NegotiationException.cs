namespace FinXp.Domain.Exceptions;

public class NegotiationException : Exception
{
    public NegotiationException() : base()
    {
    }

    public NegotiationException(string? message) : base(message)
    {
    }

    public NegotiationException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
