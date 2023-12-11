namespace SharedKernel.Dtos.Key
{
    public class KeyDtoResponse
    {
        public KeyDtoResponse(Guid id, string password, int keyPort, string method, Guid serverId)
        {
            Id = id;
            Password = password;
            KeyPort = keyPort;
            Method = method;
            ServerId = serverId;
        }
        public Guid Id { get; set; }
        public string Password { get; set; } = string.Empty;
        public int KeyPort { get; set; }
        public string Method { get; set; } = string.Empty;
        public Guid ServerId { get; set; }
    }
}
