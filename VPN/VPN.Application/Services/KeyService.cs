using SharedKernel.Dtos.Key;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VPN.Domain;

namespace VPN.Application.Services
{
    internal class KeyService : IKeyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private bool disposedValue;

        public KeyService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<KeyDtoResponse>> GetByServerIdAsync(Guid id)
        {
            var keys = await _unitOfWork.Keys.GetByServerIdAsync(id);
            return keys.Select(k => new KeyDtoResponse(k.Id, k.Password, k.KeyPort, k.Method, k.ServerId)).ToList();
        }

        public async Task<IEnumerable<KeyDtoResponse>> GetAllAsync()
        {
            var keys = await _unitOfWork.Keys.GetAllAsync();
            return keys.Select(k => new KeyDtoResponse(k.Id, k.Password, k.KeyPort, k.Method, k.ServerId)).ToList();
        }

        public async Task<KeyDtoResponse> CreateAsync()
        {
            var servers = await _unitOfWork.Servers.GetAllAsync();
            var min = servers.MinBy(s => s.Keys?.Count());
            var key = await _unitOfWork.Keys.CreateAsync(min.Id);
            await _unitOfWork.SaveChangesAsync();
            return new KeyDtoResponse(key.Id, key.Password, key.KeyPort, key.Method, key.ServerId);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _unitOfWork.Keys.DeleteByIdAsync(id);
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
