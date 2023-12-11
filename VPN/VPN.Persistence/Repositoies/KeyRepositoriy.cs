using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using VPN.Domain.Entities;
using VPN.Domain.Repositories;
using VPN.Persistence.Context;

namespace VPN.Persistence.Repositoies
{
    public class KeyRepositoriy : IKeyRepository
    {
        private readonly ApplicationDbContext _context;
        public KeyRepositoriy(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Key> CreateAsync(Guid serverId)
        {
            Server server = _context.Servers.First(s => s.Id == serverId);
            using (var httpClient = new HttpClient())
            {
                UriBuilder builder = new UriBuilder("https", server.NetworkId, server.ApiPort, $"{server.ApiPrefix}/access-keys");
                using HttpResponseMessage response = await httpClient.PostAsync(builder.Uri, new StringContent(string.Empty));
                response.EnsureSuccessStatusCode();
                string json = await response.Content.ReadAsStringAsync();
                Key key = DeserializeJsonToKey(json);
                key.Id = Guid.NewGuid();
                key.ServerId = server.Id;
                _context.Add(key);
                return key;
            }
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            Key key = await _context.Keys.Include(k => k.Server).FirstAsync(k => k.Id == id);
            using (var httpClient = new HttpClient())
            {
                UriBuilder builder = new UriBuilder("https", key.Server.NetworkId, key.Server.ApiPort, $"{key.Server.ApiPrefix}/access-keys/{key.OutlineId}");
                using HttpResponseMessage response = await httpClient.DeleteAsync(builder.Uri);
                response.EnsureSuccessStatusCode();
                _context.Remove(key);
            }
        }

        public async Task<IEnumerable<Key>> GetAllAsync()
        {
            return await _context.Keys.Include(k => k.Server).ToListAsync();
        }

        public async Task<Key?> GetByIdAsync(Guid id)
        {
            return await _context.Keys.FirstOrDefaultAsync(k => k.Id == id);
        }

        public async Task<IEnumerable<Key>> GetByServerIdAsync(Guid serverId)
        {
            return await _context.Keys.Include(k => k.Server).Where(k => k.ServerId == serverId).ToListAsync();
        }

        private Key DeserializeJsonToKey(string json)
        {
            using JsonDocument doc = JsonDocument.Parse(json);
            JsonElement root = doc.RootElement;
            Key key = new Key
            {
                OutlineId = root.GetProperty("id").GetInt32(),
                Name = root.GetProperty("name").GetString(),
                Password = root.GetProperty("password").GetString(),
                KeyPort = root.GetProperty("port").GetInt32(),
                Method = root.GetProperty("method").GetString()
            };
            return key;
        }
    }
}
