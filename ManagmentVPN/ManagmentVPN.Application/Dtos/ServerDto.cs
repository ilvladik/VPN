namespace ManagmentVPN.Application.Dtos;

public class ServerDto
{
    public Guid Id { get; set; }
    public string NetworkId { get; set; } = string.Empty;
    public int ApiPort { get; set; }
    public string ApiPrefix { get; set; } = string.Empty;

    public ICollection<KeyDto>? Keys { get; set; }
}
