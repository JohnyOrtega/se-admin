using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
namespace Core.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException() : base() { }

    public NotFoundException(string message) : base(message) { }

    public NotFoundException(string message, Exception innerException)
        : base(message, innerException) { }

    public static NotFoundException For(string resourceName, object identifier)
    {
        return new NotFoundException($"{resourceName} com identificador {identifier} não encontrado.");
    }
}
