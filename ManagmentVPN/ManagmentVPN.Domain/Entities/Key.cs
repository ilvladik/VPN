namespace ManagmentVPN.Domain.Entities
{
    public class Key
    {
        public Guid Id { get; set; }
        public Guid ServerId { get; set; }
        public Guid UserId { get; set; }
        public Server? Server { get; set; }
        public User? User { get; set; }

    }
}
