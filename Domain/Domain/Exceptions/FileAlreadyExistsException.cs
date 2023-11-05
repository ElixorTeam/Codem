namespace Domain.Exceptions;

public class FileAlreadyExistsException : Exception
{
    public FileAlreadyExistsException() : base("File can't be set")
    {
    }

    public FileAlreadyExistsException(string message)
        : base(message)
    {
    }

    public FileAlreadyExistsException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}