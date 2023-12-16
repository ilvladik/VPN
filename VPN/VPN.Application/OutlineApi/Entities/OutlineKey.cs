using System.Text.Json.Serialization;

namespace VPN.Application.OutlineApi.Entities
{
    public class OutlineKey
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("password")]
        public string Password { get; set; }
        [JsonPropertyName("port")]
        public int Port { get; set; }
        [JsonPropertyName("method")]
        public string Method { get; set; }
        [JsonPropertyName("accessUrl")]
        public string AccessUrl { get; set; }
    }
}
