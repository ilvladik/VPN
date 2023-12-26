using ManagmentVPN.Application.Dtos;
using ManagmentVPN.Domain;
using ManagmentVPN.Domain.Entities;
using System.Net.Http.Json;

namespace ManagmentVPN.Application.Services
{
    public class KeyService : IKeyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private bool disposedValue;

        public KeyService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<KeyDto> AddAsync(Guid userId)
        {
            var servers = await _unitOfWork.Servers.GetAllAsync();
            var server = servers.Where(s => s.State == ServerState.ENABLED).MinBy(s => s.Keys.Count);
            if (server == null)
                throw new Exception();
            using (HttpClient httpClient = new HttpClient())
            {
                UriBuilder builder = new UriBuilder("http", "vpn-api", 8001, $"api/keys/{server.Id}");
                using var response = await httpClient.PostAsync(builder.Uri, new StringContent(string.Empty));
                response.EnsureSuccessStatusCode();
                var key = await response.Content.ReadFromJsonAsync<KeyDto>();
                await _unitOfWork.Keys.AddAsync(new Key()
                {
                    Id = key.Id,
                    UserId = userId,
                    ServerId = key.ServerId,
                });
                await _unitOfWork.SaveChangesAsync();
                return key;
            }
        }

        public async Task<KeyDto> GetByIdAsync(Guid id)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                UriBuilder builder = new UriBuilder("http", "vpn-api", 8001, $"api/keys/{id}");
                using var response = await httpClient.GetAsync(builder.Uri);
                response.EnsureSuccessStatusCode();
                var key = await response.Content.ReadFromJsonAsync<KeyDto>();
                return key;
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                UriBuilder builder = new UriBuilder("http", "vpn-api", 8001, $"api/keys/{id}");
                using var response = await httpClient.DeleteAsync(builder.Uri);
                response.EnsureSuccessStatusCode();
                await _unitOfWork.Keys.DeleteByIdAsync(id);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task<KeyVpnDto> GetByUserIdAsync(Guid userId)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(userId);
            if (user == null)
                throw new Exception("User not found");
            
            if (user.VpnAccessMode == VpnAccessMode.FORBIDDEN)
                throw new Exception("Forbidden");

            if (user.Key == null)
            {
                var keyVpn = await AddAsync(user.Id);
                return new KeyVpnDto()
                {
                    NetworkId = keyVpn.Server.NetworkId,
                    KeyPort = keyVpn.KeyPort,
                    Password = keyVpn.Password,
                    Method = keyVpn.Method
                };
            }
            if (user.Key.Server.State == ServerState.DISABLED)
            {
                await DeleteAsync(user.Key.Id);
                var keyVpn = await AddAsync(user.Id);
                return new KeyVpnDto()
                {
                    NetworkId = keyVpn.Server.NetworkId,
                    KeyPort = keyVpn.KeyPort,
                    Password = keyVpn.Password,
                    Method = keyVpn.Method
                };
            }
            var key = await GetByIdAsync(user.Key.Id);
            return new KeyVpnDto()
            {
                NetworkId = key.Server.NetworkId,
                KeyPort = key.KeyPort,
                Password = key.Password,
                Method = key.Method
            };
        }

        public async Task<IEnumerable<KeyDto>> GetAllAsync()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                UriBuilder builder = new UriBuilder("http", "vpn-api", 8001, $"api/keys");
                using var response = await httpClient.GetAsync(builder.Uri);
                response.EnsureSuccessStatusCode();
                var keys = await response.Content.ReadFromJsonAsync<IEnumerable<KeyDto>>();
                return keys ?? new List<KeyDto>();
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _unitOfWork.Dispose();
                }

                disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
