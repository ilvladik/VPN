namespace VPN.Domain.Exceptions;

public class KeyNotFoundException : Exception
{
    public KeyNotFoundException() { }
    public KeyNotFoundException(string message) : base(message) { }
}
