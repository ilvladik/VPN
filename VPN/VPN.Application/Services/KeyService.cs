using VPN.Application.OutlineApi;
using VPN.Application.OutlineApi.Entities;
using VPN.Domain;
using VPN.Domain.Entities;
using VPN.Domain.Exceptions;

namespace VPN.Application.Services
{
    internal class KeyService : IKeyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOutlineProvider _outlineProvider;
        private bool disposedValue;

        public KeyService(IUnitOfWork unitOfWork, IOutlineProvider outlineProvider)
        {
            _unitOfWork = unitOfWork;
            _outlineProvider = outlineProvider;
        }

        public async Task<Key> CreateAsync(Guid id)
        {
            var server = await _unitOfWork.Servers.GetByIdAsync(id);
            var outlineKey = await _outlineProvider.CreateKeyAsync(new OutlineServer()
            {
                NetworkId = server.NetworkId,
                ApiPort = server.ApiPort,
                ApiPrefix = server.ApiPrefix,
            });
            Key key = new Key()
            {
                Id = Guid.NewGuid(),
                OutlineId = outlineKey.Id,
                Name = outlineKey.Name,
                Password = outlineKey.Password,
                Method = outlineKey.Method,
                KeyPort = outlineKey.Port,
                ServerId = server.Id
            };
            await _unitOfWork.Keys.AddAsync(key);
            await _unitOfWork.SaveChangesAsync();
            return key;
        }

        public async Task DeleteAsync(Guid id)
        {
            var key = await _unitOfWork.Keys.GetByIdAsync(id);
            if (key == null)
                return;
            await _unitOfWork.Keys.DeleteByIdAsync(id);
            await _outlineProvider.DeleteKeyAsync(new OutlineServer()
            {
                NetworkId = key.Server!.NetworkId,
                ApiPort = key.Server.ApiPort,
                ApiPrefix = key.Server.ApiPrefix
            }, key.OutlineId);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<Key> GetByIdAsync(Guid id)
        {
            var key = await _unitOfWork.Keys.GetByIdAsync(id);
            if (key == null) throw new Domain.Exceptions.KeyNotFoundException($"Key with id: {id} not found");
            return key;
        }

        public async Task<IEnumerable<Key>> GetByServerIdAsync(Guid id)
        {
            var keys = await _unitOfWork.Keys.GetByServerIdAsync(id);
            return keys;
        }

        public async Task<IEnumerable<Key>> GetAllAsync()
        {
            var keys = await _unitOfWork.Keys.GetAllAsync();
            return keys;
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
