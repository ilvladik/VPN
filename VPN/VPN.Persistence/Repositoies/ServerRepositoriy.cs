using VPN.Domain.Entities;
using VPN.Domain.Repositories;
using VPN.Persistence.Context;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace VPN.Persistence.Repositoies
{
    public class ServerRepositoriy : IServerRepository
    {
        private readonly ApplicationDbContext _context;
        public ServerRepositoriy(ApplicationDbContext context)
        { 
            _context = context; 
        }
        public async Task<Server> AddAsync(Server server)
        {
            using (var httpClient = new HttpClient())
            {
                UriBuilder builder = new UriBuilder("https", server.NetworkId, server.ApiPort, $"{server.ApiPrefix}/access-keys");
                using HttpResponseMessage response = await httpClient.GetAsync(builder.Uri);
                response.EnsureSuccessStatusCode();
                string json = await response.Content.ReadAsStringAsync();
                List<Key> keys = DeserializeJsonToListOfKeys(json);
                keys.Select(k => { k.Id = Guid.NewGuid(); k.ServerId = server.Id; k.Server = server; return k; });
                server.Keys = keys;
                _context.Add(server);
                return server;
            }
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            _context.Servers.Remove(await _context.Servers.FirstAsync(s => s.Id == id));
        }

        public async Task<IEnumerable<Server>> GetAllAsync()
        {
            return await _context.Servers.ToListAsync();
        }

        public async Task<Server?> GetByIdAsync(Guid id)
        {
            return await _context.Servers.FirstOrDefaultAsync(s => s.Id == id);
        }

        private List<Key> DeserializeJsonToListOfKeys(string json)
        {
            using JsonDocument doc = JsonDocument.Parse(json);
            JsonElement root = doc.RootElement;

            List<Key> result = new List<Key>();
            JsonElement accessKeys = root.GetProperty("accessKeys");
            foreach (var key in accessKeys.EnumerateArray())
            {
                Key item = new Key
                {
                    OutlineId = key.GetProperty("id").GetInt32(),
                    Name = key.GetProperty("name").GetString(),
                    Password = key.GetProperty("password").GetString(),
                    KeyPort = key.GetProperty("port").GetInt32(),
                    Method = key.GetProperty("method").GetString()
                };
                result.Add(item);
            }
            return result;
        }
    }
}
