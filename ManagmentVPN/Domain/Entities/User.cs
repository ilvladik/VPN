namespace Domain.Entities
{
    public enum UserRole
    {
        USER,
        ADMIN
    }

    public enum VpnAccessMode
    {
        ALLOWED,
        FORBIDDEN
    }
    public class User
    {
        public Guid Id { get; set; }
        public string Login { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public UserRole Role {  get; set; } 
        public VpnAccessMode VpnAccessMode { get; set; }
        public Key? Key { get; set; }
    }
}
