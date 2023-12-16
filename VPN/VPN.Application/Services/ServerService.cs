using VPN.Application.OutlineApi;
using VPN.Application.OutlineApi.Entities;
using VPN.Domain;
using VPN.Domain.Entities;
using VPN.Domain.Exceptions;

namespace VPN.Application.Services
{
    public class ServerService : IServerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOutlineProvider _outlineProvider;
        private bool disposedValue;

        public ServerService(IUnitOfWork unitOfWork, IOutlineProvider outlineProvider)
        {
            _unitOfWork = unitOfWork;
            _outlineProvider = outlineProvider;
        }

        public async Task<Server> AddAsync(string networkId, int apiPort, string apiPrefix)
        {
            var outlineKeys = await _outlineProvider.GetAllKeysAsync(new OutlineServer()
            {
                NetworkId = networkId,
                ApiPort = apiPort,
                ApiPrefix = apiPrefix
            });
            var serverId = Guid.NewGuid();
            var keys = outlineKeys.Select(o => new Key()
            {
                Id = Guid.NewGuid(),
                OutlineId = o.Id,
                Name = o.Name,
                Password = o.Password,
                KeyPort = o.Port,
                Method = o.Method,
                ServerId = serverId
            }).ToList();
            var server = new Server()
            {
                Id = serverId,
                NetworkId = networkId,
                ApiPort = apiPort,
                ApiPrefix = apiPrefix,
                Keys = keys
            };
            await _unitOfWork.Servers.AddAsync(server);
            await _unitOfWork.SaveChangesAsync();
            return server;
        }

        public async Task DeleteAsync(Guid id)
        {
            await _unitOfWork.Servers.DeleteByIdAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<Server>> GetAllAsync()
        {
            return await _unitOfWork.Servers.GetAllAsync();
        }

        public async Task<Server> GetByIdAsync(Guid id)
        {
            var server = await _unitOfWork.Servers.GetByIdAsync(id);
            if (server == null)
                throw new ServerNotFoundException($"Server with id: {id} not found");
            return server;
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
