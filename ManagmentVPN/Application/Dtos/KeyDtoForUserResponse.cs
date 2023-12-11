using System.Text.Json.Serialization;

namespace Application.Dtos
{
    public class KeyDtoForUserResponse
    {
        public KeyDtoForUserResponse(string networkId, string password, int keyPort, string method)
        {
            NetworkId = networkId;
            Password = password;
            KeyPort = keyPort;
            Method = method;
        }
        [JsonPropertyName("server")]
        public string NetworkId { get; set; }
        [JsonPropertyName("password")]
        public string Password { get; set; } = string.Empty;
        [JsonPropertyName("port")]
        public int KeyPort { get; set; }
        [JsonPropertyName("method")]
        public string Method { get; set; } = string.Empty;
    }
}
