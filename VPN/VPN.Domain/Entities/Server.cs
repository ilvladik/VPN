namespace VPN.Domain.Entities;

public class Server
{
    public Guid Id { get; set; }
    public string NetworkId { get; set; } = string.Empty;
    public int ApiPort { get; set; }
    public string ApiPrefix { get; set; } = string.Empty;

    public ICollection<Key>? Keys { get; set; }
}
