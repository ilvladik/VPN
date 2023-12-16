namespace VPN.Domain.Exceptions;

public class ServerNotFoundException : Exception
{
    public ServerNotFoundException() { }
    public ServerNotFoundException(string message) : base(message) { }
}
