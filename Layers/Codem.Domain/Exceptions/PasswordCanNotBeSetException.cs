namespace Codem.Domain.Exceptions;

public class PasswordCanNotBeSetException : Exception
{
    public PasswordCanNotBeSetException() 
        : base("Password can't be set")
    {
    }

    public PasswordCanNotBeSetException(string message)
        : base(message)
    {
    }

    public PasswordCanNotBeSetException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}