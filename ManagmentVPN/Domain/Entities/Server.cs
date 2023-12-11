namespace Domain.Entities
{
    public enum ServerState
    {
        CREATE_ALLOWED,
        GET_ALLOWED,
        FORBIDDEN
    }
    public class Server
    {
        public Guid Id { get; set; }
        public string NetworkId { get; set; } = string.Empty;
        public ServerState State { get; set; }
        public ICollection<Key>? Keys { get; set; }
    }
}
