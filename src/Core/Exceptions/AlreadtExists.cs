namespace Core.Exceptions;

public class AlreadyExistsException : Exception
{
    public AlreadyExistsException() : base() { }

    public AlreadyExistsException(string message) : base(message) { }

    public AlreadyExistsException(string message, Exception innerException)
        : base(message, innerException) { }

    public static AlreadyExistsException For(string resourceName, object identifier)
    {
        return new AlreadyExistsException($"{resourceName} com identificador {identifier} já existe.");
    }
}
