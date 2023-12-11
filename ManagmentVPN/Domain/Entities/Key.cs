namespace Domain.Entities
{
    public class Key
    {
        public Guid Id { get; set; }
        public string Password { get; set; } = string.Empty;
        public string Method {  get; set; } = string.Empty;
        public int KeyPort {  get; set; }
        public Guid ServerId { get; set; }
        public Guid UserId { get; set; }
        public Server? Server { get; set; }
        public User? User { get; set; }

    }
}
