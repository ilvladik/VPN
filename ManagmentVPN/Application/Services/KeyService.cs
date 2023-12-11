using Domain;
using SharedKernel.Dtos.Key;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class KeyService : IKeyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private bool disposedValue;

        public KeyService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<KeyDtoResponse>> GetAllAsync()
        {
            return (await _unitOfWork.Keys.GetAllAsync()).Select(k => new KeyDtoResponse(k.Id, k.Password, k.KeyPort, k.Method, k.ServerId));
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
