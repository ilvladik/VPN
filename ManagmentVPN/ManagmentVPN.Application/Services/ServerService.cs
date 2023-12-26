using ManagmentVPN.Application.Dtos;
using ManagmentVPN.Domain;
using ManagmentVPN.Domain.Entities;
using System.Net.Http;
using System.Net.Http.Json;

namespace ManagmentVPN.Application.Services
{
    public class ServerService : IServerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private bool disposedValue;

        public ServerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddAsync(string networkId, string apiPort, string apiPrefix)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                UriBuilder builder = new UriBuilder("http", "vpn.api", 1000, $"api/servers");
                var jsonContent = JsonContent.Create(new 
                {
                    networkId,
                    apiPort,
                    apiPrefix
                });

                using var response = await httpClient.PostAsync(builder.Uri, jsonContent);
                response.EnsureSuccessStatusCode();
                var server = await response.Content.ReadFromJsonAsync<ServerDto>();
                await _unitOfWork.Servers.AddAsync(new Server
                {
                    Id = server.Id,
                    State = ServerState.ENABLED,
                    Keys = new List<Key>()
                });
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                UriBuilder builder = new UriBuilder("http", "vpn.api", 1000, $"api/servers/{id}");
                using var response = await httpClient.DeleteAsync(builder.Uri);
                if (!response.IsSuccessStatusCode)
                    throw new Exception("Cannot connect to VPN");
                await _unitOfWork.Servers.DeleteByIdAsync(id);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task DisableAsync(Guid id)
        {
            var server = await _unitOfWork.Servers.GetByIdAsync(id);
            if (server is null)
                throw new Exception($"Server not found {server.Id}");
            server.State = ServerState.DISABLED;
            await _unitOfWork.Servers.UpdateAsync(server);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task EnableAsync(Guid id)
        {
            var server = await _unitOfWork.Servers.GetByIdAsync(id);
            if (server is null)
                throw new Exception($"Server not found {server.Id}");
            server.State = ServerState.ENABLED;
            await _unitOfWork.Servers.UpdateAsync(server);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<Server>> GetAllAsync()
        {
            return await _unitOfWork.Servers.GetAllAsync();
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
