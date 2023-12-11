namespace VPN.Domain.Entities
{
    public class Key
    {
        public Guid Id { get; set; }
        public int OutlineId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int KeyPort { get; set; }
        public string Method { get; set; } = string.Empty;
        public Guid ServerId { get; set; }
        
        public Server? Server { get; set; }
    }
}
