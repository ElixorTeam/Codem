namespace Codem.Domain.Exceptions;

public class ExpireDtNotValidException : Exception
{
    public ExpireDtNotValidException() 
        : base("Expired date can't be set")
    {
    }

    public ExpireDtNotValidException(string message)
        : base(message)
    {
    }

    public ExpireDtNotValidException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}