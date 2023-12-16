using System.Net.Http.Json;
using VPN.Application.OutlineApi.Entities;

namespace VPN.Application.OutlineApi
{
    public class OutlineProvider : IOutlineProvider
    {
        public async Task<OutlineKey> CreateKeyAsync(OutlineServer server)
        {
            using (var httpClient = new HttpClient())
            {
                UriBuilder builder = new UriBuilder("https", server.NetworkId, server.ApiPort, $"{server.ApiPrefix}/access-keys");
                using HttpResponseMessage response = await httpClient.PostAsync(builder.Uri, new StringContent(string.Empty));
                response.EnsureSuccessStatusCode();
                OutlineKey? outlineKey = await response.Content.ReadFromJsonAsync<OutlineKey>();
                if (outlineKey == null)
                    throw new Exception($"Cannot create key, server: {server.NetworkId}");
                return outlineKey;
            }
        }

        public async Task DeleteKeyAsync(OutlineServer server, int outlineKeyId)
        {
            using (var httpClient = new HttpClient())
            {
                UriBuilder builder = new UriBuilder("https", server.NetworkId, server.ApiPort, $"{server.ApiPrefix}/access-keys/{outlineKeyId}");
                using HttpResponseMessage response = await httpClient.DeleteAsync(builder.Uri);
                response.EnsureSuccessStatusCode();
            }
        }

        public async Task<IEnumerable<OutlineKey>> GetAllKeysAsync(OutlineServer server)
        {
            using (var httpClient = new HttpClient())
            {
                UriBuilder builder = new UriBuilder("https", server.NetworkId, server.ApiPort, $"{server.ApiPrefix}/access-keys");
                using HttpResponseMessage response = await httpClient.GetAsync(builder.Uri);
                response.EnsureSuccessStatusCode();
                IEnumerable<OutlineKey>? outlineKeys = await response.Content.ReadFromJsonAsync<IEnumerable<OutlineKey>>();
                if (outlineKeys == null)
                    throw new Exception($"Cannot get a keys, server: {server.NetworkId}");
                return outlineKeys;
            }
        }

        public async Task<OutlineKey> GetKeyByIdAsync(OutlineServer server, int outlineKeyId)
        {
            using (var httpClient = new HttpClient())
            {
                UriBuilder builder = new UriBuilder("https", server.NetworkId, server.ApiPort, $"{server.ApiPrefix}/access-keys/{outlineKeyId}");
                using HttpResponseMessage response = await httpClient.GetAsync(builder.Uri);
                response.EnsureSuccessStatusCode();
                OutlineKey? outlineKey = await response.Content.ReadFromJsonAsync<OutlineKey>();
                if (outlineKey == null)
                    throw new Exception($"Cannot get a keys, server: {server.NetworkId}");
                return outlineKey;
            }
        }
    }
}
