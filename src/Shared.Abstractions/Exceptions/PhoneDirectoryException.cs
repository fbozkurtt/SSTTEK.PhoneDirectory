namespace Shared.Abstractions.Exceptions;

public abstract class PhoneDirectoryException : Exception
{
    protected PhoneDirectoryException(string message) : base(message)
    {
        
    }
}