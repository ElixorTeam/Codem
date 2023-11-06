namespace Codem.Domain.Exceptions;

public class PasswordIsNotValidException: Exception
{
    public PasswordIsNotValidException() 
        : base("Password is not valid")
    {
    }

    public PasswordIsNotValidException(string message)
        : base(message)
    {
    }

    public PasswordIsNotValidException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}