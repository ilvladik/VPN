using SharedKernel.Dtos.Server;
using VPN.Domain;
using VPN.Domain.Entities;

namespace VPN.Application.Services
{
    public class ServerService : IServerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private bool disposedValue;

        public ServerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ServerDtoResponse> AddAsync(ServerDtoAddRequest serverDtoAddRequest)
        {
            Server server = await _unitOfWork.Servers.AddAsync(new Server()
            {
                Id = Guid.NewGuid(),
                NetworkId = serverDtoAddRequest.NetworkId,
                ApiPort = serverDtoAddRequest.ApiPort,
                ApiPrefix = serverDtoAddRequest.ApiPrefix,
            });
            await _unitOfWork.SaveChangesAsync();
            return new ServerDtoResponse(server.Id, server.NetworkId, server.ApiPort, server.ApiPrefix);
        }

        public async Task<IEnumerable<ServerDtoResponse>> GetAllAsync()
        {
            var server = await _unitOfWork.Servers.GetAllAsync();
            return server.Select(s => new ServerDtoResponse(s.Id, s.NetworkId, s.ApiPort, s.ApiPrefix));
        }

        public async Task DeleteAsync(Guid id)
        {
            await _unitOfWork.Servers.DeleteByIdAsync(id);
            await _unitOfWork.SaveChangesAsync();
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
