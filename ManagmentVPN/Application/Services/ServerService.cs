using Domain;
using Domain.Entities;
using SharedKernel;
using SharedKernel.Dtos.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ServerService : IServerService
    {
        private readonly HttpClient _httpClient;
        private readonly IUnitOfWork _unitOfWork;
        private bool disposedValue;

        public ServerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _httpClient = new HttpClient();
        }
        public async Task AddAsync(ServerDtoAddRequest request)
        {
            using var response = await _httpClient.PostAsync("http://vpn.api:8081/api/servers", new StringContent(""));
            if (!response.IsSuccessStatusCode)
                throw new Exception("Cannot connect to VPN");
            Response<ServerDtoResponse>? serverDtoResponse = await response.Content.ReadFromJsonAsync<Response<ServerDtoResponse>>();
            if (serverDtoResponse is null)
                throw new Exception($"Json Parse Exception typeof:{typeof(Response<ServerDtoResponse>)}");
            if (!serverDtoResponse.IsSuccess && serverDtoResponse.Dto is not null)
                throw new Exception(serverDtoResponse.Errors.ToArray().ToString());
            await _unitOfWork.Servers.AddAsync(new Server()
            {
                Id = serverDtoResponse.Dto.Id,
                NetworkId = serverDtoResponse.Dto.NetworkId,
                State = ServerState.CREATE_ALLOWED,
            });
            await _unitOfWork.SaveChangesAsync();

        }

        public async Task ChangeStateAsync(Guid id, ServerState state)
        {
            var server = await _unitOfWork.Servers.GetByIdAsync(id);
            server.State = state;
            await _unitOfWork.Servers.UpdateAsync(server);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            using var response = await _httpClient.DeleteAsync("");
            if (!response.IsSuccessStatusCode)
                throw new Exception("Cannot connect to VPN");
            await _unitOfWork.Servers.DeleteByIdAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<Server>> GetAllAsync()
        {
            return (await _unitOfWork.Servers.GetAllAsync());
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _unitOfWork.Dispose();
                    _httpClient.Dispose();
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
