namespace ManagmentVPN.Domain.Entities
{
    public enum ServerState
    {
        ENABLED,
        DISABLED
    }
    public class Server
    {
        public Guid Id { get; set; }
        public ServerState State { get; set; }
        public ICollection<Key>? Keys { get; set; }
    }
}
