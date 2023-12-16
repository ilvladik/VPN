using System.Text.Json.Serialization;

namespace ManagmentVPN.Application.Dtos
{
    public class KeyVpnDto
    {
        [JsonPropertyName("server")]
        public string NetworkId { get; set; }
        [JsonPropertyName("server_port")]
        public int KeyPort { get; set; }
        [JsonPropertyName("password")]
        public string Password { get; set; }
        [JsonPropertyName("method")]
        public string Method { get; set; }
    }
}
